using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ReportingServices.ProcessingRenderingCommon.Tracing;

namespace Microsoft.ReportingServices.ProcessingRenderingCommon.Common
{
	// Token: 0x020000DA RID: 218
	public sealed class TlsTracingHttpMessageHandler : DelegatingHandler
	{
		// Token: 0x0600078C RID: 1932 RVA: 0x0001432C File Offset: 0x0001252C
		public TlsTracingHttpMessageHandler()
			: base(new HttpClientHandler())
		{
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00014339 File Offset: 0x00012539
		public TlsTracingHttpMessageHandler(HttpMessageHandler handler)
			: base(handler)
		{
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00014344 File Offset: 0x00012544
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			HttpResponseMessage httpResponseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
			EngineTracer.Info("Outbound TLS Protocol (HttpClient): {0}", new object[] { TlsInspector.GetTlsProtocol(httpResponseMessage) });
			return httpResponseMessage;
		}
	}
}
