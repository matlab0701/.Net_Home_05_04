using Domain.DTOs.Users;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService)
{
    [HttpGet]
    public async Task<Response<List<UserDTO>>> GetAllAsync()
    {
        var users = await userService.GetAllAsync();
        return users;
    }

    [HttpGet("{id:int}")]
    public async Task<Response<UserDTO>> GetByIdAsync(int id)
    {
        var user = await userService.GetByIdAsync(id);
        return user;
    }

    [HttpPost]
    public async Task<Response<UserDTO>> CreateAsync(CreateDTO request)
    {
        var response = await userService.CreateAsync(request);
        return response;
    }

    [HttpPut("{id:int}")]
    public async Task<Response<UserDTO>> UpdateAsync(int id, UpdateDTO request)
    {
        var response = await userService.UpdateAsync(id, request);
        return response;
    }

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var response = await userService.DeleteAsync(id);
        return response;
    }
}
