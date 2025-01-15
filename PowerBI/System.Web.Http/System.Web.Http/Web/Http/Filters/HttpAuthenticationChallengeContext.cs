using System;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x020000BF RID: 191
	public class HttpAuthenticationChallengeContext
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x0000DA17 File Offset: 0x0000BC17
		public HttpAuthenticationChallengeContext(HttpActionContext actionContext, IHttpActionResult result)
		{
			if (actionContext == null)
			{
				throw new ArgumentNullException("actionContext");
			}
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			this.ActionContext = actionContext;
			this.Result = result;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0000DA49 File Offset: 0x0000BC49
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x0000DA51 File Offset: 0x0000BC51
		public HttpActionContext ActionContext { get; private set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0000DA5A File Offset: 0x0000BC5A
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x0000DA62 File Offset: 0x0000BC62
		public IHttpActionResult Result
		{
			get
			{
				return this._result;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._result = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0000DA79 File Offset: 0x0000BC79
		public HttpRequestMessage Request
		{
			get
			{
				return this.ActionContext.Request;
			}
		}

		// Token: 0x0400012D RID: 301
		private IHttpActionResult _result;
	}
}
