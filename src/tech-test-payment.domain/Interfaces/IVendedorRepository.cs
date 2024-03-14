using tech_test_payment.domain.Entities;

namespace tech_test_payment.domain.Interfaces;

public interface IVendedorRepository
{
    Task<List<Vendedor>> GetAllAsync();
}
