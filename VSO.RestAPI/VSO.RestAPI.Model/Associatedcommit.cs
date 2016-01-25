using System;
namespace VSO.RestAPI.Model
{
	public class Associatedcommit
	{
		public string id
		{
			get;
			set;
		}
		public string by
		{
			get;
			set;
		}
		public string comment
		{
			get;
			set;
		}
		public Artifactdata artifactData
		{
			get;
			set;
		}
	}
}
