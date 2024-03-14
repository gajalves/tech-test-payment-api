namespace tech_test_payment.application.Dtos;

public class VendedorDto
{
    public Guid Id { get; set; }
    public string CPF { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public VendedorDto()
    { }

    public VendedorDto(Guid id, string cpf, string nome, string email, string telefone)
    {
        Id = id;
        CPF = cpf;
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }
}
