namespace Domain.DTOs.Users;

public class UserDTO
{

    public int Id { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public int PostCount { get; set; }
    public DateTimeOffset JoinDate { get; set; }
}
