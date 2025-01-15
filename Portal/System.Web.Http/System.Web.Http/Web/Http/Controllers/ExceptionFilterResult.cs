using System;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000EF RID: 239
	internal class ExceptionFilterResult : IHttpActionResult
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x0000FCFB File Offset: 0x0000DEFB
		public ExceptionFilterResult(HttpActionContext context, IExceptionFilter[] filters, IExceptionLogger exceptionLogger, IExceptionHandler exceptionHandler, IHttpActionResult innerResult)
		{
			this._context = context;
			this._filters = filters;
			this._exceptionLogger = exceptionLogger;
			this._exceptionHandler = exceptionHandler;
			this._innerResult = innerResult;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0000FD28 File Offset: 0x0000DF28
		public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			ExceptionDispatchInfo exceptionInfo;
			try
			{
				return await this._innerResult.ExecuteAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				exceptionInfo = ExceptionDispatchInfo.Capture(ex);
			}
			Exception exception = exceptionInfo.SourceException;
			bool isCancellationException = exception is OperationCanceledException;
			ExceptionContext exceptionContext = new ExceptionContext(exception, ExceptionCatchBlocks.IExceptionFilter, this._context);
			if (!isCancellationException)
			{
				await this._exceptionLogger.LogAsync(exceptionContext, cancellationToken);
			}
			HttpActionExecutedContext executedContext = new HttpActionExecutedContext(this._context, exception);
			for (int i = this._filters.Length - 1; i >= 0; i--)
			{
				await this._filters[i].ExecuteExceptionFilterAsync(executedContext, cancellationToken);
			}
			if (executedContext.Response == null && !isCancellationException)
			{
				HttpActionExecutedContext httpActionExecutedContext = executedContext;
				httpActionExecutedContext.Response = await this._exceptionHandler.HandleAsync(exceptionContext, cancellationToken);
				httpActionExecutedContext = null;
			}
			if (executedContext.Response == null)
			{
				if (exception == executedContext.Exception)
				{
					exceptionInfo.Throw();
				}
				throw executedContext.Exception;
			}
			return executedContext.Response;
		}

		// Token: 0x04000177 RID: 375
		private readonly HttpActionContext _context;

		// Token: 0x04000178 RID: 376
		private readonly IExceptionFilter[] _filters;

		// Token: 0x04000179 RID: 377
		private readonly IExceptionLogger _exceptionLogger;

		// Token: 0x0400017A RID: 378
		private readonly IExceptionHandler _exceptionHandler;

		// Token: 0x0400017B RID: 379
		private readonly IHttpActionResult _innerResult;
	}
}
