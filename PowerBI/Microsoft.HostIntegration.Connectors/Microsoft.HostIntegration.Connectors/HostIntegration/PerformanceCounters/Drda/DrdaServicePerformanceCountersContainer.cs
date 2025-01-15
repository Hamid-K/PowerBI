using System;
using System.Diagnostics;
using Microsoft.HostIntegration.StrictResources.PerformanceCountersContainers;

namespace Microsoft.HostIntegration.PerformanceCounters.Drda
{
	// Token: 0x0200077A RID: 1914
	public class DrdaServicePerformanceCountersContainer : PerformanceCountersContainer
	{
		// Token: 0x06003DD3 RID: 15827 RVA: 0x000D0140 File Offset: 0x000CE340
		static DrdaServicePerformanceCountersContainer()
		{
			DrdaServicePerformanceCountersContainer.drdaAsCounters.Add(new PerformanceCounterInformation(0, DrdaServicePerformanceCountersContainer.drdaAsCategory, SR.DrdaAsActiveDrdaSessions, SR.DrdaAsActiveDrdaSessionsHelp, PerformanceCounterType.NumberOfItems32, false));
			DrdaServicePerformanceCountersContainer.drdaAsCounters.Add(new PerformanceCounterInformation(1, DrdaServicePerformanceCountersContainer.drdaAsCategory, SR.DrdaAsActiveSqlConnections, SR.DrdaAsActiveSqlConnectionsHelp, PerformanceCounterType.NumberOfItems32, false));
			DrdaServicePerformanceCountersContainer.drdaAsCounters.Add(new PerformanceCounterInformation(2, DrdaServicePerformanceCountersContainer.drdaAsCategory, SR.DrdaAsActiveTransactions, SR.DrdaAsActiveTransactionsHelp, PerformanceCounterType.NumberOfItems32, false));
			DrdaServicePerformanceCountersContainer.drdaAsCounters.Add(new PerformanceCounterInformation(3, DrdaServicePerformanceCountersContainer.drdaAsCategory, SR.DrdaAsBytesReceivedPerSecond, SR.DrdaAsBytesReceivedPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			DrdaServicePerformanceCountersContainer.drdaAsCounters.Add(new PerformanceCounterInformation(4, DrdaServicePerformanceCountersContainer.drdaAsCategory, SR.DrdaAsBytesSentPerSecond, SR.DrdaAsBytesSentPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			DrdaServicePerformanceCountersContainer.drdaAsCounters.Add(new PerformanceCounterInformation(5, DrdaServicePerformanceCountersContainer.drdaAsCategory, SR.DrdaAsTotalTransactions, SR.DrdaAsTotalTransactionsHelp, PerformanceCounterType.NumberOfItems32, false));
			DrdaServicePerformanceCountersContainer.drdaAsCounters.Add(new PerformanceCounterInformation(6, DrdaServicePerformanceCountersContainer.drdaAsCategory, SR.DrdaAsTransactionsPerSecond, SR.DrdaAsTransactionsPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			DrdaServicePerformanceCountersContainer.drdaAsCounters.Add(new PerformanceCounterInformation(7, DrdaServicePerformanceCountersContainer.drdaAsCategory, SR.DrdaAsTransactionCommitsPerSecond, SR.DrdaAsTransactionCommitsPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			DrdaServicePerformanceCountersContainer.drdaAsCounters.Add(new PerformanceCounterInformation(8, DrdaServicePerformanceCountersContainer.drdaAsCategory, SR.DrdaAsTransactionRollbacksPerSecond, SR.DrdaAsTransactionRollbacksPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			DrdaServicePerformanceCountersContainer.commonPerformanceCounterIdToSpecific = new int[2][];
			DrdaServicePerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1] = PerformanceCounters.InitializeIntArrayToMinus1(7);
			DrdaServicePerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1][0] = 0;
			DrdaServicePerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1][1] = 3;
			DrdaServicePerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1][2] = 4;
			DrdaServicePerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1][3] = 5;
			DrdaServicePerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1][4] = 6;
			DrdaServicePerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1][5] = 7;
			DrdaServicePerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1][6] = 8;
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x000D0318 File Offset: 0x000CE518
		public DrdaServicePerformanceCountersContainer(string instanceName)
			: base(instanceName, DrdaServicePerformanceCountersContainer.drdaAsCategory, DrdaServicePerformanceCountersContainer.drdaAsCategoryHelp, DrdaServicePerformanceCountersContainer.drdaAsCounters, DrdaServicePerformanceCountersContainer.commonPerformanceCounterIdToSpecific)
		{
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x000D0135 File Offset: 0x000CE335
		public GenericPerformanceCounter GetPerformanceCounter(DrdaServicePerformanceCounter counterIdentifier)
		{
			return base.GetPerformanceCounter((int)counterIdentifier);
		}

		// Token: 0x040024AF RID: 9391
		private static int[][] commonPerformanceCounterIdToSpecific;

		// Token: 0x040024B0 RID: 9392
		private static string drdaAsCategory = SR.DrdaAsPerformanceCounterCategory;

		// Token: 0x040024B1 RID: 9393
		private static string drdaAsCategoryHelp = SR.DrdaAsPerformanceCounterCategoryHelp;

		// Token: 0x040024B2 RID: 9394
		private static PerformanceCounterInformationCollection drdaAsCounters = new PerformanceCounterInformationCollection(9);
	}
}
