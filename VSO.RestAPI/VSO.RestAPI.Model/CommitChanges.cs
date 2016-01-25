using System;
namespace VSO.RestAPI.Model
{
	public class CommitChanges
	{
		public Changecounts changeCounts
		{
			get;
			set;
		}
		public Change[] changes
		{
			get;
			set;
		}
	}
}
