using Microsoft.AspNetCore.Mvc;
using BookstoreApi.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly BookstoreContext _context;
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(BookstoreContext context, IAuthService authService, ILogger<AuthController> logger)
    {
        _context = context;
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDto)
    {
        _logger.LogInformation("Login attempt for user: {Username}", loginDto.Username);

        var user = await _authService.AuthenticateUser(loginDto.Username, loginDto.Password);

        if (user == null)
        {
            _logger.LogWarning("Login failed for user: {Username}", loginDto.Username);
            return Unauthorized("Invalid username or password");
        }

        _logger.LogInformation("Login successful for user: {Username}", loginDto.Username);
        return Ok("Login successful");
    }
}
