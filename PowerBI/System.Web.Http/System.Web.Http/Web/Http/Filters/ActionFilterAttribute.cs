using System;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.Filters
{
	// Token: 0x020000C6 RID: 198
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public abstract class ActionFilterAttribute : FilterAttribute, IActionFilter, IFilter
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x00005744 File Offset: 0x00003944
		public virtual void OnActionExecuting(HttpActionContext actionContext)
		{
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00005744 File Offset: 0x00003944
		public virtual void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000DCE0 File Offset: 0x0000BEE0
		public virtual Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			try
			{
				this.OnActionExecuting(actionContext);
			}
			catch (Exception ex)
			{
				return TaskHelpers.FromError(ex);
			}
			return TaskHelpers.Completed();
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000DD18 File Offset: 0x0000BF18
		public virtual Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			try
			{
				this.OnActionExecuted(actionExecutedContext);
			}
			catch (Exception ex)
			{
				return TaskHelpers.FromError(ex);
			}
			return TaskHelpers.Completed();
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000DD50 File Offset: 0x0000BF50
		Task<HttpResponseMessage> IActionFilter.ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			if (continuation == null)
			{
				throw Error.ArgumentNull("continuation");
			}
			return this.ExecuteActionFilterAsyncCore(actionContext, cancellationToken, continuation);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000DD78 File Offset: 0x0000BF78
		private async Task<HttpResponseMessage> ExecuteActionFilterAsyncCore(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			await this.OnActionExecutingAsync(actionContext, cancellationToken);
			HttpResponseMessage httpResponseMessage;
			if (actionContext.Response != null)
			{
				httpResponseMessage = actionContext.Response;
			}
			else
			{
				httpResponseMessage = await this.CallOnActionExecutedAsync(actionContext, cancellationToken, continuation);
			}
			return httpResponseMessage;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		private async Task<HttpResponseMessage> CallOnActionExecutedAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			cancellationToken.ThrowIfCancellationRequested();
			HttpResponseMessage response = null;
			ExceptionDispatchInfo exceptionInfo = null;
			try
			{
				HttpResponseMessage httpResponseMessage = await continuation();
				response = httpResponseMessage;
			}
			catch (Exception ex)
			{
				exceptionInfo = ExceptionDispatchInfo.Capture(ex);
			}
			Exception exception;
			if (exceptionInfo == null)
			{
				exception = null;
			}
			else
			{
				exception = exceptionInfo.SourceException;
			}
			HttpActionExecutedContext executedContext = new HttpActionExecutedContext(actionContext, exception)
			{
				Response = response
			};
			try
			{
				await this.OnActionExecutedAsync(executedContext, cancellationToken);
			}
			catch
			{
				actionContext.Response = null;
				throw;
			}
			if (executedContext.Response != null)
			{
				return executedContext.Response;
			}
			Exception exception2 = executedContext.Exception;
			if (exception2 != null)
			{
				if (exception2 != exception)
				{
					throw exception2;
				}
				exceptionInfo.Throw();
			}
			throw Error.InvalidOperation(SRResources.ActionFilterAttribute_MustSupplyResponseOrException, new object[] { base.GetType().Name });
		}
	}
}
