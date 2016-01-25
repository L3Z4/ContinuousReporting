using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VSO.RestAPI.CustomModel;
using VSO.RestAPI.Model;

namespace VSO.RestAPI
{
    public class ApiWrapper
    {
        private readonly string entity_;
        private readonly string vsoProjectName_;

        public ApiWrapper(string entity, string vsoProjectName)
        {
            entity_ = entity;
            vsoProjectName_ = vsoProjectName;
        }

        public async Task<ModuleCoverage[]> GetBuildCoverage(ServiceHooks.HttpModel.HttpHookBuild build)
        {
            if (build == null)
                return null;

            var buildDetail = await GetBuildDetail(entity_, vsoProjectName_, build.resource.id);
            return ComputeCoverage(buildDetail);
        }

        private static ModuleCoverage[] ComputeCoverage(BuildDetails buildDetail)
        {
            if (buildDetail == null)
                return null;

            var coverageData = buildDetail.information.Select(i => i.coverageData);
            var modulCoverage = new List<ModuleCoverage>();
            foreach (var coverage in coverageData)
            {
                if (coverage == null) continue;
                foreach (var module in coverage.modules)
                {
                    if (module.name.Contains("unittest")) continue;
                    var moduleDetail = new ModuleCoverage
                    {
                        Name = module.name,
                        BlocksCovered = module.blocksCovered,
                        BlocksNotCovered = module.blocksNotCovered
                    };

                    modulCoverage.Add(moduleDetail);
                }
            }
            return modulCoverage.ToArray();
        }

        public async Task<BuildDetails> GetBuildDetail(string entity, string vsoProjectName, int buildId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}", Constantes.UserName, Constantes.Password))));

                var url = string.Format(
                    "https://{0}.visualstudio.com/DefaultCollection/{1}/_api/_build/build?__v=5&uri=vstfs%3A%2F%2F%2FBuild%2FBuild%2F{2}&includeTestRuns=true&includeCoverage=true",
                    entity,
                    vsoProjectName,
                    buildId);

                using (var response = client.GetAsync(url).Result)
                {
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<BuildDetails>(responseBody);
                }
            }
        }

        private static async Task<string[]> GetFilesLinkedToLastPush(string vsoHostName, string repository)
        {
            string[] result;
            using (IHttpClient httpClient = HttpClientFactory.CreateClient())
            {
                var list = new List<string>();
                var url = string.Format("https://{0}.visualstudio.com/DefaultCollection/_apis/git/repositories", vsoHostName);
                var repositoryCollection = await httpClient.GetAndParse<RepositoryCollection>(url);

                var buildUrl = string.Format("https://{0}.visualstudio.com/defaultcollection/Crosscut/_apis/build/builds?api-version=1.0", vsoHostName);
                var buildCollection = await httpClient.GetAndParse<BuildCollection>(buildUrl);
                var nonSucceedBuildCount = buildCollection.value.TakeWhile(build => build.status != "succeeded").Count();

                var commitUrl = string.Format("{0}/pushes?api-version=1.0&$top={1}", repositoryCollection.value.First(i => i.name == repository).url, nonSucceedBuildCount + 1);
                var pushCollection = await httpClient.GetAndParse<PushCollection>(commitUrl);

                foreach (var currentPush in pushCollection.value.Select(push => string.Format("{0}/commits", push.url)))
                {
                    var commitCollection = await httpClient.GetAndParse<CommitCollection>(currentPush);
                    foreach (var currentCommit in commitCollection.value.Where(i => i.committer.name != "TeamBuildCI").Select(commit => string.Format("{0}/changes", commit.url)))
                    {
                        var commitChanges = await httpClient.GetAndParse<CommitChanges>(currentCommit);
                        list.AddRange(commitChanges.changes.Select(change => change.item.path));
                    }
                }
                result = FilterFiles(list);
            }
            return result;
        }
        private static string[] FilterFiles(IEnumerable<string> filesPaths)
        {
            return filesPaths
                .Where(i => i.EndsWith(".cs") && !i.EndsWith("AssemblyInfo.cs") && !i.Contains("UnitTests") && !i.Contains("_Utils/Shared"))
                .Distinct()
                .ToArray();
        }

        //public static string[] GetEditedProjects(string vsoHostName, string repository, string[] availableProjects)
        //{
        //    availableProjects = availableProjects.Select(i => i.Replace("\\", "/")).OrderByDescending(i => i.Length).ToArray();
        //    var committedFiles = AsyncHelper.RunSynchronously(() => GetFilesLinkedToLastPush(vsoHostName, repository));

        //    var editedProjects = committedFiles.Select(file => availableProjects.FirstOrDefault(file.Contains)).Where(project => project != null).ToList();
        //    return editedProjects.Distinct().ToArray();
        //}
    }
}
