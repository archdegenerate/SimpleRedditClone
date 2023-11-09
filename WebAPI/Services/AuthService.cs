using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
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
        User? user = await userLogic.GetByUsernameAsync(username);
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
        User? existingUser = await userLogic.GetByUsernameAsync(user.UserName);
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
    