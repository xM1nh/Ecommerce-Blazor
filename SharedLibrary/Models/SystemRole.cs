using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.Models;

[Table("SystemRoles")]
public class SystemRole
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
}
