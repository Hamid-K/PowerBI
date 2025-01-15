using System;
using System.Diagnostics;
using Microsoft.HostIntegration.StrictResources.PerformanceCountersContainers;

namespace Microsoft.HostIntegration.PerformanceCounters.TI
{
	// Token: 0x02000782 RID: 1922
	public class TiWipPerformanceCountersContainer : PerformanceCountersContainer
	{
		// Token: 0x06003DDF RID: 15839 RVA: 0x000D0690 File Offset: 0x000CE890
		static TiWipPerformanceCountersContainer()
		{
			TiWipPerformanceCountersContainer.wipCounters.Add(new PerformanceCounterInformation(0, TiWipPerformanceCountersContainer.wipCategory, SR.WipAverageMethodCallTime, SR.WipAverageMethodCallTimeHelp, PerformanceCounterType.AverageTimer32, true));
			TiWipPerformanceCountersContainer.wipCounters.Add(new PerformanceCounterInformation(1, TiWipPerformanceCountersContainer.wipCategory, SR.WipCumulativeCalls, SR.WipCumulativeCallsHelp, PerformanceCounterType.NumberOfItems32, false));
			TiWipPerformanceCountersContainer.wipCounters.Add(new PerformanceCounterInformation(2, TiWipPerformanceCountersContainer.wipCategory, SR.WipCurrentlyExecutingCalls, SR.WipCurrentlyExecutingCallsHelp, PerformanceCounterType.NumberOfItems32, false));
			TiWipPerformanceCountersContainer.wipCounters.Add(new PerformanceCounterInformation(3, TiWipPerformanceCountersContainer.wipCategory, SR.WipTotalCallsPerSecond, SR.WipTotalCallsPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			TiWipPerformanceCountersContainer.wipCounters.Add(new PerformanceCounterInformation(4, TiWipPerformanceCountersContainer.wipCategory, SR.WipTotalErrorsPerSecond, SR.WipTotalErrorsPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			TiWipPerformanceCountersContainer.wipCounters.Add(new PerformanceCounterInformation(5, TiWipPerformanceCountersContainer.wipCategory, SR.WipAverageHostResponseTime, SR.WipAverageHostResponseTimeHelp, PerformanceCounterType.AverageTimer32, true));
			TiWipPerformanceCountersContainer.commonPerformanceCounterIdToSpecific = new int[2][];
			TiWipPerformanceCountersContainer.commonPerformanceCounterIdToSpecific[0] = PerformanceCounters.InitializeIntArrayToMinus1(1);
			TiWipPerformanceCountersContainer.commonPerformanceCounterIdToSpecific[0][0] = 5;
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x000D07BC File Offset: 0x000CE9BC
		public TiWipPerformanceCountersContainer(string instanceName)
			: base(instanceName, TiWipPerformanceCountersContainer.wipCategory, TiWipPerformanceCountersContainer.wipCategoryHelp, TiWipPerformanceCountersContainer.wipCounters, TiWipPerformanceCountersContainer.commonPerformanceCounterIdToSpecific)
		{
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x000D0135 File Offset: 0x000CE335
		public GenericPerformanceCounter GetPerformanceCounter(WipPerformanceCounter counterIdentifier)
		{
			return base.GetPerformanceCounter((int)counterIdentifier);
		}

		// Token: 0x040024DE RID: 9438
		private static int[][] commonPerformanceCounterIdToSpecific;

		// Token: 0x040024DF RID: 9439
		private static string wipCategory = SR.WipPerformanceCounterCategory;

		// Token: 0x040024E0 RID: 9440
		private static string wipCategoryHelp = SR.WipPerformanceCounterCategoryHelp;

		// Token: 0x040024E1 RID: 9441
		private static PerformanceCounterInformationCollection wipCounters = new PerformanceCounterInformationCollection(6);
	}
}
