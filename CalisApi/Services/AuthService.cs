using CalisApi.Database.Interfaces;
using CalisApi.Models;
using CalisApi.Models.DTOs;
using CalisApi.Services.Interfaces;

namespace CalisApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        public AuthService( IHashService hashService, ITokenService tokenService ,IUserRepository userRepositoy)
        {
            _hashService = hashService;
            _userRepository = userRepositoy;
            _tokenService = tokenService;
        }
        public async Task<string> Register(RegisterDto user)
        {
            var exist = await _userRepository.GetByEmail(user.Email);
            if (exist != null) {
                throw new InvalidOperationException("El email ya está registrado.");
            }
            User newUser = new User()
            {
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                State = "Activo",
                Role = user.Role
            };
            newUser.Password = _hashService.Hash(newUser, user.Password);
            await _userRepository.Add(newUser);
            var token = _tokenService.GenerateJwtToken(newUser);
            return token;
        }
    }
}
