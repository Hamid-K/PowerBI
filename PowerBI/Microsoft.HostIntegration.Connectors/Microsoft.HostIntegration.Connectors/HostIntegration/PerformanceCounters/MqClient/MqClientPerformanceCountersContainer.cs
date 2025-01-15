using System;
using System.Diagnostics;
using Microsoft.HostIntegration.StrictResources.PerformanceCountersContainers;

namespace Microsoft.HostIntegration.PerformanceCounters.MqClient
{
	// Token: 0x0200077E RID: 1918
	public class MqClientPerformanceCountersContainer : PerformanceCountersContainer
	{
		// Token: 0x06003DD9 RID: 15833 RVA: 0x000D0340 File Offset: 0x000CE540
		static MqClientPerformanceCountersContainer()
		{
			MqClientPerformanceCountersContainer.mqClientCounters.Add(new PerformanceCounterInformation(0, MqClientPerformanceCountersContainer.mqClientCategory, SR.MqClientAveragePutTime, SR.MqClientAveragePutTimeHelp, PerformanceCounterType.AverageTimer32, true));
			MqClientPerformanceCountersContainer.mqClientCounters.Add(new PerformanceCounterInformation(1, MqClientPerformanceCountersContainer.mqClientCategory, SR.MqClientAverageGetTime, SR.MqClientAverageGetTimeHelp, PerformanceCounterType.AverageTimer32, true));
			MqClientPerformanceCountersContainer.mqClientCounters.Add(new PerformanceCounterInformation(2, MqClientPerformanceCountersContainer.mqClientCategory, SR.MqClientCumulativePuts, SR.MqClientCumulativePutsHelp, PerformanceCounterType.NumberOfItems32, false));
			MqClientPerformanceCountersContainer.mqClientCounters.Add(new PerformanceCounterInformation(3, MqClientPerformanceCountersContainer.mqClientCategory, SR.MqClientCumulativeGets, SR.MqClientCumulativeGetsHelp, PerformanceCounterType.NumberOfItems32, false));
			MqClientPerformanceCountersContainer.mqClientCounters.Add(new PerformanceCounterInformation(4, MqClientPerformanceCountersContainer.mqClientCategory, SR.MqClientPutsPerSecond, SR.MqClientPutsPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			MqClientPerformanceCountersContainer.mqClientCounters.Add(new PerformanceCounterInformation(5, MqClientPerformanceCountersContainer.mqClientCategory, SR.MqClientGetsPerSecond, SR.MqClientGetsPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			MqClientPerformanceCountersContainer.mqClientCounters.Add(new PerformanceCounterInformation(6, MqClientPerformanceCountersContainer.mqClientCategory, SR.MqClientAverageTransactionalPutTime, SR.MqClientAverageTransactionalPutTimeHelp, PerformanceCounterType.AverageTimer32, true));
			MqClientPerformanceCountersContainer.mqClientCounters.Add(new PerformanceCounterInformation(7, MqClientPerformanceCountersContainer.mqClientCategory, SR.MqClientAverageTransactionalGetTime, SR.MqClientAverageTransactionalGetTimeHelp, PerformanceCounterType.AverageTimer32, true));
			MqClientPerformanceCountersContainer.mqClientCounters.Add(new PerformanceCounterInformation(8, MqClientPerformanceCountersContainer.mqClientCategory, SR.MqClientCumulativeTransactionalPuts, SR.MqClientCumulativeTransactionalPutsHelp, PerformanceCounterType.NumberOfItems32, false));
			MqClientPerformanceCountersContainer.mqClientCounters.Add(new PerformanceCounterInformation(9, MqClientPerformanceCountersContainer.mqClientCategory, SR.MqClientCumulativeTransactionalGets, SR.MqClientCumulativeTransactionalGetsHelp, PerformanceCounterType.NumberOfItems32, false));
			MqClientPerformanceCountersContainer.mqClientCounters.Add(new PerformanceCounterInformation(10, MqClientPerformanceCountersContainer.mqClientCategory, SR.MqClientTransactionalPutsPerSecond, SR.MqClientTransactionalPutsPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
			MqClientPerformanceCountersContainer.mqClientCounters.Add(new PerformanceCounterInformation(11, MqClientPerformanceCountersContainer.mqClientCategory, SR.MqClientTransactionalGetsPerSecond, SR.MqClientTransactionalGetsPerSecondHelp, PerformanceCounterType.RateOfCountsPerSecond32, false));
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x000D052C File Offset: 0x000CE72C
		public MqClientPerformanceCountersContainer(string instanceName)
			: base(instanceName, MqClientPerformanceCountersContainer.mqClientCategory, MqClientPerformanceCountersContainer.mqClientCategoryHelp, MqClientPerformanceCountersContainer.mqClientCounters)
		{
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x000D0135 File Offset: 0x000CE335
		public GenericPerformanceCounter GetPerformanceCounter(MqClientPerformanceCounter counterIdentifier)
		{
			return base.GetPerformanceCounter((int)counterIdentifier);
		}

		// Token: 0x040024C9 RID: 9417
		private static string mqClientCategory = SR.MqClientPerformanceCounterCategory;

		// Token: 0x040024CA RID: 9418
		private static string mqClientCategoryHelp = SR.MqClientPerformanceCounterCategoryHelp;

		// Token: 0x040024CB RID: 9419
		private static PerformanceCounterInformationCollection mqClientCounters = new PerformanceCounterInformationCollection(12);
	}
}
