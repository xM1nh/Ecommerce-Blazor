using SharedLibrary.Responses;
using System.Linq.Expressions;

namespace Server.Common;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();

    Task Add(T entity);

    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
}
