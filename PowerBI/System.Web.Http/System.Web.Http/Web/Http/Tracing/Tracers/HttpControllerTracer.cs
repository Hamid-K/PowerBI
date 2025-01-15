using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200012D RID: 301
	internal class HttpControllerTracer : IHttpController, IDisposable, IDecorator<IHttpController>
	{
		// Token: 0x060007F8 RID: 2040 RVA: 0x00014251 File Offset: 0x00012451
		public HttpControllerTracer(HttpRequestMessage request, IHttpController innerController, ITraceWriter traceWriter)
		{
			this._innerController = innerController;
			this._request = request;
			this._traceWriter = traceWriter;
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0001426E File Offset: 0x0001246E
		public IHttpController Inner
		{
			get
			{
				return this._innerController;
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00014278 File Offset: 0x00012478
		void IDisposable.Dispose()
		{
			IDisposable disposable = this._innerController as IDisposable;
			if (disposable != null)
			{
				this._traceWriter.TraceBeginEnd(this._request, TraceCategories.ControllersCategory, TraceLevel.Info, this._innerController.GetType().Name, "Dispose", null, new Action(disposable.Dispose), null, null);
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x000142D0 File Offset: 0x000124D0
		Task<HttpResponseMessage> IHttpController.ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
		{
			return this._traceWriter.TraceBeginEndAsync(controllerContext.Request, TraceCategories.ControllersCategory, TraceLevel.Info, this._innerController.GetType().Name, "ExecuteAsync", null, delegate
			{
				controllerContext.Controller = HttpControllerTracer.ActualController(controllerContext.Controller);
				return this.ExecuteAsyncCore(controllerContext, cancellationToken);
			}, delegate(TraceRecord tr, HttpResponseMessage response)
			{
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, null);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00014358 File Offset: 0x00012558
		private async Task<HttpResponseMessage> ExecuteAsyncCore(HttpControllerContext controllerContext, CancellationToken cancellationToken)
		{
			HttpResponseMessage httpResponseMessage;
			try
			{
				httpResponseMessage = await this._innerController.ExecuteAsync(controllerContext, cancellationToken);
			}
			finally
			{
				IDisposable disposable = this._innerController as IDisposable;
				IList<IDisposable> list;
				if (disposable != null && this._request.Properties.TryGetValue(HttpPropertyKeys.DisposableRequestResourcesKey, out list))
				{
					list.Remove(disposable);
					list.Add(this);
				}
			}
			return httpResponseMessage;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000143B0 File Offset: 0x000125B0
		public static IHttpController ActualController(IHttpController controller)
		{
			HttpControllerTracer httpControllerTracer = controller as HttpControllerTracer;
			if (httpControllerTracer != null)
			{
				return httpControllerTracer._innerController;
			}
			return controller;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x000143CF File Offset: 0x000125CF
		public static Type ActualControllerType(IHttpController controller)
		{
			return HttpControllerTracer.ActualController(controller).GetType();
		}

		// Token: 0x04000226 RID: 550
		private const string DisposeMethodName = "Dispose";

		// Token: 0x04000227 RID: 551
		private const string ExecuteAsyncMethodName = "ExecuteAsync";

		// Token: 0x04000228 RID: 552
		private readonly IHttpController _innerController;

		// Token: 0x04000229 RID: 553
		private readonly HttpRequestMessage _request;

		// Token: 0x0400022A RID: 554
		private readonly ITraceWriter _traceWriter;
	}
}
