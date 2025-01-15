using System;
using System.Diagnostics;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001CB RID: 459
	internal struct MeasureDurationResult<TResult>
	{
		// Token: 0x06001441 RID: 5185 RVA: 0x00044FE8 File Offset: 0x000431E8
		public MeasureDurationResult(TResult result, long ticks)
		{
			this.Result = result;
			this.Milliseconds = (long)((double)ticks / MeasureDurationResult<TResult>.s_tickFrequency / 10000.0);
			this.Microseconds = (long)((double)ticks * MeasureDurationResult<TResult>.s_tickFrequency / 10.0 % 1000.0);
			this.Ticks = ticks;
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0004503F File Offset: 0x0004323F
		public readonly TResult Result { get; }

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x00045047 File Offset: 0x00043247
		public readonly long Milliseconds { get; }

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0004504F File Offset: 0x0004324F
		public readonly long Microseconds { get; }

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x00045057 File Offset: 0x00043257
		public readonly long Ticks { get; }

		// Token: 0x04000844 RID: 2116
		private const int TicksPerMicrosecond = 10;

		// Token: 0x04000845 RID: 2117
		private static readonly double s_tickFrequency = 10000000.0 / (double)Stopwatch.Frequency;
	}
}
