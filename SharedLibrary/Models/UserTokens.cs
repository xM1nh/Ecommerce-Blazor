using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.Models;

[Table("UserTokens")]
internal class UserTokens
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }

}
