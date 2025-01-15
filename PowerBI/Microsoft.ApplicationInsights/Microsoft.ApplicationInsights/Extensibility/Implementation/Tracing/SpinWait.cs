using System;
using System.Threading;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x020000A4 RID: 164
	internal static class SpinWait
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x00015168 File Offset: 0x00013368
		internal static void ExecuteSpinWaitLock(this object syncRoot, Action action)
		{
			while (!Monitor.TryEnter(syncRoot, 0))
			{
			}
			try
			{
				action();
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
		}
	}
}
