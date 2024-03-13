using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tech_test_payment.domain.Entities;

namespace tech_test_payment.infrastructure.Configurations;

internal class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Preco)
            .HasPrecision(10, 2);


        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<Produto> builder)
    {
        builder.HasData(
            new Produto("Produto numero 1", 9.99M),
            new Produto("Produto numero 2", 11.50M),
            new Produto("Produto numero 3", 20M),
            new Produto("Produto numero 4", 13.98M),
            new Produto("Produto numero 5", 10M));
    }
}
