using CwiczeniaKolosV2.Entities;
using CwiczeniaKolosV2.ModelsDTOs;
using Microsoft.EntityFrameworkCore;

namespace CwiczeniaKolosV2.Repositories;

public class BoatCompanyRepository : IBoatCompanyRepository
{
    private readonly BoatCompanyContext _context = new();

    public async Task<ClientReservationsDto> GetClientReservations(int idClient, CancellationToken cancellationToken)
    {
        if (await DoesClientExist(idClient, cancellationToken) == false)
        {
            return new ClientReservationsDto
            {
                IdClient = -1
            };
        }

        var clientReservationsDto = await _context
            .Clients
            .Include(c => c.Reservations)
            .Where(c => c.IdClient == idClient)
            .Select(c => new ClientReservationsDto
            {
                IdClient = c.IdClient,
                Name = c.Name,
                LastName = c.LastName,
                Birthday = c.Birthday,
                Pesel = c.Pesel,
                Email = c.Email,
                IdClientCategory = c.IdClientCategory,
                ReservationsList = c.Reservations.Select(r => new ReservationDto
                {
                    IdReservation = r.IdReservation,
                    IdClient = r.IdClient,
                    DateFrom = r.DateFrom,
                    DateTo = r.DateFrom,
                    IdBoatStandard = r.IdBoatStandard,
                    Capacity = r.Capacity,
                    NumOfBoats = r.NumOfBoats,
                    Fulfilled = r.Fulfilled,
                    Price = r.Price,
                    CancelReason = r.CancelReason
                }).OrderByDescending(r => r.DateTo).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        return clientReservationsDto!;
    }

    private async Task<bool> DoesClientExist(int idClient, CancellationToken cancellationToken)
    {
        var res = await _context
            .Clients
            .Where(c => c.IdClient == idClient)
            .FirstOrDefaultAsync(cancellationToken);

        return res != null;
    }

    public async Task<int> AddNewReservation(NewReservationDto newReservationDto, CancellationToken cancellationToken)
    {
        if (await DoesClientExist(newReservationDto.IdClient, cancellationToken) == false)
        {
            return -1;
        }

        if (await DoesClientHaveActiveReservation(newReservationDto.IdClient, cancellationToken))
        {
            await _context
                .Reservations
                .AddAsync(new Reservation
                {
                    IdClient = newReservationDto.IdClient,
                    DateFrom = newReservationDto.DateFrom,
                    DateTo = newReservationDto.DateTo,
                    IdBoatStandard = newReservationDto.IdBoatStandard,
                    Capacity = 0,
                    NumOfBoats = newReservationDto.NumOfBoats,
                    Fulfilled = false,
                    Price = null,
                    CancelReason = "Client already has an active reservation!",
                }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return -2;
        }

        if (await IsThereEnoughBoatsWithinGivenStandard(newReservationDto.NumOfBoats, newReservationDto.IdBoatStandard,
                cancellationToken) == false)
        {
            await _context
                .Reservations
                .AddAsync(new Reservation
                {
                    IdClient = newReservationDto.IdClient,
                    DateFrom = newReservationDto.DateFrom,
                    DateTo = newReservationDto.DateTo,
                    IdBoatStandard = newReservationDto.IdBoatStandard,
                    Capacity = 0,
                    NumOfBoats = newReservationDto.NumOfBoats,
                    Fulfilled = false,
                    Price = null,
                    CancelReason = "Not enough Boats within the given standard!",
                }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return -3;
        }

        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var reservation = new Reservation
            {
                IdClient = newReservationDto.IdClient,
                DateFrom = newReservationDto.DateFrom,
                DateTo = newReservationDto.DateTo,
                IdBoatStandard = newReservationDto.IdBoatStandard,
                Capacity = await _context.Sailboats
                    .Where(sb => sb.IdBoatStandard == newReservationDto.IdBoatStandard)
                    .Select(sb => sb.Capacity)
                    .FirstAsync(cancellationToken) * newReservationDto.NumOfBoats,
                NumOfBoats = newReservationDto.NumOfBoats,
                Fulfilled = false,
                Price = await _context
                    .Sailboats
                    .Where(sb => sb.IdBoatStandard == newReservationDto.IdBoatStandard)
                    .Take(newReservationDto.NumOfBoats)
                    .SumAsync(sb => sb.Price, cancellationToken),
                CancelReason = null,
            };

            await _context
                .Reservations
                .AddAsync(reservation, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var sailboats = await _context.Sailboats
                .Where(sb => sb.IdBoatStandard == newReservationDto.IdBoatStandard)
                .Take(newReservationDto.NumOfBoats)
                .ToListAsync(cancellationToken);

            foreach (var sailboat in sailboats)
            {
                await _context
                    .AddAsync(new SailboatReservation
                    {
                        IdReservation = reservation.IdReservation,
                        IdSailboat = sailboat.IdSailboat,
                    }, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);
            return reservation.IdReservation;
        }
        catch (Exception e)
        {
            throw new Exception(e.StackTrace);
            await transaction.RollbackAsync(cancellationToken);
            return -4;
        }
    }

    private async Task<bool> DoesClientHaveActiveReservation(int idClient, CancellationToken cancellationToken)
    {
        var res = await _context
            .Reservations
            .Where(r => r.IdClient == idClient
                        && r.CancelReason == null
                        && r.Fulfilled == false
            )
            .FirstOrDefaultAsync(cancellationToken);

        return res != null;
    }

    private async Task<bool> IsThereEnoughBoatsWithinGivenStandard(int numOfBoats, int idBoatStandard,
        CancellationToken cancellationToken)
    {
        var res = await _context
            .Sailboats
            .Include(s => s.BoatStandard)
            .Where(s => s.BoatStandard.IdBoatStandard == idBoatStandard)
            .CountAsync(cancellationToken);

        return res == numOfBoats;
    }
}