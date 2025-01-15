using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200000A RID: 10
	public static class PerformanceUtilities
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000234B File Offset: 0x0000054B
		public static long ExecuteAndMeasureDurationMs(Action action)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			action();
			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}
	}
}
