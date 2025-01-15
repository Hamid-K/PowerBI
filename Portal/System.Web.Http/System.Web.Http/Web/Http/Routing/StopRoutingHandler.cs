using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Routing
{
	// Token: 0x02000159 RID: 345
	public sealed class StopRoutingHandler : HttpMessageHandler
	{
		// Token: 0x0600094C RID: 2380 RVA: 0x00017A31 File Offset: 0x00015C31
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}
	}
}
