namespace VSO.RestAPI.ServiceHooks.HttpModel
{
    public class Request
    {
        public int id { get; set; }
        public string url { get; set; }
        public Requestedfor requestedFor { get; set; }
    }
}