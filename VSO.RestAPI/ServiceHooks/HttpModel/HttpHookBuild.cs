using System;

namespace VSO.RestAPI.ServiceHooks.HttpModel
{
    public class HttpHookBuild
    {
        public string subscriptionId { get; set; }
        public int notificationId { get; set; }
        public string id { get; set; }
        public string eventType { get; set; }
        public string publisherId { get; set; }
        public Message message { get; set; }
        public Detailedmessage detailedMessage { get; set; }
        public Resource resource { get; set; }
        public DateTime createdDate { get; set; }
    }
}