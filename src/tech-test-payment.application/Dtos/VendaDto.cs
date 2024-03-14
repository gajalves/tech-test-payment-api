namespace tech_test_payment.application.Dtos;

public class VendaDto
{
    public Guid Id { get; set; }
    public VendedorDto Vendedor { get; set; }
    public string Status { get; set; }
    public DateTime DataVenda { get; set; }

    public List<ItemsVendaDto> Produtos { get; set; } = new();
}
