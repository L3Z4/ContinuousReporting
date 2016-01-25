using System;
namespace VSO.RestAPI.Model
{
	public class Module
	{
		public string name
		{
			get;
			set;
		}
		public int blocksCovered
		{
			get;
			set;
		}
		public int blocksNotCovered
		{
			get;
			set;
		}
	}
}
