using System;
using System.Collections.Generic;

namespace CumulativeCountReport.Models
{
    public class CumulativeCountReportViewModel
    {
        public string EopId { get; set; } = string.Empty;
        public string TestOpNo { get; set; } = string.Empty;
        public string ItemPrompt { get; set; } = string.Empty;
        public decimal ControlValue { get; set; }
        public Dictionary<DateTime, decimal> DailyValues { get; set; } = new Dictionary<DateTime, decimal>();
        public string? Area { get; set; }
        public string? Group { get; set; }
        public int? Order { get; set; }

        public bool IsOverThreshold(decimal value)
        {
            if (ControlValue == 0) return false;
            return value >= (ControlValue * 0.8m);
        }
    }

    public class CumulativeCountReportPageViewModel
    {
        public List<CumulativeCountReportViewModel> Reports { get; set; } = new List<CumulativeCountReportViewModel>();
        public List<DateTime> DateRange { get; set; } = new List<DateTime>();
        public List<string> Areas { get; set; } = new List<string>();
        public string? SelectedArea { get; set; }
    }
}
