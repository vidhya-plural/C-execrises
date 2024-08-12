using System.Threading.Tasks;
using BookstoreApi.Models;

public interface IAuthService
{
    Task<User> AuthenticateUser(string username, string password);
}
