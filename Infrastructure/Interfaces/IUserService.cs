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
    Task<Response<List<UserDTO>>> GetUserNewRegistr();
    Task<Response<List<HighInteractionUserDto>>> HighInteraction();
    Task<Response<List<RecentlyActiveUserDto>>> RecentlyActiveUser();
    Task<Response<List<TopCreatorDto>>> TopCreator();
    Task<Response<List<UserRecentPostDto>>> UserRecentPost(int id);
    Task<Response<List<ActivitySummaryDto>>> ActivitySummary(int id);

}
