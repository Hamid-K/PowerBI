using System;

namespace Microsoft.HostIntegration.PerformanceCounters
{
	// Token: 0x02000791 RID: 1937
	public class UpDownCounter : CumulativeCounter
	{
		// Token: 0x06003E7C RID: 15996 RVA: 0x000D1A3B File Offset: 0x000CFC3B
		internal UpDownCounter(PerformanceCounterInformation performanceCounterInformation, string instanceName)
			: base(performanceCounterInformation, instanceName)
		{
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x000D1A45 File Offset: 0x000CFC45
		public void Decrement()
		{
			if (this.counter != null)
			{
				this.counter.Decrement();
			}
			if (this.commonCounter != null)
			{
				this.commonCounter.Decrement();
			}
		}
	}
}
