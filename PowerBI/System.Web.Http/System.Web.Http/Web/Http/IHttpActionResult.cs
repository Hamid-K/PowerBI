using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http
{
	// Token: 0x02000014 RID: 20
	public interface IHttpActionResult
	{
		// Token: 0x0600008D RID: 141
		Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken);
	}
}
