using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CumulativeCountReport.Models
{
    [Table("Equipment")]
    public class Equipment
    {
        [Key]
        [Column("EOPID")]
        [StringLength(50)]
        public string EopId { get; set; } = string.Empty;

        [Column("AREA")]
        [StringLength(50)]
        public string? Area { get; set; }

        [Column("GROUP")]
        [StringLength(50)]
        public string? Group { get; set; }

        [Column("ORDER")]
        public int? Order { get; set; }
    }
}
