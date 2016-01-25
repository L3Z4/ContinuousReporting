using System;
namespace VSO.RestAPI.Model
{
	public class BuildCollection
	{
		public int count
		{
			get;
			set;
		}
		public Build[] value
		{
			get;
			set;
		}
	}
}
