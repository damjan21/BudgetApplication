using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructures.Identity.EntityFramework.Data
{
    public class IdentityUnitOfWork
    {
        protected IdentityDbContext _context;

        public IdentityUnitOfWork(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task StartTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }
    }
}
