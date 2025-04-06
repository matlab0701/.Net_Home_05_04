using Domain.DTOs.Comments;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController(ICommentService commentService)
{
       [HttpGet]
    public async Task<Response<List<CommentDTO>>> GetAllAsync()
    {
        var users = await commentService.GetAllAsync();
        return users;
    }

    [HttpGet("{id:int}")]
    public async Task<Response<CommentDTO>> GetByIdAsync(int id)
    {
        var comment = await commentService.GetByIdAsync(id);
        return comment;
    }

    [HttpPost]
    public async Task<Response<CommentDTO>> CreateAsync(CreateDTO request)
    {
        var response = await commentService.CreateAsync(request);
        return response;
    }

    [HttpPut("{id:int}")]
    public async Task<Response<CommentDTO>> UpdateAsync(int id, UpdateDTO request)
    {
        var response = await commentService.UpdateAsync(id, request);
        return response;
    }

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var response = await commentService.DeleteAsync(id);
        return response;
    }
}
