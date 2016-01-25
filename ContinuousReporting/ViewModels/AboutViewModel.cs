using System.Collections.Generic;
using System.Web.Mvc;

namespace ContinuousReporting.ViewModels
{
    public class AboutViewModel
    {
        public AboutViewModel()
        {
            StatisticModes = new List<SelectListItem>
            {
                new SelectListItem { Text = "All" },
                new SelectListItem { Text = "Last 20" },
            };
            BuildNames = new List<SelectListItem>();
        }

        public string BuildName { get; set; }
        public ModuleCoverageByBuild AverageStatistics { get; set; }
        public string Message { get; set; }

        public List<SelectListItem> StatisticModes { get; set; }
        public List<SelectListItem> BuildNames { get; set; }
    }

    public class ModuleCoverageByBuild
    {
        public string ModuleName { get; set; }
        public string Dates { get; set; }
        public string Coverages { get; set; }
    }
    public class ModuleCoverageForABuild
    {
        public string Modules { get; set; }
        public string Coverages { get; set; }
    }
}