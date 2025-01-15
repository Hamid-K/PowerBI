using System;
using System.Web.Http.Batch;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000DD RID: 221
	public static class ExceptionCatchBlocks
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0000E74F File Offset: 0x0000C94F
		public static ExceptionContextCatchBlock HttpBatchHandler
		{
			get
			{
				return ExceptionCatchBlocks._httpBatchHandler;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000E756 File Offset: 0x0000C956
		public static ExceptionContextCatchBlock HttpControllerDispatcher
		{
			get
			{
				return ExceptionCatchBlocks._httpControllerDispatcher;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000E75D File Offset: 0x0000C95D
		public static ExceptionContextCatchBlock HttpServer
		{
			get
			{
				return ExceptionCatchBlocks._httpServer;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000E764 File Offset: 0x0000C964
		public static ExceptionContextCatchBlock IExceptionFilter
		{
			get
			{
				return ExceptionCatchBlocks._exceptionFilter;
			}
		}

		// Token: 0x0400014B RID: 331
		private static readonly ExceptionContextCatchBlock _httpBatchHandler = new ExceptionContextCatchBlock(typeof(HttpBatchHandler).Name, false, true);

		// Token: 0x0400014C RID: 332
		private static readonly ExceptionContextCatchBlock _httpControllerDispatcher = new ExceptionContextCatchBlock(typeof(HttpControllerDispatcher).Name, false, true);

		// Token: 0x0400014D RID: 333
		private static readonly ExceptionContextCatchBlock _httpServer = new ExceptionContextCatchBlock(typeof(HttpServer).Name, true, true);

		// Token: 0x0400014E RID: 334
		private static readonly ExceptionContextCatchBlock _exceptionFilter = new ExceptionContextCatchBlock(typeof(IExceptionFilter).Name, false, true);
	}
}
