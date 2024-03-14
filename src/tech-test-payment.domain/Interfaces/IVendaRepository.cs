using tech_test_payment.domain.Entities;

namespace tech_test_payment.domain.Interfaces;

public interface IVendaRepository
{    
    Task<Venda> UpdateStatusAsync(Venda venda);

    Task<Venda> CreateAsync(Venda venda);

    Task<Venda> GetByIdAsync(Guid id);
}
