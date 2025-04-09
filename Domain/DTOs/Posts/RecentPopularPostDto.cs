namespace Domain.DTOs.Posts;

public class RecentPopularPostDto
{
    public string Content { get; set; }
    public string Username { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public int CommentCount { get; set; }
}