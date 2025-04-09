namespace Domain.DTOs.Comments;

public class RecentCommentDto
{
    public string Text { get; set; }
    public string Username { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
