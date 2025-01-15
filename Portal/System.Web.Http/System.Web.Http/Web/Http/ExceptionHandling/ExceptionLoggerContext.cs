using System;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000E0 RID: 224
	public class ExceptionLoggerContext
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x0000E7E5 File Offset: 0x0000C9E5
		public ExceptionLoggerContext(ExceptionContext exceptionContext)
		{
			if (exceptionContext == null)
			{
				throw new ArgumentNullException("exceptionContext");
			}
			this._exceptionContext = exceptionContext;
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0000E802 File Offset: 0x0000CA02
		public ExceptionContext ExceptionContext
		{
			get
			{
				return this._exceptionContext;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0000E80A File Offset: 0x0000CA0A
		public Exception Exception
		{
			get
			{
				return this._exceptionContext.Exception;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0000E817 File Offset: 0x0000CA17
		public ExceptionContextCatchBlock CatchBlock
		{
			get
			{
				return this._exceptionContext.CatchBlock;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000E824 File Offset: 0x0000CA24
		public HttpRequestMessage Request
		{
			get
			{
				return this._exceptionContext.Request;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000E831 File Offset: 0x0000CA31
		public HttpRequestContext RequestContext
		{
			get
			{
				return this._exceptionContext.RequestContext;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000E83E File Offset: 0x0000CA3E
		public bool CallsHandler
		{
			get
			{
				return this._exceptionContext.CatchBlock.CallsHandler;
			}
		}

		// Token: 0x0400014F RID: 335
		private readonly ExceptionContext _exceptionContext;
	}
}
