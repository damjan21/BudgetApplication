using Common.Constants;
using Core.Domain.Entites.Users;
using Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task RegisterAsync(Guid id, string email, string password)
        {
            ApplicationUser applicationUser = new(id, email);
            IdentityResult result = await _userManager.CreateAsync(applicationUser, password);

            await _userManager.AddToRoleAsync(applicationUser, ApplicationUserRole.CUSTOMER);
        }

        public async Task LoginAsync(string email, string password)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(email,
                                                                 password,
                                                                 isPersistent: false,
                                                                 lockoutOnFailure: false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
