using System.Linq;
using Newtonsoft.Json;

namespace VSO.RestAPI.CustomModel
{
    public class BuildTableEntity
    {
        public string BuildName { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string Link { get; set; }
        public ModuleCoverage[] ModuleCoverages { get; set; }

        public BuildTableEntry ToEntry()
        {
            ModuleCoverages = ModuleCoverages.Where(i => !i.Name.Contains("unittests")).ToArray();

            var entry = new BuildTableEntry(BuildName, Date)
            {
                Status = Status,
                Reason = Reason,
                Link = Link,
                ModuleCoverage = JsonConvert.SerializeObject(ModuleCoverages)
            };

            return entry;
        }

        public string TitleClassName
        {
            get
            {
                return Status == "succeeded" ? "bg-success" : "bg-danger";
            }
        }
    }
}
