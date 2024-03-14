using tech_test_payment.domain.Entities;
using tech_test_payment.domain.Interfaces;
using tech_test_payment.infrastructure.Context;

namespace tech_test_payment.infrastructure.Repositories;

internal class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
{
    public ProdutoRepository(ApplicationDbContext context) : base(context)
    {
    }    
}
