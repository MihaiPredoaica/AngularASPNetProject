using AnguilarTutorialAPI.DTOs;
using AnguilarTutorialAPI.Entity;
using AnguilarTutorialAPI.Helpers;
using AnguilarTutorialAPI.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AnguilarTutorialAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Include(x => x.Photos).SingleOrDefaultAsync(user => user.UserName == username);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<PagedList<MemberDTO>> GetMembersAsync(UserParams userParams)
        {
            var query = _context.Users
                    .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                    .AsNoTracking();

            return await PagedList<MemberDTO>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<MemberDTO> GetMemberAsync(string username)
        {
            return await _context.Users
                    .Where(x => x.UserName == username)
                    .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();
        }
    }
}
