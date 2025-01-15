using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x020000A5 RID: 165
	internal class ThreadResourceLock : IDisposable
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x000151A0 File Offset: 0x000133A0
		public ThreadResourceLock()
		{
			ThreadResourceLock.syncObject = new object();
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000151B2 File Offset: 0x000133B2
		public static bool IsResourceLocked
		{
			get
			{
				return ThreadResourceLock.syncObject != null;
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000151BC File Offset: 0x000133BC
		public void Dispose()
		{
			ThreadResourceLock.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000151CA File Offset: 0x000133CA
		private static void Dispose(bool disponing)
		{
			if (disponing)
			{
				ThreadResourceLock.syncObject = null;
			}
		}

		// Token: 0x04000202 RID: 514
		[ThreadStatic]
		private static object syncObject;
	}
}
