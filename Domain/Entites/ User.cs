using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [StringLength(200)]
    public string Bio { get; set; }

    // navigation
    public List<Post> Posts { get; set; }
    public List<Comment> Comments { get; set; }







}
