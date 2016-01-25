using System;
namespace VSO.RestAPI.Model
{
	public class Commit
	{
		public string commitId
		{
			get;
			set;
		}
		public Author author
		{
			get;
			set;
		}
		public Committer committer
		{
			get;
			set;
		}
		public string comment
		{
			get;
			set;
		}
		public Changecounts changeCounts
		{
			get;
			set;
		}
		public string url
		{
			get;
			set;
		}
		public string remoteUrl
		{
			get;
			set;
		}
	}
}
