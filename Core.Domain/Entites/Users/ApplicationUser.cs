using Common.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entites.Users
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser(Guid id, string email)
        {
            if (email.Length > 255)
                throw new ArgumentException("Email is too long.");
            EmailFormatExtension.CheckEmailFormat(email);

            Id = id;
            Email = email;
            UserName = EmailFormatExtension.GetEmailWithoutDomain(email);
        }
    }
}
