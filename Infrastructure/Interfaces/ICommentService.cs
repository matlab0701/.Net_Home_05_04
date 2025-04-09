using Domain.DTOs.Comments;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICommentService
{
    Task<Response<List<CommentDTO>>> GetAllAsync();
    Task<Response<List<RecentCommentDto>>> RecentComment();
    Task<Response<List<PostRecentCommentsDto>>> PostRecentComments(int commentId);
    Task<Response<List<LongTextCommentDto>>> LongTextComment();
    Task<Response<List<QuickResponseCommentDto>>> QuickResponseComment();
    Task<Response<CommentDTO>> CreateAsync(CreateDTO request);
    Task<Response<CommentDTO>> GetByIdAsync(int Id);
    Task<Response<CommentDTO>> UpdateAsync(int Id, UpdateDTO request);
    Task<Response<string>> DeleteAsync(int Id);
}
