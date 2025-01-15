using System;
using System.Diagnostics;
using Microsoft.HostIntegration.StrictResources.PerformanceCountersContainers;

namespace Microsoft.HostIntegration.PerformanceCounters.TI
{
	// Token: 0x02000780 RID: 1920
	public class TiHipPerformanceCountersContainer : PerformanceCountersContainer
	{
		// Token: 0x06003DDC RID: 15836 RVA: 0x000D0544 File Offset: 0x000CE744
		static TiHipPerformanceCountersContainer()
		{
			TiHipPerformanceCountersContainer.hipCounters.Add(new PerformanceCounterInformation(0, TiHipPerformanceCountersContainer.hipCategory, SR.HipAverageMethodCallTime, SR.HipAverageMethodCallTimeHelp, PerformanceCounterType.AverageTimer32, true));
			TiHipPerformanceCountersContainer.hipCounters.Add(new PerformanceCounterInformation(1, TiHipPerformanceCountersContainer.hipCategory, SR.HipCumulativeCalls, SR.HipCumulativeCallsHelp, PerformanceCounterType.NumberOfItems32, false));
			TiHipPerformanceCountersContainer.hipCounters.Add(new PerformanceCounterInformation(2, TiHipPerformanceCountersContainer.hipCategory, SR.HipCurrentlyExecutingCalls, SR.HipCurrentlyExecutingCallsHelp, PerformanceCounterType.NumberOfItems32, false));
			TiHipPerformanceCountersContainer.hipCounters.Add(new PerformanceCounterInformation(3, TiHipPerformanceCountersContainer.hipCategory, SR.HipTotalCallsPerSecond, SR.HipTotalCallsPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			TiHipPerformanceCountersContainer.hipCounters.Add(new PerformanceCounterInformation(4, TiHipPerformanceCountersContainer.hipCategory, SR.HipTotalErrorsPerSecond, SR.HipTotalErrorsPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			TiHipPerformanceCountersContainer.hipCounters.Add(new PerformanceCounterInformation(5, TiHipPerformanceCountersContainer.hipCategory, SR.HipAverageHostResponseTime, SR.HipAverageHostResponseTimeHelp, PerformanceCounterType.AverageTimer32, true));
			TiHipPerformanceCountersContainer.commonPerformanceCounterIdToSpecific = new int[2][];
			TiHipPerformanceCountersContainer.commonPerformanceCounterIdToSpecific[0] = PerformanceCounters.InitializeIntArrayToMinus1(1);
			TiHipPerformanceCountersContainer.commonPerformanceCounterIdToSpecific[0][0] = 5;
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x000D0670 File Offset: 0x000CE870
		public TiHipPerformanceCountersContainer(string instanceName)
			: base(instanceName, TiHipPerformanceCountersContainer.hipCategory, TiHipPerformanceCountersContainer.hipCategoryHelp, TiHipPerformanceCountersContainer.hipCounters, TiHipPerformanceCountersContainer.commonPerformanceCounterIdToSpecific)
		{
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x000D0135 File Offset: 0x000CE335
		public GenericPerformanceCounter GetPerformanceCounter(HipPerformanceCounter counterIdentifier)
		{
			return base.GetPerformanceCounter((int)counterIdentifier);
		}

		// Token: 0x040024D3 RID: 9427
		private static int[][] commonPerformanceCounterIdToSpecific;

		// Token: 0x040024D4 RID: 9428
		private static string hipCategory = SR.HipPerformanceCounterCategory;

		// Token: 0x040024D5 RID: 9429
		private static string hipCategoryHelp = SR.HipPerformanceCounterCategoryHelp;

		// Token: 0x040024D6 RID: 9430
		private static PerformanceCounterInformationCollection hipCounters = new PerformanceCounterInformationCollection(6);
	}
}
