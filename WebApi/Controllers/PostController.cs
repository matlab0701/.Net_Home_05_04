using Domain.DTOs.Posts;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController(IPostService postService)
{
    [HttpGet]
    public async Task<Response<List<PostDTO>>> GetAllAsync()
    {
        var users = await postService.GetAllAsync();
        return users;
    }

    [HttpGet("{id:int}")]
    public async Task<Response<PostDTO>> GetByIdAsync(int id)
    {
        var post = await postService.GetByIdAsync(id);
        return post;
    }

    [HttpPost]
    public async Task<Response<PostDTO>> CreateAsync(CreateDto request)
    {
        var response = await postService.CreateAsync(request);
        return response;
    }

    [HttpPut("{id:int}")]
    public async Task<Response<PostDTO>> UpdateAsync(int id, UpdateDto request)
    {
        var response = await postService.UpdateAsync(id, request);
        return response;
    }

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var response = await postService.DeleteAsync(id);
        return response;
    }
}
