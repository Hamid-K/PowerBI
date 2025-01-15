using System;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000ED RID: 237
	internal class AuthenticationFilterResult : IHttpActionResult
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x0000FBEE File Offset: 0x0000DDEE
		public AuthenticationFilterResult(HttpActionContext context, ApiController controller, IAuthenticationFilter[] filters, IHttpActionResult innerResult)
		{
			this._context = context;
			this._controller = controller;
			this._filters = filters;
			this._innerResult = innerResult;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0000FC14 File Offset: 0x0000DE14
		public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			IHttpActionResult result = this._innerResult;
			IPrincipal originalPrincipal = this._controller.User;
			HttpAuthenticationContext authenticationContext = new HttpAuthenticationContext(this._context, originalPrincipal);
			for (int i = 0; i < this._filters.Length; i++)
			{
				await this._filters[i].AuthenticateAsync(authenticationContext, cancellationToken);
				IHttpActionResult errorResult = authenticationContext.ErrorResult;
				if (errorResult != null)
				{
					result = errorResult;
					break;
				}
			}
			IPrincipal principal = authenticationContext.Principal;
			if (principal != originalPrincipal)
			{
				this._controller.User = principal;
			}
			HttpAuthenticationChallengeContext challengeContext = new HttpAuthenticationChallengeContext(this._context, result);
			for (int i = 0; i < this._filters.Length; i++)
			{
				await this._filters[i].ChallengeAsync(challengeContext, cancellationToken);
			}
			result = challengeContext.Result;
			return await result.ExecuteAsync(cancellationToken);
		}

		// Token: 0x04000170 RID: 368
		private readonly HttpActionContext _context;

		// Token: 0x04000171 RID: 369
		private readonly ApiController _controller;

		// Token: 0x04000172 RID: 370
		private readonly IAuthenticationFilter[] _filters;

		// Token: 0x04000173 RID: 371
		private readonly IHttpActionResult _innerResult;
	}
}
