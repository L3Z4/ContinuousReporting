namespace VSO.RestAPI.CustomModel
{
    public class ModuleCoverage
    {
        public string Name { get; set; }
        public int BlocksCovered { get; set; }
        public int BlocksNotCovered { get; set; }
        public int ComputedAverage { get; set; }
    }
}