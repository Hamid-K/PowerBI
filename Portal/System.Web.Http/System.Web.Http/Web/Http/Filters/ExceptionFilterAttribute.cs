using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Filters
{
	// Token: 0x020000C8 RID: 200
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public abstract class ExceptionFilterAttribute : FilterAttribute, IExceptionFilter, IFilter
	{
		// Token: 0x06000567 RID: 1383 RVA: 0x00005744 File Offset: 0x00003944
		public virtual void OnException(HttpActionExecutedContext actionExecutedContext)
		{
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000DF00 File Offset: 0x0000C100
		public virtual Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			try
			{
				this.OnException(actionExecutedContext);
			}
			catch (Exception ex)
			{
				return TaskHelpers.FromError(ex);
			}
			return TaskHelpers.Completed();
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000DF38 File Offset: 0x0000C138
		Task IExceptionFilter.ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			if (actionExecutedContext == null)
			{
				throw Error.ArgumentNull("actionExecutedContext");
			}
			return this.ExecuteExceptionFilterAsyncCore(actionExecutedContext, cancellationToken);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000DF50 File Offset: 0x0000C150
		private async Task ExecuteExceptionFilterAsyncCore(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			await this.OnExceptionAsync(actionExecutedContext, cancellationToken);
		}
	}
}
