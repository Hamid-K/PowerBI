using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x020000B1 RID: 177
	public class UnauthorizedResult : IHttpActionResult
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x0000C45C File Offset: 0x0000A65C
		public UnauthorizedResult(IEnumerable<AuthenticationHeaderValue> challenges, HttpRequestMessage request)
			: this(challenges, new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000C46B File Offset: 0x0000A66B
		public UnauthorizedResult(IEnumerable<AuthenticationHeaderValue> challenges, ApiController controller)
			: this(challenges, new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000C47A File Offset: 0x0000A67A
		private UnauthorizedResult(IEnumerable<AuthenticationHeaderValue> challenges, StatusCodeResult.IDependencyProvider dependencies)
		{
			if (challenges == null)
			{
				throw new ArgumentNullException("challenges");
			}
			this._challenges = challenges;
			this._dependencies = dependencies;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000C49E File Offset: 0x0000A69E
		public IEnumerable<AuthenticationHeaderValue> Challenges
		{
			get
			{
				return this._challenges;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000C4A6 File Offset: 0x0000A6A6
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000C4B3 File Offset: 0x0000A6B3
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000C4C0 File Offset: 0x0000A6C0
		private HttpResponseMessage Execute()
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
			try
			{
				foreach (AuthenticationHeaderValue authenticationHeaderValue in this._challenges)
				{
					httpResponseMessage.Headers.WwwAuthenticate.Add(authenticationHeaderValue);
				}
				httpResponseMessage.RequestMessage = this._dependencies.Request;
			}
			catch
			{
				httpResponseMessage.Dispose();
				throw;
			}
			return httpResponseMessage;
		}

		// Token: 0x04000110 RID: 272
		private readonly IEnumerable<AuthenticationHeaderValue> _challenges;

		// Token: 0x04000111 RID: 273
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
