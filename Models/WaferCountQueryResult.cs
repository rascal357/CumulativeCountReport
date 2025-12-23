using System;

namespace CumulativeCountReport.Models
{
    public class WaferCountQueryResult
    {
        public string EopId { get; set; } = string.Empty;
        public string TestOpNo { get; set; } = string.Empty;
        public string ItemPrompt { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Value { get; set; }
    }
}
