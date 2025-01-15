using System;

namespace Microsoft.HostIntegration.PerformanceCounters.Drda
{
	// Token: 0x0200077C RID: 1916
	public class CommonDrdaPerformanceCountersContainer : PerformanceCountersContainer
	{
		// Token: 0x06003DD6 RID: 15830 RVA: 0x000D0335 File Offset: 0x000CE535
		public CommonDrdaPerformanceCountersContainer(DrdaServicePerformanceCountersContainer parentContainer)
			: base(parentContainer, SharedPerformanceCountersContainer.CommonDrda)
		{
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x000D0335 File Offset: 0x000CE535
		public CommonDrdaPerformanceCountersContainer(DrdaClientPerformanceCountersContainer parentContainer)
			: base(parentContainer, SharedPerformanceCountersContainer.CommonDrda)
		{
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x000D0135 File Offset: 0x000CE335
		public GenericPerformanceCounter GetPerformanceCounter(CommonDrdaPerformanceCounter counterIdentifier)
		{
			return base.GetPerformanceCounter((int)counterIdentifier);
		}
	}
}
