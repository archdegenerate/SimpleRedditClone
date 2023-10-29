using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto postToCreate);
    Task<IEnumerable<Post>> GetAsync();
    Task<Post?> GetByIdAsync(int postId);
}