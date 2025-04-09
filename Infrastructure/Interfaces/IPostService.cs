using Domain.DTOs.Posts;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IPostService
{
    Task<Response<List<PostDTO>>> GetAllAsync();
    Task<Response<PostDTO>> CreateAsync(CreateDto request);
    Task<Response<PostDTO>> GetByIdAsync(int Id);
    Task<Response<PostDTO>> UpdateAsync(int Id, UpdateDto request);
    Task<Response<string>> DeleteAsync(int Id);

    Task<Response<List<HighCommentPostDto>>> HighCommentPost();

}
