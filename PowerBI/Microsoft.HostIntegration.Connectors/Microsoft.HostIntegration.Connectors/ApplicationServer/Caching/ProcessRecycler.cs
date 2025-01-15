using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200035B RID: 859
	internal class ProcessRecycler
	{
		// Token: 0x06001E35 RID: 7733 RVA: 0x0005A6A8 File Offset: 0x000588A8
		internal static void RegisterRecycleCallback(EventHandler handler)
		{
			if (ProcessRecycler._processRecycle != null)
			{
				throw new InvalidOperationException("Can be set only once.");
			}
			ProcessRecycler._processRecycle = (EventHandler)Delegate.Combine(ProcessRecycler._processRecycle, handler);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0005A6D4 File Offset: 0x000588D4
		internal static void Recycle()
		{
			EventHandler processRecycle = ProcessRecycler._processRecycle;
			if (processRecycle != null)
			{
				processRecycle(null, null);
				return;
			}
			Environment.Exit(-1);
		}

		// Token: 0x040010FF RID: 4351
		private static EventHandler _processRecycle;
	}
}
