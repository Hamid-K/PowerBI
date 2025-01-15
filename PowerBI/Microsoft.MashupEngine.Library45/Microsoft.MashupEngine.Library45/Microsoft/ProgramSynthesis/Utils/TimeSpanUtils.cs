using System;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000518 RID: 1304
	public static class TimeSpanUtils
	{
		// Token: 0x06001CFD RID: 7421 RVA: 0x00056743 File Offset: 0x00054943
		public static TimeSpan Multiply(this TimeSpan timeSpan, double multiplier)
		{
			return TimeSpan.FromTicks((long)Math.Ceiling((double)timeSpan.Ticks * multiplier));
		}
	}
}
