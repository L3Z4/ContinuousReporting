using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using ContinuousReporting.Models;
using ContinuousReporting.ViewModels;

namespace ContinuousReporting.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            using (var dbContext = new ReportingContext())
            {
                var buildSummary = GetLastBuildSummary(dbContext);
                var viewModel = new HomeViewModel
                {
                    Builds = buildSummary,
                    CoverageStat = new ModuleCoverageForABuild(),
                    Challenges = ComputeChallengesSummary(dbContext, startDate, startDate.AddMonths(1)),
                };

                var mainProjectName = ConfigurationManager.AppSettings["MainProjectName"];
                var lastBuild = !string.IsNullOrEmpty(mainProjectName) ? buildSummary.FirstOrDefault(i => i.ActualBuild.Definition == mainProjectName) : buildSummary.FirstOrDefault();
                if (lastBuild != null)
                {
                    var coverages = lastBuild.ActualBuild.CoverageCollection.OrderBy(i => (i.BlocksCovered * 100) / (i.BlocksNotCovered + i.BlocksCovered)).ToArray();
                    viewModel.CoverageStat.Modules = string.Join(", ", coverages.Select(i => i.Name.Replace(".dll", string.Empty).Replace(".exe", string.Empty)));
                    viewModel.CoverageStat.Coverages = string.Join(", ", coverages.Select(i => (i.BlocksCovered * 100) / (i.BlocksNotCovered + i.BlocksCovered)));
                }

                viewModel.ShootBoxBuilds = GetLastBuilds(dbContext, startDate, startDate.AddMonths(1)).ToArray();
                return View(viewModel);
            }
        }

        public ActionResult About(string id, string mode)
        {
            var top = 0;
            if (mode == "Last 20")
                top = 20;
            return View(GetCoverageStat(id, top));
        }

        public ActionResult Challenges()
        {
            var monthChallenges = new List<MonthChallenge>();
            using (var context = new ReportingContext())
            {
                var firstDate = context.Builds.OrderBy(i => i.Date).Select(i => i.Date).FirstOrDefault();
                firstDate = new DateTime(firstDate.Year, firstDate.Month, 1);
                for (var date = firstDate; date < DateTime.UtcNow; date = date.AddMonths(1))
                {
                    var challengeSummary = ComputeChallengesSummary(context, date, date.AddMonths(1));
                    monthChallenges.Add(new MonthChallenge
                    {
                        Date = date,
                        Summary = challengeSummary
                    });
                }
            }

            return View(new ChallengesViewModel { MonthChallenges = monthChallenges.OrderByDescending(i => i.Date).ToArray() });
        }

        private IEnumerable<ChallengeSummary> ComputeChallengesSummary(ReportingContext dbContext, DateTime startDate, DateTime stopDate)
        {
            var challenges = new List<ChallengeSummary>();
            var committers = dbContext.Builds.Where(i => i.Date >= startDate && i.Date < stopDate).Select(i => i.User).Distinct().ToArray();

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var committer in committers)
            {
                var currentCommitter = committer;
                var challengeEntities = dbContext.Challenges.Where(i => i.User == currentCommitter && i.Build.Date >= startDate && i.Build.Date < stopDate).ToList();
                var points = challengeEntities.Sum(i => i.Points);
                var challengeSummary = new ChallengeSummary
                {
                    Name = committer,
                    Points = Convert.ToInt32(points)
                };
                challenges.Add(challengeSummary);
            }

            return challenges.OrderByDescending(i => i.Points).Take(3);
        }

        private IEnumerable<ShootBoxBuild> GetLastBuilds(ReportingContext dbContext, DateTime startDate, DateTime stopDate)
        {
            return dbContext.Builds.Where(i => i.Date >= startDate && i.Date < stopDate).OrderByDescending(i => i.Date).ToList().Select(i =>
            {
                var challenge = dbContext.Challenges.FirstOrDefault(j => j.Build.Id == i.Id);

                return new ShootBoxBuild
                {
                    BuildName = i.BuildName,
                    BuildDate = string.Format("{0} {1}", i.Date.ToShortDateString(), i.Date.ToShortTimeString()),
                    BuildColor = i.Status == "succeeded" ? "#5cb85c" : "#d9534f",
                    SumPoints = challenge != null ? Convert.ToInt32(challenge.Points) : 0,
                    DetailPoints = challenge != null ? challenge.ModuleCoverages : null,
                    User = i.User,
                };
            });
        }

        private List<BuildSummary> GetLastBuildSummary(ReportingContext dbContext)
        {
            var builds = new List<BuildSummary>();

            foreach (var buildName in ConfigurationManager.AppSettings["ExpectedBuilds"].Split('|'))
            {
                var orderByDescending = dbContext.Builds.OrderByDescending(i => i.Date);
                var build = orderByDescending.FirstOrDefault(i => i.Definition == buildName);
                if (build == null)
                {
                    builds.Add(new BuildSummary { ActualBuild = new BuildEntity { BuildName = "Not actually a build.", CoverageCollection = new Collection<CoverageEntity>() }, Challenge = new ChallengeEntity() });
                    continue;
                }

                var actualBuild = build;
                foreach (var coverageEntity in actualBuild.CoverageCollection)
                {
                    coverageEntity.ComputedAverage =
                        (coverageEntity.BlocksCovered * 100) / (coverageEntity.BlocksCovered + coverageEntity.BlocksNotCovered);
                }
                actualBuild.CoverageCollection = actualBuild.CoverageCollection.OrderBy(i => i.ComputedAverage).ToArray();

                var actualAverageCoverage = ComputeBuildCoverage(actualBuild.CoverageCollection.ToList());

                var buildSummary = new BuildSummary
                {
                    ActualBuild = actualBuild,
                    ActualAverageCoverage = actualAverageCoverage,
                    Challenge = dbContext.Challenges.FirstOrDefault(i => i.Build.Id == actualBuild.Id),
                    UserGravatar = CalculateMD5Hash(actualBuild.User.ToLower()).ToLower()
                };
                builds.Add(buildSummary);
            }

            return builds;
        }

        private double ComputeBuildCoverage(List<CoverageEntity> coverages)
        {
            if (!coverages.Any())
                return Double.NaN;

            var allCovered = coverages.Sum(i => i.BlocksCovered);
            var allNotCovered = coverages.Sum(i => i.BlocksNotCovered);
            return ((Double)allCovered * 100) / (allNotCovered + allCovered);
        }

        private AboutViewModel GetCoverageStat(string buildName, int top)
        {
            using (var context = new ReportingContext())
            {
                var allStats = context.Builds
                    .Where(i => i.Definition == buildName && i.CoverageCollection.Any()).ToArray();

                if (top != 0)
                {
                    var count = allStats.Count();
                    if (count > top)
                    {
                        allStats = allStats.Skip(count - top).Take(top).ToArray();
                    }
                }

                var stats = allStats
                    .ToArray();
                var step = (stats.Length / 20);
                var selectedEntries = new List<BuildEntity>();
                for (var i = stats.Min(a => a.Id); i < stats.Max(a => a.Id); i += step)
                {
                    var b = stats.FirstOrDefault(j => j.Id == i);
                    if (b != null)
                        selectedEntries.Add(b);
                }
                if (!selectedEntries.Any())
                {
                    return new AboutViewModel { BuildName = buildName, Message = "No coverage result" };
                }


                if (selectedEntries.Count() == 1)
                {
                    selectedEntries = new List<BuildEntity> { selectedEntries[0], selectedEntries[0] };
                }

                selectedEntries = selectedEntries.OrderBy(i => i.Date).ToList();
                var moduleCoverages = new List<int>();
                foreach (var buildInformation in selectedEntries)
                {
                    if (buildInformation.CoverageCollection.Any())
                    {
                        var allCovered = buildInformation.CoverageCollection.Sum(i => i.BlocksCovered);
                        var allNotCovered = buildInformation.CoverageCollection.Sum(i => i.BlocksNotCovered);
                        moduleCoverages.Add((allCovered * 100) / (allNotCovered + allCovered));
                    }
                    else
                    {
                        moduleCoverages.Add(0);
                    }
                }
                var averageStat = new ModuleCoverageByBuild
                {
                    ModuleName = buildName,
                    Dates = string.Join(", ", selectedEntries.Select(i => string.Format("{0} {1}", i.Date.ToShortDateString(), i.Date.ToShortTimeString()))),
                    Coverages = string.Join(", ", moduleCoverages)
                };

                return new AboutViewModel
                {
                    BuildName = buildName,
                    AverageStatistics = averageStat,
                    BuildNames = ConfigurationManager.AppSettings["ExpectedBuilds"].Split('|').Select(i => new SelectListItem { Text = i }).ToList()
                };
            }
        }

        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public ActionResult SelectBuild(string buildName)
        {
            return Redirect(string.Format("/Home/About/{0}", buildName));
        }
    }
}