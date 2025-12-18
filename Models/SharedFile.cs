using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FileShareApp.Models
{
    [Table("files")]
    [Index("ShareToken", Name = "UQ_Files_ShareToken", IsUnique = true)]
    [Index("UserId", Name = "idx_files_userid")]
    public partial class SharedFile
    {
        [Key]
        [Column("FileID")]
        public int FileId { get; set; }

        [Column("UserID")]
        public int UserId { get; set; }

        [StringLength(255)]
        public string OriginalName { get; set; } = null!;

        [StringLength(255)]
        public string StoredFileName { get; set; } = null!;

        [StringLength(100)]
        public string? ContentType { get; set; }

        public long FileSize { get; set; }

        [StringLength(20)]
        public string FileExtension { get; set; } = null!;

        [StringLength(64)]
        public string ShareToken { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime? UploadDate { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Files")]
        public virtual User User { get; set; } = null!;
    }
}
