namespace tech_test_payment.application.Dtos;

public class ItemsRegistrarVendaDto
{
    public Guid ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal Preco { get; set;}
}
