using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CumulativeCountReport.Data;
using CumulativeCountReport.Models;
using CumulativeCountReport.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CumulativeCountReport.Pages.Report
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public CumulativeCountReportPageViewModel ViewModel { get; set; } = new CumulativeCountReportPageViewModel();

        [BindProperty(SupportsGet = true)]
        public string? SelectedArea { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var today = DateTime.Today;
            var startDate = StartDate ?? today.AddDays(-6);
            var endDate = EndDate ?? today;

            if (startDate > endDate)
            {
                (startDate, endDate) = (endDate, startDate);
            }

            var dateRange = new List<DateTime>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                dateRange.Add(date);
            }
            ViewModel.DateRange = dateRange;

            var areas = await _context.Equipments
                .Where(e => !string.IsNullOrEmpty(e.Area))
                .Select(e => e.Area!)
                .Distinct()
                .OrderBy(a => a)
                .ToListAsync();
            ViewModel.Areas = areas;
            ViewModel.SelectedArea = SelectedArea;

            var equipmentQuery = _context.Equipments.AsQueryable();
            if (!string.IsNullOrEmpty(SelectedArea))
            {
                equipmentQuery = equipmentQuery.Where(e => e.Area == SelectedArea);
            }

            // Areaに応じたSQLクエリでwaferCountDataを取得
            var sql = WaferCountSqlUtility.GetWaferCountSqlByArea(SelectedArea);

            var waferCountData = await _context.WaferCountQueryResults
                .FromSqlRaw(sql, startDate, endDate)
                .ToListAsync();

            var controlValues = await _context.DoopControlValues
                .ToDictionaryAsync(
                    d => new { d.EopId, d.TestOpNo, d.ItemPrompt },
                    d => d.Value
                );

            var equipmentData = await equipmentQuery
                .ToDictionaryAsync(e => e.EopId);

            var reportGroups = waferCountData
                .GroupBy(w => new { w.EopId, w.TestOpNo, w.ItemPrompt });

            var reports = new List<CumulativeCountReportViewModel>();

            foreach (var group in reportGroups)
            {
                if (!equipmentData.TryGetValue(group.Key.EopId, out var equipment))
                {
                    continue;
                }

                var report = new CumulativeCountReportViewModel
                {
                    EopId = group.Key.EopId,
                    TestOpNo = group.Key.TestOpNo,
                    ItemPrompt = group.Key.ItemPrompt,
                    DailyValues = group.ToDictionary(g => g.Date, g => g.Value),
                    Area = equipment.Area,
                    Group = equipment.Group,
                    Order = equipment.Order
                };

                var controlKey = new { group.Key.EopId, group.Key.TestOpNo, group.Key.ItemPrompt };
                if (controlValues.TryGetValue(controlKey, out var controlValue))
                {
                    report.ControlValue = controlValue;
                }

                reports.Add(report);
            }

            ViewModel.Reports = reports
                .OrderBy(r => r.Area)
                .ThenBy(r => r.Group)
                .ThenBy(r => r.Order)
                .ThenBy(r => r.EopId)
                .ToList();

            return Page();
        }
    }
}
