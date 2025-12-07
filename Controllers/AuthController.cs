using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthApp.Entities;
using AuthApp.Model;
using AuthApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController (IAuthService authService) : ControllerBase
    {
          
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await authService.RegisterAsync(request);

            if (user == null)
                return BadRequest("User already exists");

            return Ok(user);
        }


        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var result = await authService.LoginAsync(request);
            
            if (result == null) return BadRequest("Invalid username/password");

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokensAsync(request);
            if (result is null || result.AccessToken is null || result.RefreshToken is null)
                return Unauthorized("Invalid refresh token.");

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedEndPoint()
        {
            return Ok("You are authenticated (All Authenticated)");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin-Role")]
        public IActionResult AdminOnly()
        {
            return Ok("You are authenticated (Admin)");
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("SuperAdmin-Role")]
        public IActionResult SuperAdminOnly()
        {
            return Ok("You are authenticated (SuperAdmin)");
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("UserAndAdmin-Role")]
        public IActionResult UserOnly()
        {
            return Ok("You are authenticated (User And Admin)");
        }

    }
}
