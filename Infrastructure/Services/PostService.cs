using System.Net;
using Domain.DTOs.Posts;
using Domain.Entites;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class PostService(DataContext context) : IPostService
{
    public async Task<Response<PostDTO>> CreateAsync(CreateDto request)
    {
        var post = new Post()
        {
            UserId = request.UserId,
            Content = request.Content,
            CreatedAt = request.CreatedAt
        };
        await context.Posts.AddAsync(post);
        var result = await context.SaveChangesAsync();
        var postDto = new PostDTO()
        {
            Id = post.Id,
            UserId = post.UserId,
            Content = post.Content,
            CreatedAt = post.CreatedAt,
            CommentCount = 0

        };
        return result == 0 ?
        new Response<PostDTO>(HttpStatusCode.BadRequest, "post can`t add")
        : new Response<PostDTO>(postDto);

    }

    public async Task<Response<string>> DeleteAsync(int Id)
    {
        var post = await context.Posts.FindAsync(Id);
        if (post == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "post can`t found");
        }
        context.Remove(post);
        var result = await context.SaveChangesAsync();
        return result == 0 ?
        new Response<string>(HttpStatusCode.BadRequest, "Post not deleted")
        : new Response<string>("post deleted successfuly");
    }

    public async Task<Response<List<PostDTO>>> GetAllAsync()
    {
        var posts = await context.Posts.ToListAsync();

        var data = posts.Select(p => new PostDTO()
        {
            Id = p.Id,
            UserId = p.UserId,
            Content = p.Content,
            CreatedAt = p.CreatedAt,
            CommentCount = p.Comments.Count
        }).ToList();
        return new Response<List<PostDTO>>(data);
    }

    public async Task<Response<PostDTO>> GetByIdAsync(int Id)
    {
        var post = await context.Posts.FindAsync(Id);
        if (post == null)
        {
            return new Response<PostDTO>(HttpStatusCode.BadRequest, "post can`t found");
        }

        var postDto = new PostDTO()
        {
            Id = post.Id,
            UserId = post.UserId,
            Content = post.Content,
            CreatedAt = post.CreatedAt,
            CommentCount = post.Comments.Count
        };
        return new Response<PostDTO>(postDto);



    }

    public async Task<Response<PostDTO>> UpdateAsync(int Id, UpdateDto request)
    {
        var post = await context.Posts.FindAsync(Id);
        if (post == null)
        {
            return new Response<PostDTO>(HttpStatusCode.BadRequest, "post can`t found");
        }
        post.UserId = request.UserId;
        post.Content = request.Content;
        post.CreatedAt = request.CreatedAt;

        var result = await context.SaveChangesAsync();

        var postDto = new PostDTO()
        {
            Id = post.Id,
            UserId = post.UserId,
            Content = post.Content,
            CreatedAt = post.CreatedAt,
            CommentCount = post.Comments.Count(c => c.PostId == post.Id)
        };
        return result == 0 ?
        new Response<PostDTO>(HttpStatusCode.BadRequest, "Posrs can`t updated")
        : new Response<PostDTO>(postDto);
    }

}
