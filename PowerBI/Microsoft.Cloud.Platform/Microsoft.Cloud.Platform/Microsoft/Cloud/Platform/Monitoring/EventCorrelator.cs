using System;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000072 RID: 114
	internal class EventCorrelator : IMonitoredEventHandler, IDisposable
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0000CE74 File Offset: 0x0000B074
		public EventCorrelator([NotNull] IMonitoredEventHandler next, int lowLevelEventCapacity)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IMonitoredEventHandler>(next, "next");
			this.m_nextHandler = next;
			this.m_errorCorrelator = new ErrorEventCorrelator(lowLevelEventCapacity);
			this.m_lowLevelEvents = new Collection<MonitoredLowLevelErrorEvent>();
			this.m_uncorrelatedActivityEvents = new Collection<MonitoredFlowErrorEvent>();
			this.m_successEvents = new Collection<MonitoredFlowSuccessEvent>();
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000CEC6 File Offset: 0x0000B0C6
		public void HandleFlowSuccess(MonitoredFlowSuccessEvent successEvent)
		{
			this.m_successEvents.Add(successEvent);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000CED4 File Offset: 0x0000B0D4
		public void HandleUncorrelatedFlowError(MonitoredFlowErrorEvent activityError)
		{
			this.m_uncorrelatedActivityEvents.Add(activityError);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000CEE2 File Offset: 0x0000B0E2
		public void HandleCorrelatedFlowError(CorrelatedMonitoredErrorEvent correlatedFlowError)
		{
			throw new InvalidOperationException("Flow event cannot be already correlated");
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000CEEE File Offset: 0x0000B0EE
		public void HandleUncorrelatedLowLevelError(MonitoredLowLevelErrorEvent lowLevelError)
		{
			this.m_lowLevelEvents.Add(lowLevelError);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		public void OnBatchCompleted()
		{
			this.m_successEvents.Visit(this.m_nextHandler);
			this.m_successEvents.Clear();
			this.m_errorCorrelator.AddLowLevelEvents(this.m_lowLevelEvents);
			this.m_errorCorrelator.CorrelateActivityEvents(this.m_uncorrelatedActivityEvents).Visit(this.m_nextHandler);
			this.m_uncorrelatedActivityEvents.Clear();
			this.m_lowLevelEvents.Visit(this.m_nextHandler);
			this.m_lowLevelEvents.Clear();
			this.m_nextHandler.OnBatchCompleted();
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000CF84 File Offset: 0x0000B184
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000CF8D File Offset: 0x0000B18D
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.m_nextHandler != null)
			{
				this.m_nextHandler.Dispose();
				this.m_nextHandler = null;
			}
		}

		// Token: 0x04000121 RID: 289
		private readonly ErrorEventCorrelator m_errorCorrelator;

		// Token: 0x04000122 RID: 290
		private readonly Collection<MonitoredFlowSuccessEvent> m_successEvents;

		// Token: 0x04000123 RID: 291
		private readonly Collection<MonitoredLowLevelErrorEvent> m_lowLevelEvents;

		// Token: 0x04000124 RID: 292
		private readonly Collection<MonitoredFlowErrorEvent> m_uncorrelatedActivityEvents;

		// Token: 0x04000125 RID: 293
		private IMonitoredEventHandler m_nextHandler;
	}
}
