using AnguilarTutorialAPI.Entity;

namespace AnguilarTutorialAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
