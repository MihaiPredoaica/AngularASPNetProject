using AnguilarTutorialAPI.Data;
using AnguilarTutorialAPI.DTOs;
using AnguilarTutorialAPI.Entity;
using AnguilarTutorialAPI.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AnguilarTutorialAPI.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {

            if (await UserExists(registerDTO.UserName)) return BadRequest("Username is taken");

            var user = _mapper.Map<AppUser>(registerDTO);

            user.UserName = registerDTO.UserName.ToLower();

            var results = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!results.Succeeded) return BadRequest(results.Errors);

            var roleResults = await _userManager.AddToRoleAsync(user, "Members");

            if (!roleResults.Succeeded) return BadRequest(roleResults.Errors);

            return new UserDTO 
            {
                Username = user.UserName, 
                Token = await _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender,
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.Users.Include(x => x.Photos).SingleOrDefaultAsync(x => x.UserName == loginDTO.UserName.ToLower());
            if (user == null) return Unauthorized("Invalid Username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new UserDTO
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender = user.Gender,
            };
        }

        private async Task<bool> UserExists(string userName)
        {
            return await _userManager.Users.AnyAsync(user => user.UserName == userName.ToLower());
        }
    }
}
