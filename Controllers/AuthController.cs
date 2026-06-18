using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotesAPI.DTOs;
using NotesAPI.Models;
using NotesAPI.Repositories;
using NotesAPI.Services;

namespace NotesAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService jwtService;
        private readonly IUnitOfwork unitOfwork;
        private readonly PasswordHasher<User> passHasher;

        public AuthController(IJwtService jwtService,IUnitOfwork unitOfwork,PasswordHasher<User> passHasher)
        {
            this.jwtService = jwtService;
            this.unitOfwork = unitOfwork;
            this.passHasher = passHasher;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var exiting = await unitOfwork.Users.GetByUserNameAsync(dto.Username);
            if (exiting != null)
                return BadRequest(new {message = "Username is already taken." });


            
            var user = new User()
            {
                UserName = dto.Username
            };
            user.PasswordHash = passHasher.HashPassword(user, dto.Password);
            var token = jwtService.GenerateToken(user);

            await unitOfwork.Users.AddAsync(user);
            await unitOfwork.CompleteAsync();

            return Ok(new AuthResponseDto
            {
                Token = token,
                Username = user.UserName
            });

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await unitOfwork.Users.GetByUserNameAsync(dto.Username);
            if (user == null)
                return Unauthorized(new { message = "Invalid username or password" });

            var result = passHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized(new { message = "Invalid username or password" });

            var token = jwtService.GenerateToken(user);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Username = user.UserName
            });

        }
    }
}

