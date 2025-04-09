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

    public async Task<Response<List<RecentCommentDto>>> RecentComment()
    {
        var comment = await context.Comments
        .Select(c => new RecentCommentDto()
        {
            Text = c.Text,
            Username = c.User.Username,
            CreatedAt = c.CreatedAt

        })
        .OrderByDescending(c => c.CreatedAt)
        .Take(5)
        .ToListAsync();

        return new Response<List<RecentCommentDto>>(comment);
    }

    public async Task<Response<List<PostRecentCommentsDto>>> PostRecentComments(int commentId)
    {
        var comment = await context.Comments
       .Where(c => c.Id == commentId)
        .Select(c => new PostRecentCommentsDto()
        {
            Text = c.Text,
            Username = c.User.Username,
            CreatedAt = c.CreatedAt

        })
        .OrderByDescending(c => c.CreatedAt)
        .Take(5)
        .ToListAsync();
        return new Response<List<PostRecentCommentsDto>>(comment);
    }

    public async Task<Response<List<LongTextCommentDto>>> LongTextComment()
    {
        var comment = await context.Comments
        .Where(c => c.Text.Length > 200)
        .Select(c => new LongTextCommentDto()
        {
            Text = c.Text,
            Username = c.User.Username,
            TextLength = c.Text.Length
        }).ToListAsync();
        return new Response<List<LongTextCommentDto>>(comment);
    }

    public async Task<Response<List<QuickResponseCommentDto>>> QuickResponseComment()
    {
        var minute = DateTimeOffset.Now.AddMinutes(-15);
        var comment = await context.Comments
        .Where(c => c.CreatedAt >= minute)
        .Select(c => new QuickResponseCommentDto()
        {
            Text = c.Text,
            Username = c.User.Username,
            PostId = c.PostId,
            TimeDifference = c.CreatedAt
        }).ToListAsync();
        return new Response<List<QuickResponseCommentDto>>(comment);
    }

    public async Task<Response<List<TopCommenterDto>>> GetTopCommenters()
    {
        var users = await context.Users
            .Where(u => u.Comments.Any())
            .OrderByDescending(u => u.Comments.Count)
            .Take(5)
            .Select(u => new TopCommenterDto
            {
                Username = u.Username,
                CommentCount = u.Comments.Count
            })
            .ToListAsync();

        return new Response<List<TopCommenterDto>>(users);
    }

}
