using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required, StringLength(100), EmailAddress]
    public string Email { get; set; }
    public string Content { get; set; }


    [StringLength(200)]
    public string Bio { get; set; }
    public DateTimeOffset LastPostDate { get; set; }
    public DateTimeOffset JoinDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }



    // navigation
    public List<Post> Posts { get; set; }
    public List<Comment> Comments { get; set; }







}
