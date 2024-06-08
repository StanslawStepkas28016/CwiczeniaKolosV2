namespace CwiczeniaKolosV2.Entities;

public class Sailboat
{
    public int IdSailboat { get; set; }

    public string? Name { get; set; }

    public int Capacity { get; set; }

    public string? Description { get; set; }

    public int IdBoatStandard { get; set; }

    public virtual BoatStandard BoatStandard { get; set; }

    public int Price { get; set; }

    public virtual ICollection<SailboatReservation> SailboatReservations { get; set; }
}