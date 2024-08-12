using Microsoft.AspNetCore.Mvc;
using BookstoreApi.Data;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly BookstoreContext _context;
    private readonly IAuthService _authService;

    public AuthController(BookstoreContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDto)
    {
        var user = await _authService.AuthenticateUser(loginDto.Username, loginDto.Password);

        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }

        // Return a success message or token if using JWT
        return Ok("Login successful");
    }
}
