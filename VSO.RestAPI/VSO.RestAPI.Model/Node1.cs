using System;
namespace VSO.RestAPI.Model
{
	public class Node1
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
		public Node2[] nodes
		{
			get;
			set;
		}
	}
}
