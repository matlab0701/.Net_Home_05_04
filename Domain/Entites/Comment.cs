using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entites;

public class Comment
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    [ForeignKey("Post")]
    public int PostId { get; set; }

    [Required]
    [StringLength(300)]
    public string Text { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    // navigations
    public Post Post { get; set; }
    public User User { get; set; }



}
