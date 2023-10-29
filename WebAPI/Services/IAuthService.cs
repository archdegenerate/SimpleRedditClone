using Domain.Models;

namespace WebAPI.Services;

public interface IAuthService
{
    Task<User> GetUser(string username, string password);
    Task RegisterUser(User user);
    Task<User> ValidateUser(User user);
}