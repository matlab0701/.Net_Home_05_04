namespace Domain.DTOs.Posts;

public class CreateDto
{
     public int UserId { get; set; } 
    
    public string Username { get; set; }
    
    public string Content { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
}
