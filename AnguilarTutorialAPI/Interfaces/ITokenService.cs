using AnguilarTutorialAPI.Entity;

namespace AnguilarTutorialAPI.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
