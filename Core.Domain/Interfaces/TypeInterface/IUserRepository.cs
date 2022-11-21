using Core.Domain.Entites.Users;
using Core.Domain.Interfaces.GenericInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces.TypeInterface
{
    public interface IUserRepository : IGenericInterface<User>
    {
    }
}
