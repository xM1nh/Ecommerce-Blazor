using Server.Infrastructure.Interfaces;
using Server.Infrastructure.Repositories;

namespace Server.Infrastructure;

public class UnitOfWork(DatabaseContext context) : IUnitOfWork
{
    private readonly DatabaseContext _context = context;

    public IProductRepository Product { get; private set; } = new ProductRepository(context);
    public ICategoryRepository Category { get; private set; } = new CategoryRepository(context);

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}
