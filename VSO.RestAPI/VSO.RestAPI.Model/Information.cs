using System;
namespace VSO.RestAPI.Model
{
	public class Information
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
		public Node[] nodes
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
		public int totalErrors
		{
			get;
			set;
		}
		public int totalWarnings
		{
			get;
			set;
		}
		public Testrun[] testRuns
		{
			get;
			set;
		}
		public Coveragedata coverageData
		{
			get;
			set;
		}
	}
}
