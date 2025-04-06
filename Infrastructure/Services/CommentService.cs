using System.Net;
using Domain.DTOs.Comments;
using Domain.Entites;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CommentService(DataContext context) : ICommentService
{
    public async Task<Response<CommentDTO>> CreateAsync(CreateDTO request)
    {
        var comment = new Comment()
        {
            UserId = request.UserId,
            PostId = request.PostId,
            Text = request.Text,
            CreatedAt = request.CreatedAt
        };
        await context.Comments.AddAsync(comment);
        var result = await context.SaveChangesAsync();
        var commentDto = new CommentDTO()
        {
            Id = comment.Id,
            PostId = comment.PostId,
            UserId = comment.UserId,
            Text = comment.Text,
            CreatedAt = comment.CreatedAt
        };
        return result == 0 ?
        new Response<CommentDTO>(HttpStatusCode.BadRequest, "Comment not add")
        : new Response<CommentDTO>(commentDto);
    }

    public async Task<Response<string>> DeleteAsync(int Id)
    {
        var comment = await context.Comments.FindAsync(Id);
        if (comment == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Comment not found");
        }
        context.Remove(comment);
        var result = await context.SaveChangesAsync();
        return result > 0
        ? new Response<string>("Comment deleted succesfuly ")
        : new Response<string>(HttpStatusCode.BadRequest, "Comment not delet");
    }

    public async Task<Response<List<CommentDTO>>> GetAllAsync()
    {
        var comments = await context.Comments.ToListAsync();
        var data = comments.Select(c => new CommentDTO()
        {
            Id = c.Id,
            UserId = c.PostId,
            PostId = c.PostId,
            Text = c.Text,
            CreatedAt = c.CreatedAt
        }).ToList();

        return new Response<List<CommentDTO>>(data);
    }

    public async Task<Response<CommentDTO>> GetByIdAsync(int Id)
    {
        var comment = await context.Comments.FindAsync(Id);
        if (comment == null)
        {
            return new Response<CommentDTO>(HttpStatusCode.BadRequest, "Comment not found");
        }
        var commentDto = new CommentDTO()
        {
            Id = comment.Id,
            UserId = comment.PostId,
            PostId = comment.PostId,
            Text = comment.Text,
            CreatedAt = comment.CreatedAt
        };
        return new Response<CommentDTO>(commentDto);
    }

    public async Task<Response<CommentDTO>> UpdateAsync(int Id, UpdateDTO request)
    {
        var comment = await context.Comments.FindAsync(Id);
        if (comment == null)
        {
            return new Response<CommentDTO>(HttpStatusCode.BadRequest, "Comment not found");
        }

        comment.PostId = request.PostId;
        comment.UserId = request.UserId;
        comment.Text = request.Text;
        comment.CreatedAt = request.CreatedAt;
        var result = await context.SaveChangesAsync();

        var commentDto = new CommentDTO()
        {
            Id = comment.Id,
            UserId = comment.PostId,
            PostId = comment.PostId,
            Text = comment.Text,
            CreatedAt = comment.CreatedAt
        };

        return result == 0 ?
        new Response<CommentDTO>(HttpStatusCode.BadRequest, "Comment dont be updated ")
        : new Response<CommentDTO>(commentDto);

    }

}
