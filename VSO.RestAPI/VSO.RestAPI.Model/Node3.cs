using System;
namespace VSO.RestAPI.Model
{
	public class Node3
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
		public Node4[] nodes
		{
			get;
			set;
		}
	}
}
