using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007E2 RID: 2018
	internal abstract class AbortHelper : IAbortHelper, IDisposable
	{
		// Token: 0x06007143 RID: 28995 RVA: 0x001D74BB File Offset: 0x001D56BB
		internal AbortHelper(IJobContext jobContext, bool enforceSingleAbortException, bool requireEventHandler)
		{
			this.m_enforceSingleAbortException = enforceSingleAbortException;
			this.m_requireEventHandler = requireEventHandler;
			this.m_syncRoot = new object();
			if (jobContext != null)
			{
				this.m_jobContext = jobContext;
				jobContext.AddAbortHelper(this);
			}
		}

		// Token: 0x06007144 RID: 28996 RVA: 0x001D74ED File Offset: 0x001D56ED
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06007145 RID: 28997 RVA: 0x001D74F6 File Offset: 0x001D56F6
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.m_jobContext != null)
			{
				this.m_jobContext.RemoveAbortHelper();
			}
		}

		// Token: 0x06007146 RID: 28998 RVA: 0x001D750E File Offset: 0x001D570E
		public virtual bool Abort(ProcessingStatus status)
		{
			return this.Abort(status, null);
		}

		// Token: 0x06007147 RID: 28999
		protected abstract ProcessingStatus GetStatus(string uniqueName);

		// Token: 0x06007148 RID: 29000
		protected abstract void SetStatus(ProcessingStatus newStatus, string uniqueName);

		// Token: 0x06007149 RID: 29001
		internal abstract void AddSubreportInstanceOrSharedDataSet(string uniqueName);

		// Token: 0x1700268C RID: 9868
		// (get) Token: 0x0600714A RID: 29002 RVA: 0x001D7518 File Offset: 0x001D5718
		// (set) Token: 0x0600714B RID: 29003 RVA: 0x001D7520 File Offset: 0x001D5720
		protected ProcessingStatus Status
		{
			get
			{
				return this.m_overallStatus;
			}
			set
			{
				this.m_overallStatus = value;
			}
		}

		// Token: 0x1700268D RID: 9869
		// (get) Token: 0x0600714C RID: 29004 RVA: 0x001D7529 File Offset: 0x001D5729
		// (set) Token: 0x0600714D RID: 29005 RVA: 0x001D7531 File Offset: 0x001D5731
		internal bool EnforceSingleAbortException
		{
			get
			{
				return this.m_enforceSingleAbortException;
			}
			set
			{
				this.m_enforceSingleAbortException = value;
			}
		}

		// Token: 0x0600714E RID: 29006 RVA: 0x001D753C File Offset: 0x001D573C
		internal bool SetError(Exception e, string uniqueName)
		{
			Global.Tracer.Trace(TraceLevel.Verbose, "An exception has occurred. Trying to abort processing. Details: {0}", new object[] { (e == null) ? "" : e.ToString() });
			if (this.m_exception == null)
			{
				this.m_exception = e;
			}
			else if (e is DataSetExecutionException && e.InnerException == this.m_exception)
			{
				this.m_exception = e;
			}
			return this.Abort(ProcessingStatus.AbnormalTermination, uniqueName);
		}

		// Token: 0x0600714F RID: 29007 RVA: 0x001D75B0 File Offset: 0x001D57B0
		private bool Abort(ProcessingStatus status, string uniqueName)
		{
			bool flag = !this.m_requireEventHandler;
			if (!Monitor.TryEnter(this.m_syncRoot))
			{
				Global.Tracer.Trace(TraceLevel.Info, "Some other thread is aborting processing.");
				return flag;
			}
			try
			{
				if (this.GetStatus(uniqueName) != ProcessingStatus.Success)
				{
					Global.Tracer.Trace(TraceLevel.Info, "Some other thread has already aborted processing.");
					return flag;
				}
				this.SetStatus(status, uniqueName);
				if (this.ProcessingAbortEvent != null)
				{
					try
					{
						this.ProcessingAbortEvent(this, new ProcessingAbortEventArgs(uniqueName));
						flag = true;
						Global.Tracer.Trace(TraceLevel.Verbose, "Abort callback successful.");
						return flag;
					}
					catch (Exception ex)
					{
						Global.Tracer.Trace(TraceLevel.Error, "Exception in abort callback. Details: {0}", new object[] { ex.ToString() });
						return flag;
					}
				}
				Global.Tracer.Trace(TraceLevel.Verbose, "No abort callback.");
			}
			finally
			{
				Monitor.Exit(this.m_syncRoot);
			}
			return flag;
		}

		// Token: 0x06007150 RID: 29008 RVA: 0x001D769C File Offset: 0x001D589C
		internal virtual void ThrowIfAborted(CancelationTrigger cancelationTrigger, string uniqueName)
		{
			object syncRoot = this.m_syncRoot;
			ProcessingStatus status;
			lock (syncRoot)
			{
				status = this.GetStatus(uniqueName);
			}
			if (status == ProcessingStatus.Success || (this.m_hasThrownAbortedException && this.m_enforceSingleAbortException))
			{
				return;
			}
			this.m_hasThrownAbortedException = true;
			if (status == ProcessingStatus.AbnormalTermination)
			{
				throw new ProcessingAbortedException(cancelationTrigger, this.m_exception);
			}
			throw new ProcessingAbortedException(cancelationTrigger);
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06007151 RID: 29009 RVA: 0x001D7710 File Offset: 0x001D5910
		// (remove) Token: 0x06007152 RID: 29010 RVA: 0x001D7748 File Offset: 0x001D5948
		public event EventHandler ProcessingAbortEvent;

		// Token: 0x04003A61 RID: 14945
		private ProcessingStatus m_overallStatus;

		// Token: 0x04003A62 RID: 14946
		private Exception m_exception;

		// Token: 0x04003A63 RID: 14947
		private readonly IJobContext m_jobContext;

		// Token: 0x04003A64 RID: 14948
		private bool m_enforceSingleAbortException;

		// Token: 0x04003A65 RID: 14949
		private bool m_hasThrownAbortedException;

		// Token: 0x04003A66 RID: 14950
		private readonly bool m_requireEventHandler;

		// Token: 0x04003A67 RID: 14951
		private readonly object m_syncRoot;
	}
}
