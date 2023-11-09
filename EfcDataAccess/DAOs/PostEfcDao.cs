using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{
    private readonly PostContext context;

    public PostEfcDao(PostContext context)
    {
        this.context = context;
    }
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Post>> GetAsync()
    {
        IQueryable<Post> query = context.Posts.Include(p => p.Author).AsQueryable();

        IEnumerable<Post> posts = await query.ToListAsync();

        return posts;
    }

    public async Task<Post?> GetPostByIdAsync(int postId)
    {
        Post? post = await context.Posts.Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == postId);
        return post;
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await GetPostByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} not found.");
        }

        context.Posts.Remove(existing);
        await context.SaveChangesAsync();
    }
}