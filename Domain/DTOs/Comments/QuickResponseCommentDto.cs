namespace Domain.DTOs.Comments;

public class QuickResponseCommentDto
{
     public string Text { get; set; }
    public string Username { get; set; }
    public int PostId { get; set; }
    public DateTimeOffset TimeDifference { get; set; }
}
