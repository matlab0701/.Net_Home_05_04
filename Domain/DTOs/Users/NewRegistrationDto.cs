namespace Domain.DTOs.Users;

public class NewRegistrationDto
{
    public string Username { get; set; }
    public string Emai { get; set; }
    public DateTimeOffset JoinDate { get; set; }
}
