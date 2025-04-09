namespace Domain.DTOs.Users;

public class UserRecentPostDto
{
    public  string Content { get; set; }
    public  string Username  { get; set; }
    public  DateTimeOffset CreatedAt { get; set; }

}
