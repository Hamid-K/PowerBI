using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x020000CF RID: 207
	public interface IAuthorizationFilter : IFilter
	{
		// Token: 0x0600057A RID: 1402
		Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation);
	}
}
