using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000103 RID: 259
	public interface IHttpActionInvoker
	{
		// Token: 0x060006D4 RID: 1748
		Task<HttpResponseMessage> InvokeActionAsync(HttpActionContext actionContext, CancellationToken cancellationToken);
	}
}
