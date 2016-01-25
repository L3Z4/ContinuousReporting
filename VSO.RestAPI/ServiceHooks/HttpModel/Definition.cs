namespace VSO.RestAPI.ServiceHooks.HttpModel
{
    public class Definition
    {
        public int batchSize { get; set; }
        public string triggerType { get; set; }
        public string definitionType { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }
}