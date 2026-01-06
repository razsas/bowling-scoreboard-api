using bowlingApp.Data;
using bowlingApp.Models;
using bowlingApp.Models.Dto;
using bowlingApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bowlingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(ApplicationDbContext context, ITokenService tokenService) : ControllerBase
    {
        [HttpPost("register")] 
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            var user = new User
            {
                Username = registerDto.Username.ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };

            var token = tokenService.CreateToken(user);
            if (token == null) return Problem("Token generation failed.", statusCode: 500);

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.Username,
                Token = token
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username");

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash)) return Unauthorized("Invalid password");

            var token = tokenService.CreateToken(user);
            if (token == null) return Problem("Token generation failed.", statusCode: 500);

            return new UserDto
            {
                Username = user.Username,
                Token = token
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }
    }
}
