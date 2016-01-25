using System;
using System.Net.Http.Headers;
namespace VSO.RestAPI
{
	public static class HttpClientFactory
	{
		public static IHttpClient CreateClient()
		{
			return new HttpClientAdapter
			{
				DefaultRequestHeaders = 
				{
					Accept = 
					{
						new MediaTypeWithQualityHeaderValue("application/json")
					},
					Authorization = new AuthenticationHeaderValue("Basic", Constantes.Key)
				}
			};
		}
	}
}
