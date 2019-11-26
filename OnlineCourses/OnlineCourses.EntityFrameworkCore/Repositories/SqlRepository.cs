using Microsoft.EntityFrameworkCore;
using OnlineCourses.Application.Interfaces;
using OnlineCourses.Domain.SeedWork;
using OnlineCourses.EntityFrameworkCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourses.EntityFrameworkCore.Repositories
{
    public class SqlRepository<TEntity, TIdentity> : IRepository<TEntity>
       where TEntity : EntityBase<TEntity, TIdentity>
    {
        protected readonly OnlineCoursesContext Context;

        public IUnitOfWork UnitOfWork => Context;

        public SqlRepository(OnlineCoursesContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TEntity> AddAsync(TEntity aggregate)
        {
            var res = await Context.Set<TEntity>().AddAsync(aggregate);

            return res.Entity;
        }

        public async Task AddListAsync(IEnumerable<TEntity> aggregates)
        {
            await Context.Set<TEntity>().AddRangeAsync(aggregates);
        }

        public Task Delete(TEntity aggregate)
        {
            //_context.Set<T>().Remove(aggregate);
            Context.Entry(aggregate).State = EntityState.Deleted;
            Context.ChangeTracker.DetectChanges();

            return Task.CompletedTask;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            var res = await Context.Set<TEntity>().SingleOrDefaultAsync(x => x.Status == EntityStatus.Active && x.Id.Equals(id));

            return res;
        }

        public Task<TEntity> Update(TEntity aggregate)
        {
            Context.Set<TEntity>().Update(aggregate);

            return Task.FromResult(aggregate);
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            return await Context.Set<TEntity>().AsNoTracking().Where(x => x.Status == EntityStatus.Active).Where(expression).ToListAsync();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Context.Set<TEntity>().Where(x => x.Status == EntityStatus.Active).SingleOrDefaultAsync(expression);
        }
    }
}
