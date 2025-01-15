using System;

namespace Microsoft.HostIntegration.PerformanceCounters
{
	// Token: 0x0200078F RID: 1935
	public class PerSecondCounter : GenericPerformanceCounter
	{
		// Token: 0x06003E77 RID: 15991 RVA: 0x000D19D9 File Offset: 0x000CFBD9
		internal PerSecondCounter(PerformanceCounterInformation performanceCounterInformation, string instanceName)
			: base(performanceCounterInformation, instanceName)
		{
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x000D19E3 File Offset: 0x000CFBE3
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

		// Token: 0x06003E79 RID: 15993 RVA: 0x000D1A0D File Offset: 0x000CFC0D
		public void IncrementBy(int number)
		{
			if (this.counter != null)
			{
				this.counter.IncrementBy((long)number);
			}
			if (this.commonCounter != null)
			{
				this.commonCounter.IncrementBy((long)number);
			}
		}
	}
}
