using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000073 RID: 115
	internal class ErrorEventCorrelator
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0000CFAC File Offset: 0x0000B1AC
		public ErrorEventCorrelator(int lowLevelEventCapacity)
		{
			this.m_lowLevelEvents = new LimitedDictionary<long, MonitoredLowLevelErrorEvent>(lowLevelEventCapacity);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		public void AddLowLevelEvents([NotNull] IEnumerable<MonitoredLowLevelErrorEvent> lowLevelEvents)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<MonitoredLowLevelErrorEvent>>(lowLevelEvents, "lowLevelEvents");
			foreach (MonitoredLowLevelErrorEvent monitoredLowLevelErrorEvent in lowLevelEvents)
			{
				this.AddLowLevelEvent(monitoredLowLevelErrorEvent);
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000D014 File Offset: 0x0000B214
		private void AddLowLevelEvent([NotNull] MonitoredLowLevelErrorEvent newEvent)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<MonitoredLowLevelErrorEvent>(newEvent, "newEvent");
			long correlationId = newEvent.ErrorCorrelationId.CorrelationId;
			MonitoredLowLevelErrorEvent monitoredLowLevelErrorEvent;
			if (!this.m_lowLevelEvents.TryGetValue(correlationId, out monitoredLowLevelErrorEvent) || newEvent.ErrorCorrelationId.CauseOf(monitoredLowLevelErrorEvent.ErrorCorrelationId))
			{
				this.m_lowLevelEvents[correlationId] = newEvent;
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000D068 File Offset: 0x0000B268
		public IEnumerable<IMonitoredEventHandlerVisitor> CorrelateActivityEvents(IEnumerable<MonitoredFlowErrorEvent> uncorrelatedActivityEvents)
		{
			return uncorrelatedActivityEvents.Select((MonitoredFlowErrorEvent e) => this.Correlate(e));
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000D07C File Offset: 0x0000B27C
		private IMonitoredEventHandlerVisitor Correlate(MonitoredFlowErrorEvent activityError)
		{
			long correlationId = activityError.ErrorCorrelationId.CorrelationId;
			MonitoredLowLevelErrorEvent monitoredLowLevelErrorEvent;
			if (this.m_lowLevelEvents.TryGetValue(correlationId, out monitoredLowLevelErrorEvent) && ErrorEventCorrelator.AreCorrelated(monitoredLowLevelErrorEvent, activityError))
			{
				return new CorrelatedMonitoredErrorEvent(monitoredLowLevelErrorEvent, activityError);
			}
			return activityError;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000D0B7 File Offset: 0x0000B2B7
		private static bool AreCorrelated(MonitoredLowLevelErrorEvent lowLevelError, MonitoredFlowErrorEvent flowError)
		{
			return lowLevelError.ErrorCorrelationId.CauseOf(flowError.ErrorCorrelationId);
		}

		// Token: 0x04000126 RID: 294
		private readonly LimitedDictionary<long, MonitoredLowLevelErrorEvent> m_lowLevelEvents;
	}
}
