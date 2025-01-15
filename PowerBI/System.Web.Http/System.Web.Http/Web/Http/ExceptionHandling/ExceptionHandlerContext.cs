using System;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000DA RID: 218
	public class ExceptionHandlerContext
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x0000E61C File Offset: 0x0000C81C
		public ExceptionHandlerContext(ExceptionContext exceptionContext)
		{
			if (exceptionContext == null)
			{
				throw new ArgumentNullException("exceptionContext");
			}
			this._exceptionContext = exceptionContext;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000E639 File Offset: 0x0000C839
		public ExceptionContext ExceptionContext
		{
			get
			{
				return this._exceptionContext;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000E641 File Offset: 0x0000C841
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x0000E649 File Offset: 0x0000C849
		public IHttpActionResult Result { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000E652 File Offset: 0x0000C852
		public Exception Exception
		{
			get
			{
				return this._exceptionContext.Exception;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000E65F File Offset: 0x0000C85F
		public ExceptionContextCatchBlock CatchBlock
		{
			get
			{
				return this._exceptionContext.CatchBlock;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0000E66C File Offset: 0x0000C86C
		public HttpRequestMessage Request
		{
			get
			{
				return this._exceptionContext.Request;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000E679 File Offset: 0x0000C879
		public HttpRequestContext RequestContext
		{
			get
			{
				return this._exceptionContext.RequestContext;
			}
		}

		// Token: 0x04000149 RID: 329
		private readonly ExceptionContext _exceptionContext;
	}
}
