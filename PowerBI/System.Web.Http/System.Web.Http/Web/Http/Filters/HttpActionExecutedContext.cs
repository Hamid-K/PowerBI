using System;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x020000C3 RID: 195
	public class HttpActionExecutedContext
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x0000DAEA File Offset: 0x0000BCEA
		public HttpActionExecutedContext(HttpActionContext actionContext, Exception exception)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			this.Exception = exception;
			this._actionContext = actionContext;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00003AA7 File Offset: 0x00001CA7
		public HttpActionExecutedContext()
		{
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0000DB0E File Offset: 0x0000BD0E
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0000DB16 File Offset: 0x0000BD16
		public HttpActionContext ActionContext
		{
			get
			{
				return this._actionContext;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._actionContext = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0000DB28 File Offset: 0x0000BD28
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0000DB30 File Offset: 0x0000BD30
		public Exception Exception { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0000DB39 File Offset: 0x0000BD39
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x0000DB50 File Offset: 0x0000BD50
		public HttpResponseMessage Response
		{
			get
			{
				if (this.ActionContext == null)
				{
					return null;
				}
				return this.ActionContext.Response;
			}
			set
			{
				this.ActionContext.Response = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0000DB5E File Offset: 0x0000BD5E
		public HttpRequestMessage Request
		{
			get
			{
				if (this.ActionContext == null || this.ActionContext.ControllerContext == null)
				{
					return null;
				}
				return this.ActionContext.ControllerContext.Request;
			}
		}

		// Token: 0x04000132 RID: 306
		private HttpActionContext _actionContext;
	}
}
