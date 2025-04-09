using System.Net;

using Domain.DTOs;
using Domain.DTOs.Users;
using Domain.Entites;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService(DataContext context) : IUserService
{
    public async Task<Response<UserDTO>> CreateAsync(CreateDTO request)
    {
        var user = new User()
        {
            Username = request.Username,
            Email = request.Email,
            Bio = request.Bio,
        };
        await context.Users.AddAsync(user);
        var result = await context.SaveChangesAsync();

        var UserDTO = new UserDTO()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            PostCount = 0
        };

        return result == 0 ?
        new Response<UserDTO>(HttpStatusCode.BadRequest, "User not add")
        : new Response<UserDTO>(UserDTO);

    }

    public async Task<Response<string>> DeleteAsync(int Id)
    {
        var user = await context.Users.FindAsync(Id);
        if (user == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "User can`t found");
        }
        context.Remove(user);
        var result = await context.SaveChangesAsync();
        return result == 0 ?
        new Response<string>(HttpStatusCode.BadRequest, "User don`t remove")
        : new Response<string>("user delete successfuly");
    }

    public async Task<Response<List<UserDTO>>> GetAllAsync()
    {
        var users = await context.Users.ToListAsync();
        var data = users.Select(u => new UserDTO()
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            PostCount = u.Posts.Count + 1
        }).ToList();
        return new Response<List<UserDTO>>(data);
    }

    public async Task<Response<UserDTO>> GetByIdAsync(int Id)
    {
        var user = await context.Users.FindAsync(Id);
        if (user == null)
        {
            return new Response<UserDTO>(HttpStatusCode.BadRequest, "User can`t found");

        }

        var userdto = new UserDTO()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            PostCount = user.Posts.Count
        };
        return new Response<UserDTO>(userdto);
    }

    public async Task<Response<UserDTO>> UpdateAsync(int Id, UpdateDTO request)
    {
        var user = await context.Users.FindAsync(Id);
        if (user == null)
        {
            return new Response<UserDTO>(HttpStatusCode.BadRequest, "User can`t found");
        }
        user.Username = request.Username;
        user.Email = request.Email;
        user.Bio = request.Bio;
        var result = await context.SaveChangesAsync();
        var userdto = new UserDTO()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            PostCount = user.Posts.Count(p => p.UserId == user.Id)
        };
        return result == 0 ?
        new Response<UserDTO>(HttpStatusCode.BadRequest, "User can`t updated")
        : new Response<UserDTO>(userdto);
    }

    public async Task<Response<List<UserPostsDto>>> GetuserPst(int userId)
    {
        var posts = await context.Posts
               .Where(p => p.UserId == userId)
               .Select(p => new UserPostsDto
               {
                   Content = p.Content,
                   CreatedAt = p.CreatedAt,
                   Username = p.User.Username
               })
               .ToListAsync();

        return new Response<List<UserPostsDto>>(posts);
    }
    public async Task<Response<List<UserDTO>>> GetUserNewRegistr()
    {
        var TwoWeek = DateTime.UtcNow.AddDays(-14);
        var users = await context.Users
        .Where(u => u.JoinDate >= TwoWeek).ToListAsync();

        var data = users
        .Select(u => new UserDTO
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            JoinDate = u.JoinDate,
        }).ToList();
        return new Response<List<UserDTO>>(data);

    }

    public async Task<Response<List<HighInteractionUserDto>>> HighInteraction()
    {
        var users = await context.Users
            .Include(u => u.Posts)
            .ThenInclude(post => post.Comments)
            .Where(u => u.Posts.Count > 0).ToListAsync();

        var list = new List<HighInteractionUserDto>();

        foreach (var user in users) // {1 => 1, umar;  2 => 3, komron}
        {
            var postCount = user.Posts.Count;
            var commentSum = user.Posts.Sum(post => post.Comments.Count);

            var averageCommentsPerPost = commentSum / (double)postCount;

            if (averageCommentsPerPost <= 5) continue;

            var highInteractionUserDto = new HighInteractionUserDto()
            {
                PostCount = postCount,
                Username = user.Username,
                AvgCommentsPerPost = averageCommentsPerPost,
            };

            list.Add(highInteractionUserDto);
        }

        return new Response<List<HighInteractionUserDto>>(list);
    }

    public async Task<Response<List<RecentlyActiveUserDto>>> RecentlyActiveUser()
    {
        var now = DateTime.Now.AddDays(-7);
        var users = await context.Users
        .Include(u => u.Posts)
        .Where(u => u.Posts.Any(p => p.CreatedAt >= now)).ToListAsync();

        var data = users
        .Select(u => new RecentlyActiveUserDto
        {
            Username = u.Username,
            LastPostDate = u.Posts
            .Where(p => p.CreatedAt >= now)
            .Max(p => p.CreatedAt),
            PostCount = u.Posts.Count(p => p.CreatedAt >= now)
        }).ToList();
        return new Response<List<RecentlyActiveUserDto>>(data);
    }

    public async Task<Response<List<TopCreatorDto>>> TopCreator()
    {
        var users = await context.Users
        .Where(u => u.Posts.Any()).ToListAsync();

        var data = users
        .Select(u => new TopCreatorDto()
        {
            Username = u.Username,
            PostCount = u.Posts.Count

        }).ToList();
        return new Response<List<TopCreatorDto>>(data);
    }

    public async Task<Response<List<UserRecentPostDto>>> UserRecentPost(int id)
    {
        var data = await context.Posts
            .Where(p => p.UserId == id)
            .OrderByDescending(p => p.CreatedAt)
            .Take(5)
            .Select(p => new UserRecentPostDto
            {
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                Username = p.User.Username
            })
            .ToListAsync();

        return new Response<List<UserRecentPostDto>>(data);
    }


    public async Task<Response<ActivitySummaryDto>> GetUserActivitySummary(int id)
    {
        var user = await context.Users
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        if (user == null)
            return new Response<ActivitySummaryDto>(HttpStatusCode.BadRequest,"User not found");

        var posts = await context.Posts
            .Where(p => p.UserId == id)
            .OrderByDescending(p => p.CreatedAt)
            .Take(3)
            .Select(p => new PostDTO
            {
                Content = p.Content,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync();

        var comments = await context.Comments
            .Where(c => c.UserId == id)
            .OrderByDescending(c => c.CreatedAt)
            .Take(3)
            .Select(c => new CommentDTO
            {
                Text = c.Text,
                PostId = c.PostId
            })
            .ToListAsync();

        var result = new ActivitySummaryDto()
        {
            Username = user.Username,
            Posts=user.Posts,
            Comments=user.Comments
        };

        return new Response<ActivitySummaryDto>(result);
    }
}
