using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogWebApi.DTO;
using BlogWebApi.Helpers;
using BlogWebApi.Models;
using BlogWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(
            IConfiguration configuration,
            IUserService userService,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // ✅ Register API
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var result = await _userService.RegisterUserAsync(dto);
                if (result.Status)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var error = ResultHelper.Failure("An unexpected error occurred.", ex.Message);
                return StatusCode(500, error);
            }
        }

        // ✅ Login API
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {

            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null)
                    return Unauthorized(new { Status = false, Message = "Invalid credentials" });

                var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
                if (!result.Succeeded)
                    return Unauthorized(new { Status = false, Message = "Invalid credentials" });

                var token = GenerateJwtToken(user);
                return Ok(new { Status = true, Data = "Name: " + user.FullName + " Email:" + user.Email, Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = false, Message = "Login failed", Error = ex.Message });
            }
        }

        // ✅ JWT Token Generator
        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.FullName ?? user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["Expire"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        [Authorize]
        [HttpGet("CheckUserPresentByEmail")]
        public async Task<IActionResult> CheckUserPresentByEmail([FromQuery] string email)
        {
            try
            {
                var result = await _userManager.FindByEmailAsync(email);
                if (result != null)
                    return Ok(new { Status = true, Message = result });

                return NotFound(new { Status = false, Message = "Email Not Found.." });
            }
            catch (Exception ex)
            {
                var error = ResultHelper.Failure("An unexpected error occurred.", ex.Message);
                return StatusCode(500, error);
            }
        }


    }
}
