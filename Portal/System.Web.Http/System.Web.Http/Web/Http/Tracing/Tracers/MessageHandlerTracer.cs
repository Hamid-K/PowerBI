using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200013C RID: 316
	internal class MessageHandlerTracer : DelegatingHandler, IDecorator<DelegatingHandler>
	{
		// Token: 0x0600088E RID: 2190 RVA: 0x0001573A File Offset: 0x0001393A
		public MessageHandlerTracer(DelegatingHandler innerHandler, ITraceWriter traceWriter)
		{
			this._innerHandler = innerHandler;
			this._traceWriter = traceWriter;
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00015750 File Offset: 0x00013950
		public DelegatingHandler Inner
		{
			get
			{
				return this._innerHandler;
			}
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00015758 File Offset: 0x00013958
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return this._traceWriter.TraceBeginEndAsync(request, TraceCategories.MessageHandlersCategory, TraceLevel.Info, this._innerHandler.GetType().Name, "SendAsync", null, () => this.<>n__0(request, cancellationToken), delegate(TraceRecord tr, HttpResponseMessage response)
			{
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, null);
		}

		// Token: 0x04000251 RID: 593
		private const string SendAsyncMethodName = "SendAsync";

		// Token: 0x04000252 RID: 594
		private readonly DelegatingHandler _innerHandler;

		// Token: 0x04000253 RID: 595
		private readonly ITraceWriter _traceWriter;
	}
}
