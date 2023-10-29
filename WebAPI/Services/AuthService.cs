using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using FileData.DAOs;
using Domain.Models;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserLogic userLogic;

    public AuthService(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }

    public async Task<User> GetUser(string username, string password)
    {
        SearchUserParametersDto parameters = new(username);
        IEnumerable<User> users = await userLogic.GetAsync(parameters);
        User? user = users.FirstOrDefault(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        if (user != null)
        {
            return user;
        }
        else
        {
            throw new Exception("List of users empty.");
        }
        
    }
    
    public async Task<User> ValidateUser(User user)
    {
        SearchUserParametersDto parameters = new(user.UserName);
        IEnumerable<User> users = await userLogic.GetAsync(parameters);
        User? existingUser = users.FirstOrDefault(u => 
            u.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase));
        Console.WriteLine($"Username: {existingUser.UserName}, Password: {existingUser.Password}");
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(user.Password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }
    

    public Task RegisterUser(User user)
    {
        if (string.IsNullOrEmpty(user.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }

        UserCreationDto dto = new UserCreationDto(user.UserName, user.Password);

        userLogic.CreateAsync(dto);
        
        return Task.CompletedTask;
    }
}
    