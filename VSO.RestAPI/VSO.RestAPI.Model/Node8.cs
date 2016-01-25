using System;
namespace VSO.RestAPI.Model
{
	public class Node8
	{
		public string type
		{
			get;
			set;
		}
		public string text
		{
			get;
			set;
		}
		public string platform
		{
			get;
			set;
		}
		public string flavor
		{
			get;
			set;
		}
		public string serverPath
		{
			get;
			set;
		}
		public string localPath
		{
			get;
			set;
		}
		public string targetNames
		{
			get;
			set;
		}
		public int errors
		{
			get;
			set;
		}
		public int warnings
		{
			get;
			set;
		}
		public int total
		{
			get;
			set;
		}
		public string status
		{
			get;
			set;
		}
		public bool message
		{
			get;
			set;
		}
		public string warningType
		{
			get;
			set;
		}
		public Node9[] nodes
		{
			get;
			set;
		}
	}
}
