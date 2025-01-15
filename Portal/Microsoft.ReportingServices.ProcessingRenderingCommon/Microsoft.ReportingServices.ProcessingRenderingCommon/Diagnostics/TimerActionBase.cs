using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000096 RID: 150
	internal abstract class TimerActionBase : IDisposable
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
		public void Start(int timerCycleSeconds, string timerTypeForTrace)
		{
			this.Start(timerCycleSeconds, timerCycleSeconds, timerTypeForTrace);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000E7AC File Offset: 0x0000C9AC
		public void Start(int nextSeconds, int cycleSeconds, string timerTypeForTrace)
		{
			this.m_continueExecuting = true;
			this.m_timerTypeForTrace = timerTypeForTrace;
			TimeSpan timeSpan = new TimeSpan(0, 0, nextSeconds);
			TimeSpan timeSpan2 = new TimeSpan(0, 0, cycleSeconds);
			this.m_serviceInstanceContext = ProcessingContext.ServiceInstanceContext;
			this.m_timer = new Timer(new TimerCallback(this.TimerAction), null, timeSpan, timeSpan2);
			if (ProcessingContext.JobsTracer.TraceInfo)
			{
				ProcessingContext.JobsTracer.Trace(TraceLevel.Info, this.m_timerTypeForTrace + " timer enabled: Next Event: {0} seconds.  Cycle: {1} seconds", new object[] { nextSeconds, cycleSeconds });
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000E840 File Offset: 0x0000CA40
		public void Update(int nextSeconds, int cycleSeconds)
		{
			try
			{
				this.m_timer.Change(nextSeconds, cycleSeconds);
				ProcessingContext.JobsTracer.Trace(TraceLevel.Info, "Timer is updated: Next Event:{0} seconds. Cycle: {1} seconds", new object[] { nextSeconds, cycleSeconds });
			}
			catch (ObjectDisposedException)
			{
				ProcessingContext.JobsTracer.Trace(TraceLevel.Error, "Timer {0} is disposed. Update was unsuccessful", new object[] { this.m_timerTypeForTrace });
			}
			catch (ArgumentOutOfRangeException)
			{
				ProcessingContext.JobsTracer.Trace(TraceLevel.Error, "Timer {0} configuration out of range: Next Event: {1} seconds. Cycle: {2} seconds", new object[] { this.m_timerTypeForTrace, nextSeconds, cycleSeconds });
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000E8F8 File Offset: 0x0000CAF8
		public void Stop()
		{
			if (this.m_timer != null)
			{
				this.m_continueExecuting = false;
				int num = 0;
				while (this.m_timerExecutingLock != 0)
				{
					Thread.Sleep(0);
					num++;
					if (num > 100000)
					{
						ProcessingContext.JobsTracer.Trace(TraceLevel.Error, "Timer " + this.m_timerTypeForTrace + " still executing, breaking...");
						break;
					}
				}
				this.m_timer.Dispose();
				this.m_timer = null;
			}
			Interlocked.Exchange(ref this.m_timerExecutingLock, 0);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000E974 File Offset: 0x0000CB74
		private void TimerAction(object unused)
		{
			if (Interlocked.CompareExchange(ref this.m_timerExecutingLock, 1, 0) != 0)
			{
				if (ProcessingContext.JobsTracer.TraceWarning)
				{
					ProcessingContext.JobsTracer.Trace(TraceLevel.Warning, "Previous request for " + this.m_timerTypeForTrace + "still executing, skipping...");
				}
				return;
			}
			ProcessingContext.InitializeRequestContext(new BackgroundRequestContext(this.m_serviceInstanceContext));
			try
			{
				this.DoTimerAction();
			}
			catch (RSException ex)
			{
				if (ProcessingContext.JobsTracer.TraceError)
				{
					ProcessingContext.JobsTracer.Trace(TraceLevel.Error, "Error in timer " + this.m_timerTypeForTrace + " : " + ex.ToString());
				}
			}
			catch (Exception ex2)
			{
				new InternalCatalogException(ex2, "Unhandled exception in timer " + this.m_timerTypeForTrace);
			}
			finally
			{
				ProcessingContext.EndRequestContext();
				Interlocked.Exchange(ref this.m_timerExecutingLock, 0);
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000EA60 File Offset: 0x0000CC60
		protected bool ContinueExecuting
		{
			get
			{
				return this.m_continueExecuting;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000EA68 File Offset: 0x0000CC68
		public bool IsExecuting
		{
			get
			{
				return this.m_timerExecutingLock == 1;
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000EA73 File Offset: 0x0000CC73
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000EA7C File Offset: 0x0000CC7C
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_timer != null)
				{
					this.m_timer.Dispose();
					this.m_timer = null;
				}
				Interlocked.Exchange(ref this.m_timerExecutingLock, 0);
			}
		}

		// Token: 0x060004B3 RID: 1203
		public abstract void DoTimerAction();

		// Token: 0x040002BB RID: 699
		private Timer m_timer;

		// Token: 0x040002BC RID: 700
		private bool m_continueExecuting = true;

		// Token: 0x040002BD RID: 701
		private int m_timerExecutingLock;

		// Token: 0x040002BE RID: 702
		private string m_timerTypeForTrace;

		// Token: 0x040002BF RID: 703
		private IServiceInstanceContext m_serviceInstanceContext;
	}
}
