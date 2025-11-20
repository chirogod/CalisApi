using CalisApi.Models;
using CalisApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CalisApi.Services
{
    public class HashService : IHashService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        public HashService(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string Hash(User user, string password)
        {
            return _passwordHasher.HashPassword(user,password);
        }
        public bool Verify(User user, string hashedPass, string inputPass)
        {
            return true;
        }
    }
}
