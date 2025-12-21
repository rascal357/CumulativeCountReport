using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CumulativeCountReport.Models
{
    [Table("Wafer_Count_History")]
    public class WaferCountHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("EOPID")]
        [StringLength(50)]
        public string EopId { get; set; } = string.Empty;

        [Required]
        [Column("TESTOPNO")]
        [StringLength(50)]
        public string TestOpNo { get; set; } = string.Empty;

        [Required]
        [Column("DATE")]
        public DateTime Date { get; set; }

        [Required]
        [Column("ITEMPROMPT")]
        [StringLength(100)]
        public string ItemPrompt { get; set; } = string.Empty;

        [Column("VALUE")]
        public decimal Value { get; set; }
    }
}
