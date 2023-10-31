using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        User toCreate = new User
        {
            UserName = dto.UserName,
            Password = dto.Password
        };

        User created = await userDao.CreateAsync(toCreate);

        return created;
    }

    private static void ValidateData(UserCreationDto userToCreate)
    {
        string alias = userToCreate.UserName;
        string password = userToCreate.Password;

        if (alias.Length < 3)
            throw new Exception("Username must be at least 3 characters long.");

        if (alias.Length > 15)
            throw new Exception("Username must be 15 or less characters long.");
        if (password.Equals(""))
        {
            throw new Exception("Password cannot be empty!");
        }
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto parameters)
    {
        return userDao.GetAsync(parameters);
    }

    public Task<User> GetByUsernameAsync(string username)
    {
        return userDao.GetByUsernameAsync(username);
    }
}