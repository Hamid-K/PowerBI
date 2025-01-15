using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x020000B3 RID: 179
	public class OkResult : IHttpActionResult
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x0000C62C File Offset: 0x0000A82C
		public OkResult(HttpRequestMessage request)
			: this(new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000C63A File Offset: 0x0000A83A
		public OkResult(ApiController controller)
			: this(new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000C648 File Offset: 0x0000A848
		private OkResult(StatusCodeResult.IDependencyProvider dependencies)
		{
			this._dependencies = dependencies;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000C657 File Offset: 0x0000A857
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000C664 File Offset: 0x0000A864
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(StatusCodeResult.Execute(HttpStatusCode.OK, this._dependencies.Request));
		}

		// Token: 0x04000114 RID: 276
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
