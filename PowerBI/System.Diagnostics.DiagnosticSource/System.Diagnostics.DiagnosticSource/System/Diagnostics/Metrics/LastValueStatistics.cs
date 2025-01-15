using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000048 RID: 72
	internal sealed class LastValueStatistics : IAggregationStatistics
	{
		// Token: 0x06000231 RID: 561 RVA: 0x0000993C File Offset: 0x00007B3C
		internal LastValueStatistics(double? lastValue)
		{
			this.LastValue = lastValue;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000994B File Offset: 0x00007B4B
		public double? LastValue { get; }
	}
}
