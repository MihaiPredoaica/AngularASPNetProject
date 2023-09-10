using AnguilarTutorialAPI.DTOs;
using AnguilarTutorialAPI.Entity;
using AnguilarTutorialAPI.Helpers;

namespace AnguilarTutorialAPI.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<PagedList<MemberDTO>> GetMembersAsync(UserParams userParams);
        Task<MemberDTO> GetMemberAsync(string username);
        Task<AppUser> GetUserByUsernameAsync(string username);
    }
}
