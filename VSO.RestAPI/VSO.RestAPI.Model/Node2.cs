using System;
namespace VSO.RestAPI.Model
{
	public class Node2
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
		public Node3[] nodes
		{
			get;
			set;
		}
	}
}
