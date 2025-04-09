namespace Domain.DTOs.Users;

public class HighInteractionUserDto
{
    public string Username { get; set; }
    public int PostCount { get; set; }
    public double AvgCommentsPerPost { get; set; }
}
