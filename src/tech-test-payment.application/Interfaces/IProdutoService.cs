using tech_test_payment.application.Dtos;
using tech_test_payment.domain.Entities;

namespace tech_test_payment.application.Interfaces;

public interface IProdutoService
{
    Task<List<ProdutoDto>> ListarTodosOsProdutos();
}
