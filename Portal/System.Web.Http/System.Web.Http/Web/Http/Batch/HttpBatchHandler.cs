using System;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Hosting;

namespace System.Web.Http.Batch
{
	// Token: 0x02000111 RID: 273
	public abstract class HttpBatchHandler : HttpMessageHandler
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x00011F36 File Offset: 0x00010136
		protected HttpBatchHandler(HttpServer httpServer)
		{
			if (httpServer == null)
			{
				throw Error.ArgumentNull("httpServer");
			}
			this._server = httpServer;
			this.Invoker = new HttpMessageInvoker(httpServer);
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00011F5F File Offset: 0x0001015F
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x00011F67 File Offset: 0x00010167
		public HttpMessageInvoker Invoker { get; private set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x00011F70 File Offset: 0x00010170
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x00011F7D File Offset: 0x0001017D
		internal IExceptionLogger ExceptionLogger
		{
			get
			{
				return this._server.ExceptionLogger;
			}
			set
			{
				this._server.ExceptionLogger = value;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00011F8B File Offset: 0x0001018B
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x00011F98 File Offset: 0x00010198
		internal IExceptionHandler ExceptionHandler
		{
			get
			{
				return this._server.ExceptionHandler;
			}
			set
			{
				this._server.ExceptionHandler = value;
			}
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00011FA8 File Offset: 0x000101A8
		protected sealed override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			request.Properties[HttpPropertyKeys.IsBatchRequest] = true;
			ExceptionDispatchInfo exceptionInfo;
			try
			{
				return await this.ProcessBatchAsync(request, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (HttpResponseException ex)
			{
				return ex.Response;
			}
			catch (Exception ex2)
			{
				exceptionInfo = ExceptionDispatchInfo.Capture(ex2);
			}
			ExceptionContext exceptionContext = new ExceptionContext(exceptionInfo.SourceException, ExceptionCatchBlocks.HttpBatchHandler, request);
			await this.ExceptionLogger.LogAsync(exceptionContext, cancellationToken);
			HttpResponseMessage httpResponseMessage = await this.ExceptionHandler.HandleAsync(exceptionContext, cancellationToken);
			if (httpResponseMessage == null)
			{
				exceptionInfo.Throw();
			}
			return httpResponseMessage;
		}

		// Token: 0x0600074F RID: 1871
		public abstract Task<HttpResponseMessage> ProcessBatchAsync(HttpRequestMessage request, CancellationToken cancellationToken);

		// Token: 0x040001D7 RID: 471
		private readonly HttpServer _server;
	}
}
