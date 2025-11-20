using CalisApi.Models;

namespace CalisApi.Services.Interfaces
{
    public interface IHashService
    {
        public string Hash(User user, string password);
        public bool Verify(User user, string hashedPass, string inputPass);
    }
}
