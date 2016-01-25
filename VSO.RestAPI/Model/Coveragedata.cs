namespace VSO.RestAPI.Model
{
    public class Coveragedata
    {
        public bool pending { get; set; }
        public string lastError { get; set; }
        public Module[] modules { get; set; }
    }
}