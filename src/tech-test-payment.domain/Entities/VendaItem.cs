using System.Text.Json.Serialization;

namespace tech_test_payment.domain.Entities;

public class VendaItem
{
    [JsonIgnore]
    public int VendaId { get; set; }

    [JsonIgnore]
    public Venda Venda { get; private set; }

    [JsonIgnore]
    public int ProdutoId { get; set; }


    public Produto Produto { get; private set; }
    public int Quantidade { get; private set; }
    public decimal Preco { get; private set; }

    public VendaItem()
    { }

    public VendaItem(Venda venda, Produto produto, int quantidade, decimal preco)
    {
        Venda = venda;
        Produto = produto;
        Quantidade = quantidade;
        Preco = preco;
    }

}
