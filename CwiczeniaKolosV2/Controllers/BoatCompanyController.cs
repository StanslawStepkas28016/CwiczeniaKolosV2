using CwiczeniaKolosV2.Services;
using Microsoft.AspNetCore.Mvc;

namespace CwiczeniaKolosV2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoatCompanyController : ControllerBase
{
    private IBoatCompanyService _boatCompanyService;

    public BoatCompanyController(IBoatCompanyService boatCompanyService)
    {
        _boatCompanyService = boatCompanyService;
    }

    [HttpGet("GetClientReservations")]
    public async Task<IActionResult> GetClientReservations(int idClient, CancellationToken cancellationToken)
    {
        var res = await _boatCompanyService.GetClientReservations(idClient, cancellationToken);

        if (res.IdClient == -1)
        {
            return NotFound("Client does not exist!");
        }

        return Ok(res);
    }
}