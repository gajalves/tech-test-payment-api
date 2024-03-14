namespace tech_test_payment.domain.Entities;

public class Vendedor : EntidadeBase
{
    public string CPF { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Telefone { get; private set; }
    
    public Vendedor()
    { }

    public Vendedor(string cpf, string nome, string email, string telefone)
    {        
        CPF = cpf;
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }

    public Vendedor(Guid id, string cpf, string nome, string email, string telefone)
    {
        Id = id;
        CPF = cpf;
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }
}
