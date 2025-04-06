namespace Domain.DTOs.Comments;

public class CreateDTO
{

    public int UserId { get; set; }

    public int PostId { get; set; }

    public string Text { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

}
