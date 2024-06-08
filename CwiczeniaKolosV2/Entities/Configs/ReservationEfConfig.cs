using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CwiczeniaKolosV2.Entities.Configs;

public class ReservationEfConfig : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder
            .HasKey(r => r.IdReservation)
            .HasName("Reservation_pk");


        builder
            .Property(r => r.IdReservation)
            .ValueGeneratedOnAdd();

        builder
            .Property(r => r.IdClient)
            .IsRequired();

        builder
            .Property(r => r.DateFrom)
            .IsRequired();

        builder
            .Property(r => r.DateTo)
            .IsRequired();

        builder
            .Property(r => r.IdBoatStandard)
            .IsRequired();

        builder
            .Property(r => r.Capacity)
            .IsRequired();

        builder
            .Property(r => r.NumOfBoats)
            .IsRequired();

        builder
            .Property(r => r.Fulfilled)
            .IsRequired();

        builder
            .Property(r => r.Price)
            .IsRequired(false);

        builder
            .Property(r => r.CancelReason)
            .IsRequired(false);

        builder
            .HasOne(c => c.Client)
            .WithMany(r => r.Reservations)
            .HasForeignKey(r => r.IdClient)
            .HasConstraintName("Reservation_Client")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(b => b.BoatStandard)
            .WithMany(r => r.Reservations)
            .HasForeignKey(r => r.IdBoatStandard)
            .HasConstraintName("BoatStandard_Reservation")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .ToTable("Reservation");
    }
}