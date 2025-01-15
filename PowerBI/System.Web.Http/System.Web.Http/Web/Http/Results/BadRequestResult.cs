using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x020000AA RID: 170
	public class BadRequestResult : IHttpActionResult
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x0000BD7D File Offset: 0x00009F7D
		public BadRequestResult(HttpRequestMessage request)
			: this(new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000BD8B File Offset: 0x00009F8B
		public BadRequestResult(ApiController controller)
			: this(new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000BD99 File Offset: 0x00009F99
		private BadRequestResult(StatusCodeResult.IDependencyProvider dependencies)
		{
			this._dependencies = dependencies;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000BDA8 File Offset: 0x00009FA8
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000BDB5 File Offset: 0x00009FB5
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(StatusCodeResult.Execute(HttpStatusCode.BadRequest, this._dependencies.Request));
		}

		// Token: 0x040000FF RID: 255
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
