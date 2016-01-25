using System;
namespace VSO.RestAPI.Model
{
	public class PushCollection
	{
		public int count
		{
			get;
			set;
		}
		public Push[] value
		{
			get;
			set;
		}
	}
}
