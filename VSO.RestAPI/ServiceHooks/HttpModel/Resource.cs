using System;

namespace VSO.RestAPI.ServiceHooks.HttpModel
{
    public class Resource
    {
        public string uri { get; set; }
        public int id { get; set; }
        public string buildNumber { get; set; }
        public string url { get; set; }
        public DateTime startTime { get; set; }
        public DateTime finishTime { get; set; }
        public string reason { get; set; }
        public string status { get; set; }
        public Drop drop { get; set; }
        public string sourceGetVersion { get; set; }
        public Lastchangedby lastChangedBy { get; set; }
        public bool retainIndefinitely { get; set; }
        public bool hasDiagnostics { get; set; }
        public Definition definition { get; set; }
        public Queue queue { get; set; }
        public Request[] requests { get; set; }
    }
}