using System;
namespace VSO.RestAPI.Model
{
	public class BuildDetails
	{
		public string uri
		{
			get;
			set;
		}
		public string name
		{
			get;
			set;
		}
		public string definition
		{
			get;
			set;
		}
		public string definitionUri
		{
			get;
			set;
		}
		public string controller
		{
			get;
			set;
		}
		public DateTime startTime
		{
			get;
			set;
		}
		public DateTime finishTime
		{
			get;
			set;
		}
		public bool finished
		{
			get;
			set;
		}
		public object quality
		{
			get;
			set;
		}
		public bool retain
		{
			get;
			set;
		}
		public string lastChangedBy
		{
			get;
			set;
		}
		public DateTime lastChangedOn
		{
			get;
			set;
		}
		public int status
		{
			get;
			set;
		}
		public int reason
		{
			get;
			set;
		}
		public Dropfolder dropFolder
		{
			get;
			set;
		}
		public string requestedFor
		{
			get;
			set;
		}
		public string project
		{
			get;
			set;
		}
		public int[] requests
		{
			get;
			set;
		}
		public Vc vc
		{
			get;
			set;
		}
		public Git git
		{
			get;
			set;
		}
		public bool hasDiagnostics
		{
			get;
			set;
		}
		public object[] customSummarySections
		{
			get;
			set;
		}
		public Information[] information
		{
			get;
			set;
		}
		public object[] openedWorkItems
		{
			get;
			set;
		}
		public object[] associatedWorkItems
		{
			get;
			set;
		}
	}
}
