using System;
namespace VSO.RestAPI.Model
{
	public class Node
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
		public Node1[] nodes
		{
			get;
			set;
		}
	}
}
