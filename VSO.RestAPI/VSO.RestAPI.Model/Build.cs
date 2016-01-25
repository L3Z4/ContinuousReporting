using System;
namespace VSO.RestAPI.Model
{
	public class Build
	{
		public string buildNumber
		{
			get;
			set;
		}
		public Definition definition
		{
			get;
			set;
		}
		public Drop drop
		{
			get;
			set;
		}
		public string dropLocation
		{
			get;
			set;
		}
		public DateTime finishTime
		{
			get;
			set;
		}
		public bool hasDiagnostics
		{
			get;
			set;
		}
		public int id
		{
			get;
			set;
		}
		public Lastchangedby lastChangedBy
		{
			get;
			set;
		}
		public Log log
		{
			get;
			set;
		}
		public string reason
		{
			get;
			set;
		}
		public bool retainIndefinitely
		{
			get;
			set;
		}
		public string sourceGetVersion
		{
			get;
			set;
		}
		public DateTime startTime
		{
			get;
			set;
		}
		public string status
		{
			get;
			set;
		}
		public string uri
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
