using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200050F RID: 1295
	public static class StopwatchUtils
	{
		// Token: 0x06001CCF RID: 7375 RVA: 0x00055CA4 File Offset: 0x00053EA4
		public static double ElapsedMillisecondsAsDouble(this Stopwatch stopwatch)
		{
			return (double)stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000.0;
		}
	}
}
