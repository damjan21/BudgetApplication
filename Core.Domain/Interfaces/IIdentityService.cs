using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface IIdentityService
    {
        Task LoginAsync(string email, string password);
        Task LogoutAsync();
        Task RegisterAsync(Guid id, string email, string password);
    }
}
