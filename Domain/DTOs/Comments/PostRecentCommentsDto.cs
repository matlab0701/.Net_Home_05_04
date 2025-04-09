namespace Domain.DTOs.Comments;

public class PostRecentCommentsDto
{
     public string Text { get; set; }
    public string Username { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
