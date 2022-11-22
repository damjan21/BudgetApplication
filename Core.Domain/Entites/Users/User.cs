using Core.Domain.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entites.Users
{
    public enum UserType : byte
    {
        ADMIN = 1,
        CUSTOMER = 2
    }

    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public string Country { get; set; }

        public User()
        {

        }

        public User(
            string firstName, 
            string lastName, 
            string email, 
            UserType userType, 
            string country
            )
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserType = userType;
            Country = country;
        }

        public User(Guid id, UserDTO userDTO) : this(userDTO.FirstName, userDTO.LastName, userDTO.Email, userDTO.UserType, userDTO.Country)
        {
            Id = id;
        }


    }
}
