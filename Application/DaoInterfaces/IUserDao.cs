using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User> GetByIdAsync(int authorId);
    Task<User> GetByUsernameAsync(string userName);
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
}