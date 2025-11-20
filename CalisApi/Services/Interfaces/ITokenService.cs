using CalisApi.Models;

namespace CalisApi.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
