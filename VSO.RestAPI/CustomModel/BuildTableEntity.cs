using System.Linq;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace VSO.RestAPI.CustomModel
{
    public class BuildTableEntry : TableEntity
    {
        public BuildTableEntry(string buildName, string endDate)
        {
            PartitionKey = buildName;
            RowKey = endDate;
        }

        public BuildTableEntry()
        {
        }

        public string BuildName { get { return PartitionKey; } }
        public string Date { get { return RowKey; } }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string Link { get; set; }
        public string ModuleCoverage { get; set; }

        public BuildTableEntity ToEntity()
        {
            var items = JsonConvert.DeserializeObject<ModuleCoverage[]>(ModuleCoverage);
            var collections = items.Where(i => !i.Name.Contains("unittests") && (i.Name.ToLower().StartsWith("core.") || i.Name.ToLower().StartsWith("crosscut."))).OrderBy(i => i.BlocksCovered / (i.BlocksCovered + i.BlocksNotCovered)).ToArray();
            foreach (var moduleCoverage in collections)
            {
                moduleCoverage.ComputedAverage = (moduleCoverage.BlocksCovered * 100) / (moduleCoverage.BlocksCovered + moduleCoverage.BlocksNotCovered);
            }
            collections = collections.OrderBy(i => i.ComputedAverage).ToArray();
            return new BuildTableEntity
            {
                BuildName = BuildName,
                Status = Status,
                Date = Date,
                Link = Link,
                ModuleCoverages = collections,
                Reason = Reason
            };
        }
    }
}
