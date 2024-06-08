using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CwiczeniaKolosV2.Entities.Configs;

public class SailboatReservationEfConfig : IEntityTypeConfiguration<SailboatReservation>
{
    public void Configure(EntityTypeBuilder<SailboatReservation> builder)
    {
        builder
            .HasKey(sr => new
            {
                sr.IdSailboat,
                sr.IdReservation
            })
            .HasName("Sailboat_Reservation_pk");

        builder
            .Property(sr => sr.IdSailboat)
            .IsRequired()
            .ValueGeneratedNever();

        builder
            .Property(sr => sr.IdReservation)
            .IsRequired()
            .ValueGeneratedNever();

        builder
            .HasOne(s => s.Sailboat)
            .WithMany(sr => sr.SailboatReservations)
            .HasForeignKey(sr => sr.IdSailboat)
            .HasConstraintName("Sailboat_Reservation_Sailboat")
            .OnDelete(DeleteBehavior.Restrict);


        builder
            .HasOne(s => s.Reservation)
            .WithMany(sr => sr.SailboatReservations)
            .HasForeignKey(sr => sr.IdReservation)
            .HasConstraintName("Sailboat_Reservation_Reservation")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .ToTable("Sailboat_Reservation");
    }
}