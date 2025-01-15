using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200010C RID: 268
	public interface IHttpController
	{
		// Token: 0x0600070D RID: 1805
		Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken);
	}
}
