using System;
namespace VSO.RestAPI.Model
{
	public class Vc
	{
		public int csId
		{
			get;
			set;
		}
		public object shelvesetName
		{
			get;
			set;
		}
		public string version
		{
			get;
			set;
		}
		public string versionText
		{
			get;
			set;
		}
		public int successCs
		{
			get;
			set;
		}
		public int failCs
		{
			get;
			set;
		}
		public object[] associatedChangesets
		{
			get;
			set;
		}
	}
}
