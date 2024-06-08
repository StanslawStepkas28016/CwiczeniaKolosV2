namespace CwiczeniaKolosV2.Entities;

public class Reservation
{
    public int IdReservation { get; set; }

    public int IdClient { get; set; }

    public virtual Client Client { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public int IdBoatStandard { get; set; }

    public virtual BoatStandard BoatStandard { get; set; }

    public int Capacity { get; set; }

    public int NumOfBoats { get; set; }

    public bool Fulfilled { get; set; }

    public decimal? Price { get; set; }

    public string? CancelReason { get; set; }
}