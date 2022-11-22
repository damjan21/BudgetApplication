using Core.Domain.Entites.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.DTO.UserDTO
{
    public record UserDTO(
            string Email,
            string FirstName,
            string LastName,
            UserType UserType,
            string Country,
            string Password
        );
}
