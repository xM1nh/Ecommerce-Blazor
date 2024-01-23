using Microsoft.EntityFrameworkCore;
using Server.Common;
using SharedLibrary.Responses;
using System.Linq.Expressions;

namespace Server.Infrastructure.Common;

public class Repository<T>(DbContext context) : IRepository<T> where T : class
{
    protected readonly DbContext Context = context;

    public async Task<IEnumerable<T>> GetAll()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public virtual Task Add(T entity)
    {
        Context.Set<T>().Add(entity);
        return Task.CompletedTask;

    }

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
    {
        return await Context.Set<T>().Where(predicate).ToListAsync();
    }
}
