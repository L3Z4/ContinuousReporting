using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace VSO.RestAPI
{
	internal class HttpClientAdapter : IHttpClient, IDisposable
	{
		private readonly HttpClient _client = new HttpClient();
		public HttpRequestHeaders DefaultRequestHeaders
		{
			get
			{
				return this._client.DefaultRequestHeaders;
			}
		}
		public Task<HttpResponseMessage> GetAsync(string url)
		{
			return this._client.GetAsync(url);
		}
		public async Task<T> GetAndParse<T>(string url)
		{
			T result;
			using (HttpResponseMessage httpResponseMessage = await this._client.GetAsync(url))
			{
				httpResponseMessage.EnsureSuccessStatusCode();
				string value = await httpResponseMessage.Content.ReadAsStringAsync();
				result = JsonConvert.DeserializeObject<T>(value);
			}
			return result;
		}
		public void Dispose()
		{
			this._client.Dispose();
		}
	}
}
