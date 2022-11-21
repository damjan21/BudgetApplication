using Core.Domain.Entites.Users;
using Core.Domain.Interfaces.TypeInterface;
using Core.Infrastructures.Data.SQLServer.Data;
using Core.Infrastructures.Data.SQLServer.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructures.Data.SQLServer.Repository.TypeRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }
    }
}
