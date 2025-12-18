using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileShareApp.Models
{
    [Table("users")]
    [Index("Email", Name = "UQ_Users_Email", IsUnique = true)]
    public partial class User
    {
        [Key]
        [Column("UserID")]
        public int UserId { get; set; }

        [StringLength(50)]
        public string Username { get; set; } = null!;

        [StringLength(100)]
        public string Email { get; set; } = null!;

        [StringLength(255)]
        public string PasswordHash { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<SharedFile> Files { get; set; } = new List<SharedFile>();
    }
}
