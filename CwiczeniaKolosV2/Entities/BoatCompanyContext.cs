using CwiczeniaKolosV2.Entities.Configs;
using Microsoft.EntityFrameworkCore;

namespace CwiczeniaKolosV2.Entities;

public class BoatCompanyContext : DbContext
{
    public virtual DbSet<ClientCategory> ClientCategories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<BoatStandard> BoatStandards { get; set; }

    public virtual DbSet<SailboatReservation> SailboatReservations { get; set; }

    public virtual DbSet<Sailboat> Sailboats { get; set; }

    public BoatCompanyContext()
    {
    }

    public BoatCompanyContext(DbContextOptions<BoatCompanyContext> options) : base(options)
    {
    }

    // WAŻNE !!
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=master;User Id=sa;Password=bazaTestowa1234;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Aplikuje wszystkie konfigurację z folderu, ale podajemy jedną klasę tylko
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReservationEfConfig).Assembly);
    }
}