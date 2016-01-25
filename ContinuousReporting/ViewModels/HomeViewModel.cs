using System.Collections.Generic;
using ContinuousReporting.Models;

namespace ContinuousReporting.ViewModels
{
    public class HomeViewModel
    {
        public List<BuildSummary> Builds { get; set; }
        public ModuleCoverageForABuild CoverageStat { get; set; }
        public IEnumerable<ShootBoxBuild> ShootBoxBuilds { get; set; }
        public IEnumerable<ChallengeSummary> Challenges { get; set; }
    }

    public class ChallengeSummary
    {
        public string Name { get; set; }
        public int Points { get; set; }
    }

    public class BuildSummary
    {
        public BuildEntity ActualBuild { get; set; }
        public ChallengeEntity Challenge { get; set; }
        public double ActualAverageCoverage { get; set; }

        public string BuildDate { get { return ActualBuild.Date.ToShortDateString(); } }
        public string BuildTime { get { return ActualBuild.Date.ToShortTimeString(); } }
        public string UserGravatar { get; set; }
    }

    public class ShootBoxBuild
    {
        public string BuildName { get; set; }
        public string BuildDate { get; set; }
        public string BuildColor { get; set; }
        public string User { get; set; }
        public int SumPoints { get; set; }
        public ICollection<ModuleChallengeCoverage> DetailPoints { get; set; }
    }
}