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
            new Vendedor(new Guid("aef8bedf-cccc-4670-83ac-fa00a75dc3f5"), "22700534000", "Bauner", "Bauner@mail.com", "2857-2450"),
            new Vendedor(new Guid("fdfd3b36-d5e3-45ed-97fc-e355920502a6"), "79425953074", "Flefle", "Flefle@mail.com", "3057-1871"),
            new Vendedor(new Guid("dc56cc05-58aa-46f8-870e-0f03ef0d5c9a"), "69651860030", "Cilis", "Cilis@mail.com", "3057-1871"),
            new Vendedor(new Guid("2326cbf7-3fdf-4b28-894b-f8bb8ffd73e5"), "65240550042", "Zusie", "Zusie@mail.com", "2969-9313"),            
            new Vendedor(new Guid("1b7d884f-f7ac-4814-b888-ffdf6780b552"), "55843128008", "Dael", "Dael@mail.com", "2175-1463"));
    }
}