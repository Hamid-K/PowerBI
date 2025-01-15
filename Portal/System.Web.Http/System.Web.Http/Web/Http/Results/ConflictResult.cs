using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x020000B0 RID: 176
	public class ConflictResult : IHttpActionResult
	{
		// Token: 0x0600043D RID: 1085 RVA: 0x0000C408 File Offset: 0x0000A608
		public ConflictResult(HttpRequestMessage request)
			: this(new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000C416 File Offset: 0x0000A616
		public ConflictResult(ApiController controller)
			: this(new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000C424 File Offset: 0x0000A624
		private ConflictResult(StatusCodeResult.IDependencyProvider dependencies)
		{
			this._dependencies = dependencies;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000C433 File Offset: 0x0000A633
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000C440 File Offset: 0x0000A640
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(StatusCodeResult.Execute(HttpStatusCode.Conflict, this._dependencies.Request));
		}

		// Token: 0x0400010F RID: 271
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
