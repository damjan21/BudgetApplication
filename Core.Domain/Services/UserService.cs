using Core.Domain.DTO.UserDTO;
using Core.Domain.Entites.Users;
using Core.Domain.Interfaces;
using Core.Domain.Interfaces.TypeInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Services
{
    public class UserService
    {
        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IIdentityService _identityService;

        public UserService(IAppUnitOfWork unitOfWork, IUserRepository userRepository, IIdentityService identityService)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _identityService = identityService;
        }

        public async Task AddAsync(User newUser)
        {
            await _unitOfWork.StartTransactionAsync();

            await _userRepository.AddAsync(newUser);

            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();
        }

        public async Task RegisterAsync(UserDTO userToRegister)
        {
            await _unitOfWork.StartTransactionAsync();

            Guid id = Guid.NewGuid();

            User user = new(id, userToRegister);

            await _userRepository.AddAsync(user);

            await _unitOfWork.SaveChangesAsync();
            await _identityService.RegisterAsync(user.Id, user.Email, userToRegister.Password);
            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
