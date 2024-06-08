using CwiczeniaKolosV2.Entities;

namespace CwiczeniaKolosV2.ModelsDTOs;

public class ClientReservationsDto
{
    public int IdClient { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public DateTime Birthday { get; set; }

    public string? Pesel { get; set; }

    public string? Email { get; set; }

    public int IdClientCategory { get; set; }
    
    public List<ReservationDto> ReservationsList { get; set; }
}