using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CwiczeniaKolosV2.Entities.Configs;

public class ClientEfConfig : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder
            .HasKey(c => c.IdClient)
            .HasName("Client_pk");

        builder
            .Property(c => c.IdClient)
            .ValueGeneratedOnAdd();

        builder
            .Property(c => c.Name)
            .IsRequired();

        builder
            .Property(c => c.LastName)
            .IsRequired();

        builder
            .Property(c => c.Birthday)
            .IsRequired();

        builder
            .Property(c => c.Pesel)
            .IsRequired();

        builder
            .Property(c => c.Email)
            .IsRequired();

        builder
            .Property(c => c.IdClientCategory)
            .IsRequired();

        builder
            .HasOne(cc => cc.ClientCategory)
            .WithMany(c => c.Clients)
            .HasForeignKey(c => c.IdClientCategory)
            .HasConstraintName("Client_ClientCategory")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .ToTable("Client");
    }
}