using CwiczeniaKolosV2.ModelsDTOs;
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

    [HttpPut("AddNewReservation")]
    public async Task<IActionResult> AddNewReservation(NewReservationDto newReservationDto,
        CancellationToken cancellationToken)
    {
        var res = await _boatCompanyService.AddNewReservation(newReservationDto, cancellationToken);

        if (res == -1)
        {
            return BadRequest("Client with the provided ID does not exist!");
        }

        if (res == -2)
        {
            return BadRequest("Client already has an active reservation!");
        }

        if (res == -3)
        {
            return BadRequest("Not enough Boats within the given standard!");
        }

        if (res == -4)
        {
            return BadRequest("Transaction error!");
        }

        return Ok(res);
    }
}