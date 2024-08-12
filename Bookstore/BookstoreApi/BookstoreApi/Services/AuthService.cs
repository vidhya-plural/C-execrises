using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookstoreApi.Data;
using BookstoreApi.Models;

public class AuthService : IAuthService
{
    private readonly BookstoreContext _context;

    public AuthService(BookstoreContext context)
    {
        _context = context;
    }

    public async Task<User> AuthenticateUser(string username, string password)
    {
        var user = await _context.Users
            .Where(u => u.Username == username && u.Password == password)
            .SingleOrDefaultAsync();

        return user; // Ensure this matches the Task<User> return type
    }
}
