using API.Data.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _auth.LoginAsync(request);
            if (!result.Success)
                return Unauthorized(new { message = result.Message });

            return Ok(new { token = result.Token });
        }

        [HttpGet("verify")]
        public IActionResult Verify()
        {
            return Ok(new { message = "API is up & running" });
        }
    }
}

