using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000EE RID: 238
	internal class AuthorizationFilterResult : IHttpActionResult
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x0000FC61 File Offset: 0x0000DE61
		public AuthorizationFilterResult(HttpActionContext context, IAuthorizationFilter[] filters, IHttpActionResult innerResult)
		{
			this._context = context;
			this._filters = filters;
			this._innerResult = innerResult;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0000FC80 File Offset: 0x0000DE80
		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			Func<Task<HttpResponseMessage>> func = () => this._innerResult.ExecuteAsync(cancellationToken);
			Func<Func<Task<HttpResponseMessage>>, IAuthorizationFilter, Func<Task<HttpResponseMessage>>> <>9__1;
			for (int i = this._filters.Length - 1; i >= 0; i--)
			{
				IAuthorizationFilter authorizationFilter = this._filters[i];
				Func<Func<Task<HttpResponseMessage>>, IAuthorizationFilter, Func<Task<HttpResponseMessage>>> func2;
				if ((func2 = <>9__1) == null)
				{
					func2 = (<>9__1 = (Func<Task<HttpResponseMessage>> continuation, IAuthorizationFilter innerFilter) => () => innerFilter.ExecuteAuthorizationFilterAsync(this._context, cancellationToken, continuation));
				}
				func = func2(func, authorizationFilter);
			}
			return func();
		}

		// Token: 0x04000174 RID: 372
		private readonly HttpActionContext _context;

		// Token: 0x04000175 RID: 373
		private readonly IAuthorizationFilter[] _filters;

		// Token: 0x04000176 RID: 374
		private readonly IHttpActionResult _innerResult;
	}
}
