using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000D8 RID: 216
	public class ExceptionContext
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x0000E3D9 File Offset: 0x0000C5D9
		public ExceptionContext(Exception exception, ExceptionContextCatchBlock catchBlock)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.Exception = exception;
			if (catchBlock == null)
			{
				throw new ArgumentNullException("catchBlock");
			}
			this.CatchBlock = catchBlock;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000E40C File Offset: 0x0000C60C
		public ExceptionContext(Exception exception, ExceptionContextCatchBlock catchBlock, HttpActionContext actionContext)
			: this(exception, catchBlock)
		{
			if (actionContext == null)
			{
				throw new ArgumentNullException("actionContext");
			}
			this.ActionContext = actionContext;
			HttpControllerContext controllerContext = actionContext.ControllerContext;
			if (controllerContext == null)
			{
				throw new ArgumentException(Error.Format(SRResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(HttpActionContext).Name,
					"ControllerContext"
				}), "actionContext");
			}
			this.ControllerContext = controllerContext;
			HttpRequestContext requestContext = controllerContext.RequestContext;
			this.RequestContext = requestContext;
			HttpRequestMessage request = controllerContext.Request;
			if (request == null)
			{
				throw new ArgumentException(Error.Format(SRResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(HttpControllerContext).Name,
					"Request"
				}), "actionContext");
			}
			this.Request = request;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		public ExceptionContext(Exception exception, ExceptionContextCatchBlock catchBlock, HttpRequestMessage request)
			: this(exception, catchBlock)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this.Request = request;
			this.RequestContext = request.GetRequestContext();
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		public ExceptionContext(Exception exception, ExceptionContextCatchBlock catchBlock, HttpRequestMessage request, HttpResponseMessage response)
			: this(exception, catchBlock)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this.Request = request;
			this.RequestContext = request.GetRequestContext();
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			this.Response = response;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000E549 File Offset: 0x0000C749
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x0000E551 File Offset: 0x0000C751
		public Exception Exception { get; private set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000E55A File Offset: 0x0000C75A
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x0000E562 File Offset: 0x0000C762
		public ExceptionContextCatchBlock CatchBlock { get; private set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000E56B File Offset: 0x0000C76B
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x0000E573 File Offset: 0x0000C773
		public HttpRequestMessage Request { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000E57C File Offset: 0x0000C77C
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x0000E584 File Offset: 0x0000C784
		public HttpRequestContext RequestContext { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000E58D File Offset: 0x0000C78D
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0000E595 File Offset: 0x0000C795
		public HttpControllerContext ControllerContext { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000E59E File Offset: 0x0000C79E
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0000E5A6 File Offset: 0x0000C7A6
		public HttpActionContext ActionContext { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000E5AF File Offset: 0x0000C7AF
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0000E5B7 File Offset: 0x0000C7B7
		public HttpResponseMessage Response { get; set; }
	}
}
