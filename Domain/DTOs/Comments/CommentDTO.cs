namespace Domain.DTOs.Comments;

public class CommentDTO
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public string Username { get; set; }
  public int PostId { get; set; }
  public string Text { get; set; }
  public DateTimeOffset CreatedAt { get; set; }
}
