using Domain.DTOs.Users;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<Response<List<UserDTO>>> GetAllAsync();
    Task<Response<UserDTO>> CreateAsync(CreateDTO request);
    Task<Response<UserDTO>> GetByIdAsync(int Id);
    Task<Response<UserDTO>> UpdateAsync(int Id, UpdateDTO request);
    Task<Response<string>> DeleteAsync(int Id);
}
