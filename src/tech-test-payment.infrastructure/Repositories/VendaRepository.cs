using Microsoft.EntityFrameworkCore;
using tech_test_payment.domain.Entities;
using tech_test_payment.domain.Interfaces;
using tech_test_payment.infrastructure.Context;

namespace tech_test_payment.infrastructure.Repositories;

internal class VendaRepository : BaseRepository<Venda>, IVendaRepository
{
    private readonly ApplicationDbContext _context;

    public VendaRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Venda> CreateAsync(Venda venda)
    {
        _context.Set<Venda>().Add(venda);
        await SaveAsync();

        return venda;
    }
    
    public async Task<Venda> UpdateStatusAsync(Venda venda)
    {        
        _context.Entry(venda).Property(v => v.Status).IsModified = true;

        await SaveAsync();

        return venda;
    }

    public async Task<Venda> GetByIdAsync(Guid id)
    {
        return await _context.Set<Venda>()
                                .Include(v => v.Vendedor)
                                .Include(v => v.VendaItems)
                                .ThenInclude(vi => vi.Produto)
                                .Where(v => v.Id == id)
                                .FirstOrDefaultAsync();
    }
}
