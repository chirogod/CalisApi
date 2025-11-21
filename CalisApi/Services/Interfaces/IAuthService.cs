using CalisApi.Models.DTOs;

namespace CalisApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDto user);
        Task<string> Login(LoginDto user);
    }
}
