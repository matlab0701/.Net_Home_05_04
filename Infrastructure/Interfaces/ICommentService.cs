using Domain.DTOs.Comments;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICommentService
{
    Task<Response<List<CommentDTO>>> GetAllAsync();
    Task<Response<CommentDTO>> CreateAsync(CreateDTO request);
    Task<Response<CommentDTO>> GetByIdAsync(int Id);
    Task<Response<CommentDTO>> UpdateAsync(int Id, UpdateDTO request);
    Task<Response<string>> DeleteAsync(int Id);
}
