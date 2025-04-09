using Domain.Entites;

namespace Domain.DTOs.Users;

public class ActivitySummaryDto
{
    public  string Username { get; set; }
    public List<Post> Posts{get;set;}
    public List<Comment> Comments{get;set;}


}
