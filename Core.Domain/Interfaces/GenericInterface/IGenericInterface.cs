using Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces.GenericInterface
{
    public interface IGenericInterface<TemplateEntity> where TemplateEntity : class, new()
    {
        public Task AddAsync(TemplateEntity entity);

        public IEnumerable<TemplateEntity> Find(Expression<Func<TemplateEntity, bool>> expression);

        public Task<TemplateEntity?> FindAsync(Expression<Func<TemplateEntity, bool>> expression);

        public IEnumerable<TemplateEntity> GetAll();

        public Task<TemplateEntity?> GetByIdAsync(Guid id);

        public void Remove(TemplateEntity entity);

        public Task SaveAsync();

        public void Update(TemplateEntity entity);
    }
}
