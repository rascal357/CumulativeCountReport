using CumulativeCountReport.Models;
using System;
using System.Linq;

namespace CumulativeCountReport.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Equipments.Any())
            {
                return;
            }

            var equipments = new Equipment[]
            {
                new Equipment { EopId = "EETH401", Area = "EATCH", Group = "GROUP1", Order = 1 },
                new Equipment { EopId = "EETH402", Area = "EATCH", Group = "GROUP1", Order = 2 },
                new Equipment { EopId = "EETH403", Area = "EATCH", Group = "GROUP2", Order = 3 },
                new Equipment { EopId = "EETH404", Area = "DEPO", Group = "GROUP1", Order = 1 },
                new Equipment { EopId = "EETH405", Area = "DEPO", Group = "GROUP1", Order = 2 }
            };
            context.Equipments.AddRange(equipments);
            context.SaveChanges();

            var controlValues = new DoopControlValue[]
            {
                new DoopControlValue { EopId = "EETH401", TestOpNo = "EET39.01", ItemPrompt = "SPC-PM1 RF RESET", Value = 600 },
                new DoopControlValue { EopId = "EETH401", TestOpNo = "EET39.01", ItemPrompt = "SPC-PM1 WAFERCNT RESET", Value = 1500 },
                new DoopControlValue { EopId = "EETH401", TestOpNo = "EET39.01", ItemPrompt = "SPC-PM2 RF RESET", Value = 600 },
                new DoopControlValue { EopId = "EETH401", TestOpNo = "EET39.01", ItemPrompt = "SPC-PM2 WAFERCNT RESET", Value = 1500 },
                new DoopControlValue { EopId = "EETH402", TestOpNo = "EET39.02", ItemPrompt = "SPC-PM1 RF RESET", Value = 650 },
                new DoopControlValue { EopId = "EETH402", TestOpNo = "EET39.02", ItemPrompt = "SPC-PM1 WAFERCNT RESET", Value = 1600 },
                new DoopControlValue { EopId = "EETH403", TestOpNo = "EET39.03", ItemPrompt = "SPC-PM1 RF RESET", Value = 700 },
                new DoopControlValue { EopId = "EETH403", TestOpNo = "EET39.03", ItemPrompt = "SPC-PM2 RF RESET", Value = 700 },
                new DoopControlValue { EopId = "EETH404", TestOpNo = "DEP40.01", ItemPrompt = "Chamber A WAFERCNT", Value = 2000 },
                new DoopControlValue { EopId = "EETH404", TestOpNo = "DEP40.01", ItemPrompt = "Chamber B WAFERCNT", Value = 2000 },
                new DoopControlValue { EopId = "EETH405", TestOpNo = "DEP40.02", ItemPrompt = "Chamber A WAFERCNT", Value = 1800 },
                new DoopControlValue { EopId = "EETH405", TestOpNo = "DEP40.02", ItemPrompt = "Chamber B WAFERCNT", Value = 1800 }
            };
            context.DoopControlValues.AddRange(controlValues);
            context.SaveChanges();

            var today = DateTime.Today;
            var random = new Random(123);
            var waferCountHistories = new System.Collections.Generic.List<WaferCountHistory>();

            for (int dayOffset = 20; dayOffset >= 0; dayOffset--)
            {
                var date = today.AddDays(-dayOffset);

                waferCountHistories.Add(new WaferCountHistory
                {
                    EopId = "EETH401",
                    TestOpNo = "EET39.01",
                    Date = date,
                    ItemPrompt = "SPC-PM1 RF RESET",
                    Value = dayOffset == 0 ? 490m : random.Next(50, 400)
                });

                waferCountHistories.Add(new WaferCountHistory
                {
                    EopId = "EETH401",
                    TestOpNo = "EET39.01",
                    Date = date,
                    ItemPrompt = "SPC-PM1 WAFERCNT RESET",
                    Value = dayOffset == 0 ? 68m : random.Next(50, 200)
                });

                waferCountHistories.Add(new WaferCountHistory
                {
                    EopId = "EETH401",
                    TestOpNo = "EET39.01",
                    Date = date,
                    ItemPrompt = "SPC-PM2 RF RESET",
                    Value = dayOffset == 0 ? 99.8m : random.Next(50, 150)
                });

                waferCountHistories.Add(new WaferCountHistory
                {
                    EopId = "EETH401",
                    TestOpNo = "EET39.01",
                    Date = date,
                    ItemPrompt = "SPC-PM2 WAFERCNT RESET",
                    Value = dayOffset == 0 ? 1231m : random.Next(800, 1400)
                });

                waferCountHistories.Add(new WaferCountHistory
                {
                    EopId = "EETH402",
                    TestOpNo = "EET39.02",
                    Date = date,
                    ItemPrompt = "SPC-PM1 RF RESET",
                    Value = random.Next(100, 500)
                });

                waferCountHistories.Add(new WaferCountHistory
                {
                    EopId = "EETH402",
                    TestOpNo = "EET39.02",
                    Date = date,
                    ItemPrompt = "SPC-PM1 WAFERCNT RESET",
                    Value = random.Next(200, 1200)
                });

                waferCountHistories.Add(new WaferCountHistory
                {
                    EopId = "EETH403",
                    TestOpNo = "EET39.03",
                    Date = date,
                    ItemPrompt = "SPC-PM1 RF RESET",
                    Value = random.Next(150, 600)
                });

                waferCountHistories.Add(new WaferCountHistory
                {
                    EopId = "EETH403",
                    TestOpNo = "EET39.03",
                    Date = date,
                    ItemPrompt = "SPC-PM2 RF RESET",
                    Value = random.Next(150, 600)
                });

                waferCountHistories.Add(new WaferCountHistory
                {
                    EopId = "EETH404",
                    TestOpNo = "DEP40.01",
                    Date = date,
                    ItemPrompt = "Chamber A WAFERCNT",
                    Value = random.Next(500, 1800)
                });

                waferCountHistories.Add(new WaferCountHistory
                {
                    EopId = "EETH404",
                    TestOpNo = "DEP40.01",
                    Date = date,
                    ItemPrompt = "Chamber B WAFERCNT",
                    Value = random.Next(500, 1800)
                });

                waferCountHistories.Add(new WaferCountHistory
                {
                    EopId = "EETH405",
                    TestOpNo = "DEP40.02",
                    Date = date,
                    ItemPrompt = "Chamber A WAFERCNT",
                    Value = random.Next(400, 1600)
                });

                waferCountHistories.Add(new WaferCountHistory
                {
                    EopId = "EETH405",
                    TestOpNo = "DEP40.02",
                    Date = date,
                    ItemPrompt = "Chamber B WAFERCNT",
                    Value = random.Next(400, 1600)
                });
            }

            context.WaferCountHistories.AddRange(waferCountHistories);
            context.SaveChanges();
        }
    }
}
