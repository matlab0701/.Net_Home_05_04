using System.Net;
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

}
