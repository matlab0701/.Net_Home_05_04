namespace Domain.DTOs.Posts;

public class LatestPostsDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string Username { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
