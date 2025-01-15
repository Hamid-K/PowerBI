using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Properties;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000011 RID: 17
	public static class HttpClientFactory
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002B1C File Offset: 0x00000D1C
		public static HttpClient Create(params DelegatingHandler[] handlers)
		{
			return HttpClientFactory.Create(new HttpClientHandler(), handlers);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B29 File Offset: 0x00000D29
		public static HttpClient Create(HttpMessageHandler innerHandler, params DelegatingHandler[] handlers)
		{
			return new HttpClient(HttpClientFactory.CreatePipeline(innerHandler, handlers));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B38 File Offset: 0x00000D38
		public static HttpMessageHandler CreatePipeline(HttpMessageHandler innerHandler, IEnumerable<DelegatingHandler> handlers)
		{
			if (innerHandler == null)
			{
				throw Error.ArgumentNull("innerHandler");
			}
			if (handlers == null)
			{
				return innerHandler;
			}
			HttpMessageHandler httpMessageHandler = innerHandler;
			foreach (DelegatingHandler delegatingHandler in handlers.Reverse<DelegatingHandler>())
			{
				if (delegatingHandler == null)
				{
					throw Error.Argument("handlers", Resources.DelegatingHandlerArrayContainsNullItem, new object[] { typeof(DelegatingHandler).Name });
				}
				if (delegatingHandler.InnerHandler != null)
				{
					throw Error.Argument("handlers", Resources.DelegatingHandlerArrayHasNonNullInnerHandler, new object[]
					{
						typeof(DelegatingHandler).Name,
						"InnerHandler",
						delegatingHandler.GetType().Name
					});
				}
				delegatingHandler.InnerHandler = httpMessageHandler;
				httpMessageHandler = delegatingHandler;
			}
			return httpMessageHandler;
		}
	}
}
