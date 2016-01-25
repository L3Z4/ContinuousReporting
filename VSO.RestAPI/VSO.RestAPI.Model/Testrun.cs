using System;
namespace VSO.RestAPI.Model
{
	public class Testrun
	{
		public int id
		{
			get;
			set;
		}
		public int passed
		{
			get;
			set;
		}
		public int failed
		{
			get;
			set;
		}
		public int inconclusive
		{
			get;
			set;
		}
		public int total
		{
			get;
			set;
		}
	}
}
