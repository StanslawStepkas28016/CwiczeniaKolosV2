using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CwiczeniaKolosV2.Entities.Configs;

public class BoatStandardEfConfig : IEntityTypeConfiguration<BoatStandard>
{
    public void Configure(EntityTypeBuilder<BoatStandard> builder)
    {
        builder
            .HasKey(bs => bs.IdBoatStandard)
            .HasName("BoatStandard_pk");

        builder
            .Property(bs => bs.IdBoatStandard)
            .ValueGeneratedOnAdd();


        builder
            .Property(bs => bs.Name)
            .IsRequired();

        builder
            .Property(bs => bs.Level)
            .IsRequired();

        builder
            .ToTable("BoatStandard");
    }
}