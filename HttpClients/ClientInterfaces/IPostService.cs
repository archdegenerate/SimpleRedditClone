using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostCreationDto dto);
    Task<ICollection<Post>> GetAsync(User? author, string? title, string? content);
    Task<Post> GetByIdAsync(int postId);
}