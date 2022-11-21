using Core.Domain.Entites.Users;
using Core.Domain.Interfaces;
using Core.Domain.Interfaces.TypeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Services
{
    public class UserService
    {
        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IAppUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task AddAsync(User newUser)
        {
            await _unitOfWork.StartTransactionAsync();

            await _userRepository.AddAsync(newUser);

            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
