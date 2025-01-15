using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200013D RID: 317
	internal class RequestMessageHandlerTracer : DelegatingHandler
	{
		// Token: 0x06000892 RID: 2194 RVA: 0x000157D9 File Offset: 0x000139D9
		public RequestMessageHandlerTracer(ITraceWriter traceWriter)
		{
			this._traceWriter = traceWriter;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x000157E8 File Offset: 0x000139E8
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return this._traceWriter.TraceBeginEndAsync(request, TraceCategories.RequestCategory, TraceLevel.Info, string.Empty, string.Empty, delegate(TraceRecord tr)
			{
				tr.Message = ((request.RequestUri == null) ? SRResources.TraceNoneObjectMessage : request.RequestUri.ToString());
			}, () => this.<>n__0(request, cancellationToken), delegate(TraceRecord tr, HttpResponseMessage response)
			{
				MediaTypeHeaderValue mediaTypeHeaderValue = ((response == null) ? null : ((response.Content == null) ? null : response.Content.Headers.ContentType));
				long? num = ((response == null) ? null : ((response.Content == null) ? null : response.Content.Headers.ContentLength));
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
				tr.Message = Error.Format(SRResources.TraceRequestCompleteMessage, new object[]
				{
					(mediaTypeHeaderValue == null) ? SRResources.TraceNoneObjectMessage : mediaTypeHeaderValue.ToString(),
					(num != null) ? num.Value.ToString(CultureInfo.CurrentCulture) : SRResources.TraceUnknownMessage
				});
			}, null);
		}

		// Token: 0x04000254 RID: 596
		private readonly ITraceWriter _traceWriter;
	}
}
