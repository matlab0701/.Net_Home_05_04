namespace Domain.DTOs.Users;

public class RecentlyActiveUserDto
{ 
    public string Username { get; set; }
    public DateTimeOffset LastPostDate { get; set; }
    public int PostCount { get; set; }

}
