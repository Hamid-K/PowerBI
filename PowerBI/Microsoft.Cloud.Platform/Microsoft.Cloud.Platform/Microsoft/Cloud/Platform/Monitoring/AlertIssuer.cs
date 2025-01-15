using System;
using System.Diagnostics;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200006F RID: 111
	internal class AlertIssuer : IMonitoredEventHandler, IDisposable
	{
		// Token: 0x06000351 RID: 849 RVA: 0x0000CCDA File Offset: 0x0000AEDA
		public AlertIssuer(EventLogEntryType severity, string eventLogSourceName)
		{
			this.m_eventLog = new WindowsEventLogWriter(eventLogSourceName);
			this.m_severity = severity;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void OnBatchCompleted()
		{
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void HandleFlowSuccess(MonitoredFlowSuccessEvent successEvent)
		{
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000CCF5 File Offset: 0x0000AEF5
		public void HandleUncorrelatedFlowError(MonitoredFlowErrorEvent flowError)
		{
			this.IssueAlert(flowError);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000CCF5 File Offset: 0x0000AEF5
		public void HandleCorrelatedFlowError(CorrelatedMonitoredErrorEvent correlatedFlowError)
		{
			this.IssueAlert(correlatedFlowError);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000CCF5 File Offset: 0x0000AEF5
		public void HandleUncorrelatedLowLevelError(MonitoredLowLevelErrorEvent lowLevelerror)
		{
			this.IssueAlert(lowLevelerror);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000CD00 File Offset: 0x0000AF00
		private void IssueAlert(IWindowsEventLogId monitoredError)
		{
			this.m_eventLog.WriteEntry(monitoredError.ToString(), this.m_severity, monitoredError.WindowsEventLogId);
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "A Windows EventLog entry with severity {0} and id {1} has been written.", new object[] { this.m_severity, monitoredError.WindowsEventLogId });
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Dispose()
		{
		}

		// Token: 0x0400011C RID: 284
		private readonly WindowsEventLogWriter m_eventLog;

		// Token: 0x0400011D RID: 285
		private readonly EventLogEntryType m_severity;
	}
}
