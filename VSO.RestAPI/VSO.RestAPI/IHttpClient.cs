using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace VSO.RestAPI
{
	public interface IHttpClient : IDisposable
	{
		Task<HttpResponseMessage> GetAsync(string url);
		Task<T> GetAndParse<T>(string url);
	}
}
