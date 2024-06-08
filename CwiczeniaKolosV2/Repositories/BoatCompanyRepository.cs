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
                }).ToList()
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
}