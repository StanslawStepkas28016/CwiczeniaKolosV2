using CwiczeniaKolosV2.ModelsDTOs;
using CwiczeniaKolosV2.Repositories;

namespace CwiczeniaKolosV2.Services;

public class BoatCompanyService : IBoatCompanyService
{
    private IBoatCompanyRepository _boatCompanyRepository;

    public BoatCompanyService(IBoatCompanyRepository boatCompanyRepository)
    {
        _boatCompanyRepository = boatCompanyRepository;
    }


    public async Task<ClientReservationsDto> GetClientReservations(int idClient, CancellationToken cancellationToken)
    {
        var res = await _boatCompanyRepository.GetClientReservations(idClient, cancellationToken);

        return res;
    }

    public async Task<int> AddNewReservation(NewReservationDto newReservationDto, CancellationToken cancellationToken)
    {
        var res = await _boatCompanyRepository.AddNewReservation(newReservationDto, cancellationToken);

        return res;
    }
}