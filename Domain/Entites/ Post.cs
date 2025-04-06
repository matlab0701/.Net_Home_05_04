using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entites;

public class Post
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    [StringLength(500)]
    public string Content { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTime.Now;

    // navigations
    public User User { get; set; }
    public List<Comment> Comments { get; set; }





}
