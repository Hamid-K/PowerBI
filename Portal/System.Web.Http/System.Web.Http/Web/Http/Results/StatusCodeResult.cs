using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;

namespace System.Web.Http.Results
{
	// Token: 0x020000BB RID: 187
	public class StatusCodeResult : IHttpActionResult
	{
		// Token: 0x06000494 RID: 1172 RVA: 0x0000CC48 File Offset: 0x0000AE48
		public StatusCodeResult(HttpStatusCode statusCode, HttpRequestMessage request)
			: this(statusCode, new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000CC57 File Offset: 0x0000AE57
		public StatusCodeResult(HttpStatusCode statusCode, ApiController controller)
			: this(statusCode, new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000CC66 File Offset: 0x0000AE66
		private StatusCodeResult(HttpStatusCode statusCode, StatusCodeResult.IDependencyProvider dependencies)
		{
			this._statusCode = statusCode;
			this._dependencies = dependencies;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000CC7C File Offset: 0x0000AE7C
		public HttpStatusCode StatusCode
		{
			get
			{
				return this._statusCode;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000CC84 File Offset: 0x0000AE84
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000CC91 File Offset: 0x0000AE91
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000CC9E File Offset: 0x0000AE9E
		private HttpResponseMessage Execute()
		{
			return StatusCodeResult.Execute(this._statusCode, this._dependencies.Request);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000CCB8 File Offset: 0x0000AEB8
		internal static HttpResponseMessage Execute(HttpStatusCode statusCode, HttpRequestMessage request)
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(statusCode);
			try
			{
				httpResponseMessage.RequestMessage = request;
			}
			catch
			{
				httpResponseMessage.Dispose();
				throw;
			}
			return httpResponseMessage;
		}

		// Token: 0x04000126 RID: 294
		private readonly HttpStatusCode _statusCode;

		// Token: 0x04000127 RID: 295
		private readonly StatusCodeResult.IDependencyProvider _dependencies;

		// Token: 0x020001D8 RID: 472
		internal interface IDependencyProvider
		{
			// Token: 0x1700031B RID: 795
			// (get) Token: 0x06000B40 RID: 2880
			HttpRequestMessage Request { get; }
		}

		// Token: 0x020001D9 RID: 473
		internal sealed class DirectDependencyProvider : StatusCodeResult.IDependencyProvider
		{
			// Token: 0x06000B41 RID: 2881 RVA: 0x0001CCE4 File Offset: 0x0001AEE4
			public DirectDependencyProvider(HttpRequestMessage request)
			{
				if (request == null)
				{
					throw new ArgumentNullException("request");
				}
				this._request = request;
			}

			// Token: 0x1700031C RID: 796
			// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0001CD01 File Offset: 0x0001AF01
			public HttpRequestMessage Request
			{
				get
				{
					return this._request;
				}
			}

			// Token: 0x04000398 RID: 920
			private readonly HttpRequestMessage _request;
		}

		// Token: 0x020001DA RID: 474
		internal sealed class ApiControllerDependencyProvider : StatusCodeResult.IDependencyProvider
		{
			// Token: 0x06000B43 RID: 2883 RVA: 0x0001CD09 File Offset: 0x0001AF09
			public ApiControllerDependencyProvider(ApiController controller)
			{
				if (controller == null)
				{
					throw new ArgumentNullException("controller");
				}
				this._controller = controller;
			}

			// Token: 0x1700031D RID: 797
			// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0001CD26 File Offset: 0x0001AF26
			public HttpRequestMessage Request
			{
				get
				{
					this.EnsureResolved();
					return this._request;
				}
			}

			// Token: 0x06000B45 RID: 2885 RVA: 0x0001CD34 File Offset: 0x0001AF34
			private void EnsureResolved()
			{
				if (this._request == null)
				{
					HttpRequestMessage request = this._controller.Request;
					if (request == null)
					{
						throw new InvalidOperationException(SRResources.ApiController_RequestMustNotBeNull);
					}
					this._request = request;
				}
			}

			// Token: 0x04000399 RID: 921
			private readonly ApiController _controller;

			// Token: 0x0400039A RID: 922
			private HttpRequestMessage _request;
		}
	}
}
