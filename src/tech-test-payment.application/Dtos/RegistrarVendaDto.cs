namespace tech_test_payment.application.Dtos;

public class RegistrarVendaDto
{
    public Guid VendedorId { get; set; }
    public List<ItemsRegistrarVendaDto> Items { get; set; } = new();
}
