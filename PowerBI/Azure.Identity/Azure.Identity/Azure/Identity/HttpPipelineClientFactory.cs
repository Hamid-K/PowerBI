using System;
using System.Net.Http;
using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x0200005F RID: 95
	internal class HttpPipelineClientFactory : IMsalHttpClientFactory
	{
		// Token: 0x06000374 RID: 884 RVA: 0x0000A9CE File Offset: 0x00008BCE
		public HttpPipelineClientFactory(HttpPipeline pipeline)
		{
			this._pipeline = pipeline;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000A9DD File Offset: 0x00008BDD
		public HttpClient GetHttpClient()
		{
			return new HttpClient(new HttpPipelineMessageHandler(this._pipeline));
		}

		// Token: 0x0400020A RID: 522
		private readonly HttpPipeline _pipeline;
	}
}
