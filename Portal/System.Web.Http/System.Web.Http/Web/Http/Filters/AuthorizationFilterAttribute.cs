using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x020000C7 RID: 199
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public abstract class AuthorizationFilterAttribute : FilterAttribute, IAuthorizationFilter, IFilter
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x00005744 File Offset: 0x00003944
		public virtual void OnAuthorization(HttpActionContext actionContext)
		{
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000DE40 File Offset: 0x0000C040
		public virtual Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			try
			{
				this.OnAuthorization(actionContext);
			}
			catch (Exception ex)
			{
				return TaskHelpers.FromError(ex);
			}
			return TaskHelpers.Completed();
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000DE78 File Offset: 0x0000C078
		Task<HttpResponseMessage> IAuthorizationFilter.ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			if (continuation == null)
			{
				throw Error.ArgumentNull("continuation");
			}
			return this.ExecuteAuthorizationFilterAsyncCore(actionContext, cancellationToken, continuation);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000DEA0 File Offset: 0x0000C0A0
		private async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsyncCore(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			await this.OnAuthorizationAsync(actionContext, cancellationToken);
			HttpResponseMessage httpResponseMessage;
			if (actionContext.Response != null)
			{
				httpResponseMessage = actionContext.Response;
			}
			else
			{
				httpResponseMessage = await continuation();
			}
			return httpResponseMessage;
		}
	}
}
