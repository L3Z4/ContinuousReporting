using System;
namespace VSO.RestAPI.Model
{
	public class Item
	{
		public string objectId
		{
			get;
			set;
		}
		public string gitObjectType
		{
			get;
			set;
		}
		public string commitId
		{
			get;
			set;
		}
		public string path
		{
			get;
			set;
		}
		public string url
		{
			get;
			set;
		}
	}
}
