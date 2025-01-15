using System;

namespace Microsoft.HostIntegration.PerformanceCounters
{
	// Token: 0x02000790 RID: 1936
	public class CumulativeCounter : GenericPerformanceCounter
	{
		// Token: 0x06003E7A RID: 15994 RVA: 0x000D19D9 File Offset: 0x000CFBD9
		internal CumulativeCounter(PerformanceCounterInformation performanceCounterInformation, string instanceName)
			: base(performanceCounterInformation, instanceName)
		{
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x000D19E3 File Offset: 0x000CFBE3
		public void Increment()
		{
			if (this.counter != null)
			{
				this.counter.Increment();
			}
			if (this.commonCounter != null)
			{
				this.commonCounter.Increment();
			}
		}
	}
}
