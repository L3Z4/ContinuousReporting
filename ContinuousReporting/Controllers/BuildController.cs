using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ContinuousReporting.Models;
using ContinuousReporting.Services;
using VSO.RestAPI;
using VSO.RestAPI.ServiceHooks.HttpModel;

namespace ContinuousReporting.Controllers
{
    public class BuildController : ApiController
    {
        [HttpPost, Route("api/Build/startvm")]
        public IHttpActionResult StartVM()
        {
            VirtualMachineService.Instance.Start();
            return Ok();
        }

        [HttpPost, Route("api/Build/stopvm")]
        public IHttpActionResult StopVM()
        {
            VirtualMachineService.Instance.Shutdown();
            return Ok();
        }

        // POST: api/Build
        [HttpPost, Route("api/Build")]
        public async Task<IHttpActionResult> Post([FromBody] HttpHookBuild build)
        {
            var knowDefinitionsBuilds = ConfigurationManager.AppSettings["ExpectedBuilds"].Split('|');
            if (knowDefinitionsBuilds.Any() && !knowDefinitionsBuilds.Contains(build.resource.definition.name))
                return Ok();

            try
            {
                using (var context = new ReportingContext())
                {
                    var request = build.resource.requests.FirstOrDefault();

                    var buildEntity = new BuildEntity
                    {
                        BuildName = build.resource.buildNumber,
                        Definition = build.resource.definition.name,
                        Date = build.resource.finishTime,
                        Status = build.resource.status,
                        Reason = build.resource.reason,
                        Link = build.message.html,
                        User = request != null ? request.requestedFor.uniqueName : "Unknown user",
                        CoverageCollection = new Collection<CoverageEntity>()
                    };

                    if (buildEntity.User == ConfigurationManager.AppSettings["VsoUsername"])
                        return Ok();

                    if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["VSO.OrganisationName"]) || string.IsNullOrEmpty(ConfigurationManager.AppSettings["VSO.ProjectName"]))
                        throw new ApplicationException("Please configuration VSO.OrganisationName and VSO.ProjectName first.");

                    var api = new ApiWrapper(ConfigurationManager.AppSettings["VSO.OrganisationName"], ConfigurationManager.AppSettings["VSO.ProjectName"]);
                    var coverage = await api.GetBuildCoverage(build);
                    if (coverage != null)
                    {
                        foreach (var moduleCoverage in coverage)
                        {
                            buildEntity.CoverageCollection.Add(new CoverageEntity
                            {
                                Name = moduleCoverage.Name,
                                ComputedAverage = moduleCoverage.ComputedAverage,
                                BlocksCovered = moduleCoverage.BlocksCovered,
                                BlocksNotCovered = moduleCoverage.BlocksNotCovered,
                                Build = buildEntity
                            });
                        }
                    }

                    double currentRatio = ComputeRatio(buildEntity, context);
                    var challenge = ComputeCoverageChanges(buildEntity, context, currentRatio);
                    if (challenge != null)
                        context.Challenges.Add(challenge);

                    context.Builds.Add(buildEntity);
                    context.SaveChanges();
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private double ComputeRatio(BuildEntity buildEntity, ReportingContext context)
        {
            var currentProject = context.Projects.FirstOrDefault(i => i.Name == buildEntity.Definition);
            if (currentProject == null)
            {
                currentProject = new ProjectEntity { Name = buildEntity.Definition };
                context.Projects.Add(currentProject);
            }
            if (!buildEntity.CoverageCollection.Any())
                return 0;

            var currentProjectBlock = buildEntity.CoverageCollection.Sum(i => i.BlocksCovered + i.BlocksNotCovered);
            currentProject.Blocks = currentProjectBlock;

            if (!context.Projects.Any())
                return 1;

            var maxBlock = context.Projects.Max(i => i.Blocks);
            var maxProject = context.Projects.FirstOrDefault(i => i.Blocks == maxBlock);
            if (maxProject == null || maxBlock == 0)
                return 1; // Ratio = 1 : no project to be compared.

            if (maxProject.Id == currentProject.Id)
                return 1; // current projet is the bigest project.

            return (double)currentProjectBlock / maxBlock;
        }

        private ChallengeEntity ComputeCoverageChanges(BuildEntity buildEntity, ReportingContext context, double currentRatio)
        {
            if (buildEntity.Status != "succeeded")
                return null;

            var previousBuild = context.Builds
                .OrderByDescending(i => i.Date)
                .FirstOrDefault(i => i.Definition == buildEntity.Definition && i.Status == "succeeded" && i.CoverageCollection.Any());

            if (previousBuild == null)
                return null;

            var challenge = new ChallengeEntity
            {
                User = buildEntity.User,
                Build = buildEntity,
                ModuleCoverages = new List<ModuleChallengeCoverage>()
            };

            var allModules = previousBuild.CoverageCollection.Select(i => i.Name).Union(buildEntity.CoverageCollection.Select(i => i.Name));
            foreach (var moduleName in allModules)
            {
                var currentModuleName = moduleName;

                var previousModule = previousBuild.CoverageCollection.FirstOrDefault(i => i.Name == currentModuleName);
                var currentModule = buildEntity.CoverageCollection.FirstOrDefault(i => i.Name == currentModuleName);

                if (currentModule != null && previousModule != null && (previousModule.BlocksCovered == currentModule.BlocksCovered && previousModule.BlocksNotCovered == currentModule.BlocksNotCovered))
                    continue; // Ignore if no change in the module.
                if (currentModule == null)
                    continue;

                var previousModuleCoverage = ComputeBuildCoverage(previousModule);
                var currentModuleCoverage = ComputeBuildCoverage(currentModule);

                var deltaCoverage = currentModuleCoverage - previousModuleCoverage;
                var allBlocks = currentModule.BlocksCovered + currentModule.BlocksNotCovered;

                challenge.ModuleCoverages.Add(new ModuleChallengeCoverage { Module = currentModuleName, DeltaCoverage = deltaCoverage, Blocks = allBlocks });
            }

            var previousProjectCoverage = ComputeBuildCoverage(previousBuild);
            var currentProjectCoverage = ComputeBuildCoverage(buildEntity);
            challenge.Points = (currentProjectCoverage - previousProjectCoverage) * 100 * currentRatio;
            if (double.IsNaN(challenge.Points))
                challenge.Points = 0;

            return challenge;
        }

        private double ComputeBuildCoverage(BuildEntity build)
        {
            if (build.CoverageCollection == null)
                return 0;

            var covered = build.CoverageCollection.Sum(i => i.BlocksCovered);
            var notCovered = build.CoverageCollection.Sum(i => i.BlocksNotCovered);

            return ((Double)covered * 100) / (notCovered + covered);
        }

        private double ComputeBuildCoverage(CoverageEntity coverages)
        {
            if (coverages == null)
                return 0;

            var covered = coverages.BlocksCovered;
            var notCovered = coverages.BlocksNotCovered;
            return ((Double)covered * 100) / (notCovered + covered);
        }
    }
}
