using Microsoft.EntityFrameworkCore;
using tech_test_payment.domain.Entities;
using tech_test_payment.infrastructure.Context;

namespace tech_test_payment.infrastructure.Repositories;

internal abstract class BaseRepository<T> where T : EntidadeBase
{

    private readonly ApplicationDbContext _context;

    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
