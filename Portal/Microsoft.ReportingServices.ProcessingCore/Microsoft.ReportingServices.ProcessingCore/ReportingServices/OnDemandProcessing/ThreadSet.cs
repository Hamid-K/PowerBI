using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000815 RID: 2069
	internal sealed class ThreadSet : IDisposable
	{
		// Token: 0x060072E1 RID: 29409 RVA: 0x001DDDA0 File Offset: 0x001DBFA0
		internal ThreadSet(int expectedThreadCount)
		{
			if (Global.Tracer.TraceVerbose)
			{
				Global.Tracer.Trace(TraceLevel.Verbose, "ThreadSet object created. {0} threads remaining.", new object[] { expectedThreadCount });
			}
			this.m_allThreadsDone = new ManualResetEvent(true);
		}

		// Token: 0x060072E2 RID: 29410 RVA: 0x001DDDF8 File Offset: 0x001DBFF8
		internal void TryQueueWorkItem(OnDemandProcessingContext processingContext, WaitCallback workItemCallback)
		{
			try
			{
				Interlocked.Increment(ref this.m_runningThreadCount);
				processingContext.JobContext.TryQueueWorkItem(workItemCallback, this);
			}
			catch (Exception)
			{
				Interlocked.Decrement(ref this.m_runningThreadCount);
				throw;
			}
		}

		// Token: 0x060072E3 RID: 29411 RVA: 0x001DDE40 File Offset: 0x001DC040
		internal void QueueWorkItem(OnDemandProcessingContext processingContext, WaitCallback workItemCallback)
		{
			try
			{
				Interlocked.Increment(ref this.m_runningThreadCount);
				processingContext.JobContext.QueueWorkItem(workItemCallback, this);
			}
			catch (Exception)
			{
				Interlocked.Decrement(ref this.m_runningThreadCount);
				throw;
			}
		}

		// Token: 0x060072E4 RID: 29412 RVA: 0x001DDE88 File Offset: 0x001DC088
		internal void ThreadCompleted()
		{
			object counterLock = this.m_counterLock;
			int num;
			lock (counterLock)
			{
				num = Interlocked.Decrement(ref this.m_runningThreadCount);
				if (num <= 0)
				{
					this.m_allThreadsDone.Set();
				}
			}
			if (Global.Tracer.TraceVerbose)
			{
				Global.Tracer.Trace(TraceLevel.Verbose, "Thread completed. {0} thread remaining.", new object[] { num });
			}
		}

		// Token: 0x060072E5 RID: 29413 RVA: 0x001DDF0C File Offset: 0x001DC10C
		internal void WaitForCompletion()
		{
			this.m_waitCalled = true;
			object counterLock = this.m_counterLock;
			lock (counterLock)
			{
				if (Thread.VolatileRead(ref this.m_runningThreadCount) > 0)
				{
					this.m_allThreadsDone.Reset();
				}
			}
			this.m_allThreadsDone.WaitOne();
			if (Global.Tracer.TraceVerbose)
			{
				Global.Tracer.Trace(TraceLevel.Verbose, "All the processing threads have completed.");
			}
		}

		// Token: 0x060072E6 RID: 29414 RVA: 0x001DDF90 File Offset: 0x001DC190
		public void Dispose()
		{
			if (this.m_disposed)
			{
				return;
			}
			this.m_disposed = true;
			this.m_allThreadsDone.Close();
		}

		// Token: 0x04003ADA RID: 15066
		private int m_runningThreadCount;

		// Token: 0x04003ADB RID: 15067
		private ManualResetEvent m_allThreadsDone;

		// Token: 0x04003ADC RID: 15068
		private bool m_disposed;

		// Token: 0x04003ADD RID: 15069
		private bool m_waitCalled;

		// Token: 0x04003ADE RID: 15070
		private object m_counterLock = new object();
	}
}
