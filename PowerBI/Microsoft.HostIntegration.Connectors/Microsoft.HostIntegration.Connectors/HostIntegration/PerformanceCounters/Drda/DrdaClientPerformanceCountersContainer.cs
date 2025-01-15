using System;
using System.Diagnostics;
using Microsoft.HostIntegration.StrictResources.PerformanceCountersContainers;

namespace Microsoft.HostIntegration.PerformanceCounters.Drda
{
	// Token: 0x02000778 RID: 1912
	public class DrdaClientPerformanceCountersContainer : PerformanceCountersContainer
	{
		// Token: 0x06003DD0 RID: 15824 RVA: 0x000CFF40 File Offset: 0x000CE140
		static DrdaClientPerformanceCountersContainer()
		{
			DrdaClientPerformanceCountersContainer.drdaArCounters.Add(new PerformanceCounterInformation(0, DrdaClientPerformanceCountersContainer.drdaArCategory, SR.DrdaArActiveDrdaSessions, SR.DrdaArActiveDrdaSessionsHelp, PerformanceCounterType.NumberOfItems32, false));
			DrdaClientPerformanceCountersContainer.drdaArCounters.Add(new PerformanceCounterInformation(1, DrdaClientPerformanceCountersContainer.drdaArCategory, SR.DrdaArOpenStatements, SR.DrdaArOpenStatementsHelp, PerformanceCounterType.NumberOfItems32, false));
			DrdaClientPerformanceCountersContainer.drdaArCounters.Add(new PerformanceCounterInformation(2, DrdaClientPerformanceCountersContainer.drdaArCategory, SR.DrdaArAverageHostProcessingTime, SR.DrdaArAverageHostProcessingTimeHelp, PerformanceCounterType.AverageTimer32, true));
			DrdaClientPerformanceCountersContainer.drdaArCounters.Add(new PerformanceCounterInformation(3, DrdaClientPerformanceCountersContainer.drdaArCategory, SR.DrdaArBytesReceivedPerSecond, SR.DrdaArBytesReceivedPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			DrdaClientPerformanceCountersContainer.drdaArCounters.Add(new PerformanceCounterInformation(4, DrdaClientPerformanceCountersContainer.drdaArCategory, SR.DrdaArBytesSentPerSecond, SR.DrdaArBytesSentPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			DrdaClientPerformanceCountersContainer.drdaArCounters.Add(new PerformanceCounterInformation(5, DrdaClientPerformanceCountersContainer.drdaArCategory, SR.DrdaArTotalTransactions, SR.DrdaArTotalTransactionsHelp, PerformanceCounterType.NumberOfItems32, false));
			DrdaClientPerformanceCountersContainer.drdaArCounters.Add(new PerformanceCounterInformation(6, DrdaClientPerformanceCountersContainer.drdaArCategory, SR.DrdaArTransactionsPerSecond, SR.DrdaArTransactionsPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			DrdaClientPerformanceCountersContainer.drdaArCounters.Add(new PerformanceCounterInformation(7, DrdaClientPerformanceCountersContainer.drdaArCategory, SR.DrdaArTransactionCommitsPerSecond, SR.DrdaArTransactionCommitsPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			DrdaClientPerformanceCountersContainer.drdaArCounters.Add(new PerformanceCounterInformation(8, DrdaClientPerformanceCountersContainer.drdaArCategory, SR.DrdaArTransactionRollbacksPerSecond, SR.DrdaArTransactionRollbacksPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			DrdaClientPerformanceCountersContainer.commonPerformanceCounterIdToSpecific = new int[2][];
			DrdaClientPerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1] = PerformanceCounters.InitializeIntArrayToMinus1(7);
			DrdaClientPerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1][0] = 0;
			DrdaClientPerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1][1] = 3;
			DrdaClientPerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1][2] = 4;
			DrdaClientPerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1][3] = 5;
			DrdaClientPerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1][4] = 6;
			DrdaClientPerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1][5] = 7;
			DrdaClientPerformanceCountersContainer.commonPerformanceCounterIdToSpecific[1][6] = 8;
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x000D0118 File Offset: 0x000CE318
		public DrdaClientPerformanceCountersContainer(string instanceName)
			: base(instanceName, DrdaClientPerformanceCountersContainer.drdaArCategory, DrdaClientPerformanceCountersContainer.drdaArCategoryHelp, DrdaClientPerformanceCountersContainer.drdaArCounters, DrdaClientPerformanceCountersContainer.commonPerformanceCounterIdToSpecific)
		{
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x000D0135 File Offset: 0x000CE335
		public GenericPerformanceCounter GetPerformanceCounter(DrdaClientPerformanceCounter counterIdentifier)
		{
			return base.GetPerformanceCounter((int)counterIdentifier);
		}

		// Token: 0x040024A1 RID: 9377
		private static int[][] commonPerformanceCounterIdToSpecific;

		// Token: 0x040024A2 RID: 9378
		private static string drdaArCategory = SR.DrdaArPerformanceCounterCategory;

		// Token: 0x040024A3 RID: 9379
		private static string drdaArCategoryHelp = SR.DrdaArPerformanceCounterCategoryHelp;

		// Token: 0x040024A4 RID: 9380
		private static PerformanceCounterInformationCollection drdaArCounters = new PerformanceCounterInformationCollection(9);
	}
}
