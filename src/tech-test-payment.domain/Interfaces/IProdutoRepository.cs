using tech_test_payment.domain.Entities;

namespace tech_test_payment.domain.Interfaces;

public interface IProdutoRepository
{
    Task<List<Produto>> GetAllAsync();
}
