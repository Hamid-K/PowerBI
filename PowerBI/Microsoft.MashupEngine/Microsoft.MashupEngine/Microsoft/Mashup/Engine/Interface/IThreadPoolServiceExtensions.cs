using System;
using System.Threading;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000E4 RID: 228
	public static class IThreadPoolServiceExtensions
	{
		// Token: 0x06000357 RID: 855 RVA: 0x00004963 File Offset: 0x00002B63
		public static void QueueUserWorkItem(this IThreadPoolService threadPool, WaitCallback callback)
		{
			threadPool.QueueUserWorkItem(callback, null);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00004970 File Offset: 0x00002B70
		public static void StartThread(this IThreadPoolService threadPool, ThreadStart threadStart)
		{
			threadPool.StartThread(delegate(object state)
			{
				threadStart();
			}, null);
		}
	}
}
