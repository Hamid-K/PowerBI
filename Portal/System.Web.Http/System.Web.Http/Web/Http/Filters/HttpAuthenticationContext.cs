using System;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x020000C0 RID: 192
	public class HttpAuthenticationContext
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x0000DA86 File Offset: 0x0000BC86
		public HttpAuthenticationContext(HttpActionContext actionContext, IPrincipal principal)
		{
			if (actionContext == null)
			{
				throw new ArgumentNullException("actionContext");
			}
			this.ActionContext = actionContext;
			this.Principal = principal;
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0000DAAA File Offset: 0x0000BCAA
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x0000DAB2 File Offset: 0x0000BCB2
		public HttpActionContext ActionContext { get; private set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0000DABB File Offset: 0x0000BCBB
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x0000DAC3 File Offset: 0x0000BCC3
		public IPrincipal Principal { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0000DACC File Offset: 0x0000BCCC
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		public IHttpActionResult ErrorResult { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0000DADD File Offset: 0x0000BCDD
		public HttpRequestMessage Request
		{
			get
			{
				return this.ActionContext.Request;
			}
		}
	}
}
