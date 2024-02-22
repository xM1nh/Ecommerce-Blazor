using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SharedLibrary.Models;

[Table("Users")]
public class User
{
    [Key]
    public int Id { get; set; }

    [Required, NotNull]
    public string? Name { get; set; }

    [Required, NotNull]
    public string? Email { get; set; }

    [Required, NotNull]
    public string? Password { get; set; }
}
