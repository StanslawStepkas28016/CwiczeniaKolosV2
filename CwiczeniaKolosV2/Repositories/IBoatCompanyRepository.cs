using CwiczeniaKolosV2.ModelsDTOs;

namespace CwiczeniaKolosV2.Repositories;

public interface IBoatCompanyRepository
{
    public Task<ClientReservationsDto> GetClientReservations(int idClient, CancellationToken cancellationToken);
}