using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x020000B5 RID: 181
	public class NotFoundResult : IHttpActionResult
	{
		// Token: 0x0600045D RID: 1117 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		public NotFoundResult(HttpRequestMessage request)
			: this(new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000C6E2 File Offset: 0x0000A8E2
		public NotFoundResult(ApiController controller)
			: this(new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
		private NotFoundResult(StatusCodeResult.IDependencyProvider dependencies)
		{
			this._dependencies = dependencies;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000C6FF File Offset: 0x0000A8FF
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000C70C File Offset: 0x0000A90C
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(StatusCodeResult.Execute(HttpStatusCode.NotFound, this._dependencies.Request));
		}

		// Token: 0x04000116 RID: 278
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
