using tech_test_payment.domain.Entities;
using tech_test_payment.domain.Interfaces;
using tech_test_payment.infrastructure.Context;

namespace tech_test_payment.infrastructure.Repositories;

internal class VendedorRepository : BaseRepository<Vendedor>, IVendedorRepository
{
    public VendedorRepository(ApplicationDbContext context) : base(context)
    {
    }
}
