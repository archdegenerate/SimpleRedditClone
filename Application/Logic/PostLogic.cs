using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.VisualBasic.CompilerServices;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.AuthorId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.AuthorId} was not found.");
        }

        Post post = new Post(user.Id, dto.Title, dto.Content);
        ValidatePost(post);
        Post created = await postDao.CreateAsync(post);
        return created;
    }

    private void ValidatePost(Post dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(dto.Content)) throw new Exception("Post cannot be empty.");
    }

    public async Task<IEnumerable<Post>> GetAsync()
    {
        return await postDao.GetAsync();
    }

    public Task<Post?> GetByIdAsync(int postId)
    {
        return postDao.GetPostByIdAsync(postId);
    }
    
}