using System;

namespace Microsoft.HostIntegration.PerformanceCounters.TI
{
	// Token: 0x02000784 RID: 1924
	public class TransportPerformanceCountersContainer : PerformanceCountersContainer
	{
		// Token: 0x06003DE2 RID: 15842 RVA: 0x000D07D9 File Offset: 0x000CE9D9
		public TransportPerformanceCountersContainer(TiWipPerformanceCountersContainer parentContainer)
			: base(parentContainer, SharedPerformanceCountersContainer.Transport)
		{
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x000D07D9 File Offset: 0x000CE9D9
		public TransportPerformanceCountersContainer(TiHipPerformanceCountersContainer parentContainer)
			: base(parentContainer, SharedPerformanceCountersContainer.Transport)
		{
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x000D0135 File Offset: 0x000CE335
		public GenericPerformanceCounter GetPerformanceCounter(TransportPerformanceCounter counterIdentifier)
		{
			return base.GetPerformanceCounter((int)counterIdentifier);
		}
	}
}
