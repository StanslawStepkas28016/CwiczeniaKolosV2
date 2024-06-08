using CwiczeniaKolosV2.ModelsDTOs;

namespace CwiczeniaKolosV2.Services;

public interface IBoatCompanyService
{
    public Task<ClientReservationsDto> GetClientReservations(int idClient, CancellationToken cancellationToken);
}