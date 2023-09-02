using AnguilarTutorialAPI.DTOs;
using AnguilarTutorialAPI.Entity;

namespace AnguilarTutorialAPI.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<MemberDTO>> GetMembersAsync();
        Task<MemberDTO> GetMemberAsync(string username);
        Task<AppUser> GetUserByUsernameAsync(string username);
    }
}
