using System;
namespace VSO.RestAPI.Model
{
	public class CommitCollection
	{
		public int count
		{
			get;
			set;
		}
		public Commit[] value
		{
			get;
			set;
		}
	}
}
