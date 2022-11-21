using Core.Domain.Interfaces.GenericInterface;
using Core.Infrastructures.Data.SQLServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Infrastructures.Data.SQLServer.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericInterface<T> where T : class, new()
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext atticDbContext)
        {
            _dbContext = atticDbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task AddAsync(T entity) =>
            await _dbSet.AddAsync(entity);

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression) =>
            _dbSet.Where(expression);

        public async Task<T?> FindAsync(Expression<Func<T, bool>> expresison) =>
            await _dbSet.FirstOrDefaultAsync(expresison);

        public IEnumerable<T> GetAll() =>
            _dbSet.ToList();

        public async Task<T?> GetByIdAsync(Guid id) =>
            await _dbSet.FindAsync(id);

        public void Remove(T entity) =>
            _dbSet.Remove(entity);

        public async Task SaveAsync() =>
            await _dbContext.SaveChangesAsync();

        public void Update(T entity) =>
            _dbContext.Update(entity);
    }
}