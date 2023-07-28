using System.Security.Cryptography;
using System.Text;
using DatingApp.API.Data;
using DatingApp.API.DTOs.Account;
using DatingApp.API.DTOs.AppUser;
using DatingApp.API.Entities;
using DatingApp.API.Interfaces;
using DatingApp.API.Interfaces.JWT;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DatingAppDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public AccountController(DatingAppDbContext context, ITokenService tokenService, IUserRepository userRepository)
        {
            _tokenService = tokenService;
            _context = context;
            _userRepository = userRepository;
        }

        [HttpPost("register")] // POST: api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await IsUserExisted(registerDto.Username))
                return BadRequest("Username is taken!");

            using var hmac = new HMACSHA512();

            AppUser user = new()
            {
                Username = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")] // POST: api/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            AppUser user = await _userRepository.GetUserByUsernameAsync(loginDto.Username.ToLower().Trim());

            if (user == null)
                return Unauthorized("Username or Password is incorrect.");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            int computedHashLen = computedHash.Length;

            for (int i = 0; i < computedHashLen; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Username or Password is incorrect.");
            }

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url
            };
        }

        private async Task<bool> IsUserExisted(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username.ToLower().Trim()) != null;
        }
    }
}