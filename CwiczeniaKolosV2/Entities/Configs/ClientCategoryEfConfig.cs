using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CwiczeniaKolosV2.Entities.Configs;

public class ClientCategoryEfConfig : IEntityTypeConfiguration<ClientCategory>
{
    public void Configure(EntityTypeBuilder<ClientCategory> builder)
    {
        builder
            .HasKey(cc => cc.IdClientCategory)
            .HasName("ClientCategory_pk");

        builder
            .Property(cc => cc.IdClientCategory)
            .ValueGeneratedOnAdd();

        builder
            .Property(cc => cc.Name)
            .IsRequired();

        builder
            .Property(cc => cc.DiscountPerc)
            .IsRequired();

        builder
            .ToTable("ClientCategory");
    }
}