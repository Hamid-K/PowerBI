using System;
using System.Diagnostics;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001CC RID: 460
	internal struct MeasureDurationResult
	{
		// Token: 0x06001447 RID: 5191 RVA: 0x00045078 File Offset: 0x00043278
		public MeasureDurationResult(long ticks)
		{
			this.Milliseconds = (long)((double)ticks * MeasureDurationResult.s_tickFrequency / 10000.0 % 1000.0);
			this.Microseconds = (long)((double)ticks * MeasureDurationResult.s_tickFrequency / 10.0 % 1000.0);
			this.Ticks = ticks;
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x000450D2 File Offset: 0x000432D2
		public readonly long Milliseconds { get; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x000450DA File Offset: 0x000432DA
		public readonly long Microseconds { get; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x000450E2 File Offset: 0x000432E2
		public readonly long Ticks { get; }

		// Token: 0x0400084A RID: 2122
		private const int TicksPerMicrosecond = 10;

		// Token: 0x0400084B RID: 2123
		private static readonly double s_tickFrequency = 10000000.0 / (double)Stopwatch.Frequency;
	}
}
