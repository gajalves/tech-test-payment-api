namespace tech_test_payment.application.Dtos;

public class ProdutoDto
{
    public Guid Id {  get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }

    public ProdutoDto()
    { }

    public ProdutoDto(Guid id, string nome, decimal preco)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
    }
}
