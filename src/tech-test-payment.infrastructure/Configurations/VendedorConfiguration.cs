using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tech_test_payment.domain.Entities;

namespace tech_test_payment.infrastructure.Configurations;

internal class VendedorConfiguration : IEntityTypeConfiguration<Vendedor>
{
    public void Configure(EntityTypeBuilder<Vendedor> builder)
    {
        builder.ToTable("Vendedores");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.CPF)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(v => v.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(v => v.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(v => v.Telefone)
            .HasMaxLength(15)
            .IsRequired();

        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<Vendedor> builder)
    {
        builder.HasData(
            new Vendedor("22700534000", "Bauner", "Bauner@mail.com", "2857-2450"),
            new Vendedor("79425953074", "Flefle", "Flefle@mail.com", "3057-1871"),
            new Vendedor("69651860030", "Cilis", "Cilis@mail.com", "3057-1871"),
            new Vendedor("65240550042", "Zusie", "Zusie@mail.com", "2969-9313"),            
            new Vendedor("55843128008", "Dael", "Dael@mail.com", "2175-1463"));
    }
}