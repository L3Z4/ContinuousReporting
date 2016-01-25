namespace VSO.RestAPI.Model
{
    public class Request
    {
        public int id { get; set; }
        public Requestedfor requestedFor { get; set; }
        public string url { get; set; }
    }
}