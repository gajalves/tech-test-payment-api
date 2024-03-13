using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tech_test_payment.domain.Entities;

namespace tech_test_payment.infrastructure.Configurations;

internal class VendaItemConfiguration : IEntityTypeConfiguration<VendaItem>
{
    public void Configure(EntityTypeBuilder<VendaItem> builder)
    {
        builder.ToTable("VendaItems");

        builder.HasKey(vp => new { vp.VendaId, vp.ProdutoId });

        builder.Property(v => v.Quantidade)
                .IsRequired();

        builder.Property(v => v.Preco)
                .IsRequired();

        builder.HasOne(vp => vp.Produto)
               .WithMany()
               .HasForeignKey(vp => vp.ProdutoId);

        builder.HasOne(vp => vp.Venda)
               .WithMany(v => v.VendaItems)
               .HasForeignKey(vp => vp.VendaId);
    }
}
