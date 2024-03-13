namespace tech_test_payment.domain.Entities;

public class VendaItem
{
    public int VendaId { get; set; }
    public Venda Venda { get; set; }

    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }

    public int Quantidade { get; set; }
}
