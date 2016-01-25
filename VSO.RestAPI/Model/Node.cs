namespace VSO.RestAPI.Model
{
    public class Node
    {
        public string type { get; set; }
        public string text { get; set; }
        public Node1[] nodes { get; set; }
    }
    public class Node1
    {
        public string type { get; set; }
        public string text { get; set; }
        public Node2[] nodes { get; set; }
    }

    public class Node2
    {
        public string type { get; set; }
        public string text { get; set; }
        public Node3[] nodes { get; set; }
    }

    public class Node3
    {
        public string type { get; set; }
        public string text { get; set; }
        public Node4[] nodes { get; set; }
    }

    public class Node4
    {
        public string type { get; set; }
        public string text { get; set; }
        public Node5[] nodes { get; set; }
        public string platform { get; set; }
        public string flavor { get; set; }
        public string serverPath { get; set; }
        public string localPath { get; set; }
        public string targetNames { get; set; }
        public int errors { get; set; }
        public int warnings { get; set; }
        public int total { get; set; }
        public string status { get; set; }
        public bool message { get; set; }
    }

    public class Node5
    {
        public string type { get; set; }
        public string text { get; set; }
        public Node6[] nodes { get; set; }
        public string platform { get; set; }
        public string flavor { get; set; }
        public string serverPath { get; set; }
        public string localPath { get; set; }
        public string targetNames { get; set; }
        public int errors { get; set; }
        public int warnings { get; set; }
        public int total { get; set; }
        public Link link { get; set; }
    }

    public class Link
    {
        public string url { get; set; }
        public string text { get; set; }
        public int type { get; set; }
    }

    public class Node6
    {
        public string type { get; set; }
        public string text { get; set; }
        public Node7[] nodes { get; set; }
        public string platform { get; set; }
        public string flavor { get; set; }
        public string serverPath { get; set; }
        public string localPath { get; set; }
        public string targetNames { get; set; }
        public int errors { get; set; }
        public int warnings { get; set; }
        public int total { get; set; }
    }

    public class Node7
    {
        public string type { get; set; }
        public string text { get; set; }
        public string platform { get; set; }
        public string flavor { get; set; }
        public string serverPath { get; set; }
        public string localPath { get; set; }
        public string targetNames { get; set; }
        public int errors { get; set; }
        public int warnings { get; set; }
        public int total { get; set; }
        public string status { get; set; }
        public bool message { get; set; }
        public string warningType { get; set; }
        public Node8[] nodes { get; set; }
    }

    public class Node8
    {
        public string type { get; set; }
        public string text { get; set; }
        public string platform { get; set; }
        public string flavor { get; set; }
        public string serverPath { get; set; }
        public string localPath { get; set; }
        public string targetNames { get; set; }
        public int errors { get; set; }
        public int warnings { get; set; }
        public int total { get; set; }
        public string status { get; set; }
        public bool message { get; set; }
        public string warningType { get; set; }
        public Node9[] nodes { get; set; }
    }

    public class Node9
    {
        public string type { get; set; }
        public string text { get; set; }
        public Node10[] nodes { get; set; }
        public string platform { get; set; }
        public string flavor { get; set; }
        public string serverPath { get; set; }
        public string localPath { get; set; }
        public string targetNames { get; set; }
        public int errors { get; set; }
        public int warnings { get; set; }
        public int total { get; set; }
        public string status { get; set; }
        public bool message { get; set; }
        public string warningType { get; set; }
    }

    public class Node10
    {
        public string type { get; set; }
        public string text { get; set; }
        public string platform { get; set; }
        public string flavor { get; set; }
        public string serverPath { get; set; }
        public string localPath { get; set; }
        public string targetNames { get; set; }
        public int errors { get; set; }
        public int warnings { get; set; }
        public int total { get; set; }
        public Node11[] nodes { get; set; }
    }

    public class Node11
    {
        public string type { get; set; }
        public string text { get; set; }
        public string platform { get; set; }
        public string flavor { get; set; }
        public string serverPath { get; set; }
        public string localPath { get; set; }
        public string targetNames { get; set; }
        public int errors { get; set; }
        public int warnings { get; set; }
        public int total { get; set; }
    }
}
