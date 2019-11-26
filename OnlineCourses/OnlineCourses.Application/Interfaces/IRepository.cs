using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourses.Application.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(Guid id);

        Task<T> AddAsync(T aggregate);

        Task AddListAsync(IEnumerable<T> aggregates);

        Task Delete(T aggregate);

        Task<T> Update(T aggregate);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression);

        IUnitOfWork UnitOfWork { get; }
    }
}
