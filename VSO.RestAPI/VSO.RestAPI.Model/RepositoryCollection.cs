using System;
namespace VSO.RestAPI.Model
{
	public class RepositoryCollection
	{
		public int count
		{
			get;
			set;
		}
		public Repository[] value
		{
			get;
			set;
		}
	}
}
