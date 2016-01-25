using System;
namespace VSO.RestAPI.Model
{
	public class Push
	{
		public Repository repository
		{
			get;
			set;
		}
		public Pushedby pushedBy
		{
			get;
			set;
		}
		public int pushId
		{
			get;
			set;
		}
		public DateTime date
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
