using Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructures.Data.SQLServer.Data
{
    public class AppUnitOfWork : IAppUnitOfWork
    {
        protected AppDbContext _context;

        public AppUnitOfWork(AppDbContext context)
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
