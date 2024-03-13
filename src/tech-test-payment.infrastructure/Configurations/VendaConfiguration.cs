using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tech_test_payment.domain.Entities;

namespace tech_test_payment.infrastructure.Configurations;

internal class VendaConfiguration : IEntityTypeConfiguration<Venda>
{
    public void Configure(EntityTypeBuilder<Venda> builder)
    {
        builder.ToTable("Vendas");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Status)
            .IsRequired();

        builder.Property(v => v.DataVenda)
            .IsRequired();

        builder.HasOne(v => v.Vendedor)
            .WithMany()
            .HasForeignKey(v => v.VendedorId)
            .IsRequired();        
    }
}
