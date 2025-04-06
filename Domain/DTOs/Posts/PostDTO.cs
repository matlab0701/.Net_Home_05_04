namespace Domain.DTOs.Posts;

public class PostDTO
{
     public int Id { get; set; }
    
    public int UserId { get; set; } 
    
    public string Username { get; set; }
    
    public string Content { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public int CommentCount { get; set; }
}
