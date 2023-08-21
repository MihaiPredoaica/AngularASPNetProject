﻿using AnguilarTutorialAPI.Data;
using AnguilarTutorialAPI.DTOs;
using AnguilarTutorialAPI.Entity;
using AnguilarTutorialAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AnguilarTutorialAPI.Controllers
{
    public class AccountController : BaseApiController
    {
        private ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService) : base(context)
        {
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            using var hmac = new HMACSHA512();

            if (await UserExists(registerDTO.UserName)) return BadRequest("Username is taken");

            var user = new AppUser
            {
                UserName = registerDTO.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDTO 
            { 
                UserName = user.UserName, 
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDTO.UserName);
            if (user == null) return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string userName)
        {
            return await _context.Users.AnyAsync(user => user.UserName == userName.ToLower());
        }
    }
}