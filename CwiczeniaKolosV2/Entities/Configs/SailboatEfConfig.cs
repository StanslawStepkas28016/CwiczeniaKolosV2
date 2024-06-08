using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CwiczeniaKolosV2.Entities.Configs;

public class SailboatEfConfig : IEntityTypeConfiguration<Sailboat>
{
    public void Configure(EntityTypeBuilder<Sailboat> builder)
    {
        builder
            .HasKey(s => s.IdSailboat)
            .HasName("Sailboat_pk");

        builder
            .Property(s => s.IdSailboat)
            .ValueGeneratedOnAdd();

        builder
            .Property(s => s.Name)
            .IsRequired();

        builder
            .Property(s => s.Capacity)
            .IsRequired();

        builder
            .Property(s => s.Description)
            .IsRequired();

        builder
            .Property(s => s.IdBoatStandard)
            .IsRequired();

        builder
            .Property(s => s.Price)
            .IsRequired();

        builder
            .HasOne(bs => bs.BoatStandard)
            .WithMany(s => s.Sailboats)
            .HasForeignKey(s => s.IdBoatStandard)
            .HasConstraintName("Sailboat_BoatStandard")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .ToTable("Sailboat");
    }
}