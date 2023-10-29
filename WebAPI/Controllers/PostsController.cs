using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]


public class PostsController : ControllerBase
{
    private readonly IPostLogic postLogic;

    public PostsController(IPostLogic postLogic)
    {
        this.postLogic = postLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync([FromBody]PostCreationDto dto)
    {
        try
        {
            Post post = await postLogic.CreateAsync(dto);
            return Created($"/posts/{post.Id}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Post>>> GetAsync()
    {
        try
        {
            IEnumerable<Post> posts = await postLogic.GetAsync();
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{postId}")]
    public async Task<ActionResult<Post>> GetPostByIdAsync(int postId)
    {
        try
        {
            Post post = await postLogic.GetByIdAsync(postId);
            return Ok(post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}