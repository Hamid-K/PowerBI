using System;
using System.Threading;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000074 RID: 116
	internal class EventSorter : IMonitoredEventHandler, IDisposable
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0000D0D4 File Offset: 0x0000B2D4
		public EventSorter(IMonitoredEventHandler next, TimeSpan delay)
		{
			this.m_lock = new object();
			this.m_nextHandler = next;
			this.m_delay = delay;
			this.m_linkedList = new SortedLinkedList<MonitoredEventBase>(new TimestampComparer<MonitoredEventBase>());
			this.m_timerFactory = new TimerFactory("Monitoring.EventSorter.TimerFactory", TimerCreationFlags.Crash);
			this.m_timerFactory.SchedulePeriodicTimer("Monitoring.EventSorter.Timer", -1, new TimerCallback(this.TimerCallback), this).UpdatePeriod((int)this.m_delay.TotalMilliseconds);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000D150 File Offset: 0x0000B350
		public void HandleFlowSuccess(MonitoredFlowSuccessEvent successEvent)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (!this.m_stopFlag)
				{
					this.AddEvent(successEvent);
				}
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000D19C File Offset: 0x0000B39C
		public void HandleUncorrelatedFlowError(MonitoredFlowErrorEvent flowError)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (!this.m_stopFlag)
				{
					this.AddEvent(flowError);
				}
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000D1E8 File Offset: 0x0000B3E8
		public void HandleCorrelatedFlowError(CorrelatedMonitoredErrorEvent correlatedFlowError)
		{
			ExtendedDiagnostics.EnsureOperation(false, "Flow event cannot be already correlated");
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		public void HandleUncorrelatedLowLevelError(MonitoredLowLevelErrorEvent lowLevelError)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (!this.m_stopFlag)
				{
					this.AddEvent(lowLevelError);
				}
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void OnBatchCompleted()
		{
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000D244 File Offset: 0x0000B444
		private void AddEvent(MonitoredEventBase monitoredError)
		{
			this.m_linkedList.AddLast(monitoredError);
			this.m_listUpdated = true;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000D25C File Offset: 0x0000B45C
		private void TimerCallback(object sender)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (this.m_stopFlag)
				{
					TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Timer CB called in Monitoring Event Sorter state machine while disposing, so return after doing nothing");
				}
				else if (this.m_linkedList.Count > 0)
				{
					DateTime dateTime = this.CalculateThreshold();
					while (this.m_linkedList.Count > 0 && this.m_linkedList.First.Timestamp <= dateTime)
					{
						this.m_linkedList.First.Visit(this.m_nextHandler);
						this.m_linkedList.RemoveFirst();
					}
					this.m_nextHandler.OnBatchCompleted();
				}
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000D31C File Offset: 0x0000B51C
		private DateTime CalculateThreshold()
		{
			DateTime timestamp = this.m_linkedList.Last.Timestamp;
			DateTime dateTime;
			if (this.m_listUpdated)
			{
				dateTime = timestamp - this.m_delay;
			}
			else
			{
				dateTime = timestamp;
			}
			this.m_listUpdated = false;
			return dateTime;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000D35B File Offset: 0x0000B55B
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000D364 File Offset: 0x0000B564
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				object @lock = this.m_lock;
				lock (@lock)
				{
					this.m_stopFlag = true;
					if (this.m_nextHandler != null)
					{
						this.m_nextHandler.Dispose();
						this.m_nextHandler = null;
					}
				}
				if (this.m_timerFactory != null)
				{
					this.m_timerFactory.Stop();
					this.m_timerFactory.WaitForStopToComplete();
					this.m_timerFactory.Shutdown();
					this.m_timerFactory = null;
				}
			}
		}

		// Token: 0x04000127 RID: 295
		private readonly object m_lock;

		// Token: 0x04000128 RID: 296
		private readonly SortedLinkedList<MonitoredEventBase> m_linkedList;

		// Token: 0x04000129 RID: 297
		private TimerFactory m_timerFactory;

		// Token: 0x0400012A RID: 298
		private IMonitoredEventHandler m_nextHandler;

		// Token: 0x0400012B RID: 299
		private readonly TimeSpan m_delay;

		// Token: 0x0400012C RID: 300
		private bool m_listUpdated;

		// Token: 0x0400012D RID: 301
		private bool m_stopFlag;
	}
}
