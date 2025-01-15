using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x020000B4 RID: 180
	public class InternalServerErrorResult : IHttpActionResult
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x0000C680 File Offset: 0x0000A880
		public InternalServerErrorResult(HttpRequestMessage request)
			: this(new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000C68E File Offset: 0x0000A88E
		public InternalServerErrorResult(ApiController controller)
			: this(new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000C69C File Offset: 0x0000A89C
		private InternalServerErrorResult(StatusCodeResult.IDependencyProvider dependencies)
		{
			this._dependencies = dependencies;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000C6AB File Offset: 0x0000A8AB
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(StatusCodeResult.Execute(HttpStatusCode.InternalServerError, this._dependencies.Request));
		}

		// Token: 0x04000115 RID: 277
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
