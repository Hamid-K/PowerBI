using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x020000AC RID: 172
	public class RedirectResult : IHttpActionResult
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		public RedirectResult(Uri location, HttpRequestMessage request)
			: this(location, new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000BEEB File Offset: 0x0000A0EB
		public RedirectResult(Uri location, ApiController controller)
			: this(location, new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000BEFA File Offset: 0x0000A0FA
		private RedirectResult(Uri location, StatusCodeResult.IDependencyProvider dependencies)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			this._location = location;
			this._dependencies = dependencies;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000BF24 File Offset: 0x0000A124
		public Uri Location
		{
			get
			{
				return this._location;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000BF2C File Offset: 0x0000A12C
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000BF39 File Offset: 0x0000A139
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000BF48 File Offset: 0x0000A148
		private HttpResponseMessage Execute()
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Found);
			try
			{
				httpResponseMessage.Headers.Location = this._location;
				httpResponseMessage.RequestMessage = this._dependencies.Request;
			}
			catch
			{
				httpResponseMessage.Dispose();
				throw;
			}
			return httpResponseMessage;
		}

		// Token: 0x04000103 RID: 259
		private readonly Uri _location;

		// Token: 0x04000104 RID: 260
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
