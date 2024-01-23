using Server.Infrastructure.Interfaces;

namespace Server.Infrastructure;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Product { get; }
    ICategoryRepository Category { get; }

    Task<int> Complete();
}
