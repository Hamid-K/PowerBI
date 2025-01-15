using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000129 RID: 297
	internal class HttpActionInvokerTracer : IHttpActionInvoker, IDecorator<IHttpActionInvoker>
	{
		// Token: 0x060007E7 RID: 2023 RVA: 0x00013EF1 File Offset: 0x000120F1
		public HttpActionInvokerTracer(IHttpActionInvoker innerInvoker, ITraceWriter traceWriter)
		{
			this._innerInvoker = innerInvoker;
			this._traceWriter = traceWriter;
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00013F07 File Offset: 0x00012107
		public IHttpActionInvoker Inner
		{
			get
			{
				return this._innerInvoker;
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00013F10 File Offset: 0x00012110
		Task<HttpResponseMessage> IHttpActionInvoker.InvokeActionAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			if (actionContext == null)
			{
				throw new ArgumentNullException("actionContext");
			}
			return this._traceWriter.TraceBeginEndAsync(actionContext.ControllerContext.Request, TraceCategories.ActionCategory, TraceLevel.Info, this._innerInvoker.GetType().Name, "InvokeActionAsync", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceActionInvokeMessage, new object[] { FormattingUtilities.ActionInvokeToString(actionContext) });
			}, () => this._innerInvoker.InvokeActionAsync(actionContext, cancellationToken), delegate(TraceRecord tr, HttpResponseMessage result)
			{
				if (result != null)
				{
					tr.Status = result.StatusCode;
				}
			}, null);
		}

		// Token: 0x0400021B RID: 539
		private const string InvokeActionAsyncMethodName = "InvokeActionAsync";

		// Token: 0x0400021C RID: 540
		private readonly IHttpActionInvoker _innerInvoker;

		// Token: 0x0400021D RID: 541
		private readonly ITraceWriter _traceWriter;
	}
}
