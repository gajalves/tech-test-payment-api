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
            new Produto(new Guid("bcb16fb2-a547-4c48-86ea-0fd6ee2604e4"), "Produto numero 1", 9.99M),
            new Produto(new Guid("eb66509b-c6fe-48ba-8282-aea0a3133634"), "Produto numero 2", 11.50M),
            new Produto(new Guid("65c8d6fe-86b6-4ef5-a9c4-a6a0d35d7d2e"), "Produto numero 3", 20M),
            new Produto(new Guid("bb6dad0b-832a-4fcc-b946-dfc6d3cbffd6"), "Produto numero 4", 13.98M),
            new Produto(new Guid("feb8c05c-84c3-4994-8817-f2807282728c"), "Produto numero 5", 10M));
    }
}
