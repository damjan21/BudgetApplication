using Core.Domain.DTO.UserDTO;
using Core.Domain.Entites.Users;
using Core.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.ConsumerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
                _userService = userService;
        }

        [HttpPost]
        public async Task AddUser(User userToAdd)
        {
            await _userService.AddAsync(userToAdd);
        }

        [HttpPost("register")]
        public async Task RegisterAsync(UserDTO userToRegister) =>
            await _userService.RegisterAsync(userToRegister);
    }
}
