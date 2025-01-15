using System;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000078 RID: 120
	internal interface IMonitoredEventHandler : IDisposable
	{
		// Token: 0x0600039A RID: 922
		void OnBatchCompleted();

		// Token: 0x0600039B RID: 923
		void HandleFlowSuccess(MonitoredFlowSuccessEvent successEvent);

		// Token: 0x0600039C RID: 924
		void HandleUncorrelatedFlowError(MonitoredFlowErrorEvent flowError);

		// Token: 0x0600039D RID: 925
		void HandleCorrelatedFlowError(CorrelatedMonitoredErrorEvent correlatedFlowError);

		// Token: 0x0600039E RID: 926
		void HandleUncorrelatedLowLevelError(MonitoredLowLevelErrorEvent lowLevelerror);
	}
}
