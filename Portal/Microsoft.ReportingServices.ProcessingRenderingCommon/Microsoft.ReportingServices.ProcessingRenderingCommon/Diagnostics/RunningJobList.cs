using System;
using System.Collections;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200008F RID: 143
	public sealed class RunningJobList
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000D81F File Offset: 0x0000BA1F
		public static RunningJobList Current
		{
			get
			{
				return RunningJobList.m_current;
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000D826 File Offset: 0x0000BA26
		public IEnumerator GetEnumerator()
		{
			return new CancelableJobEnumerator(this.m_runningJobs.GetEnumerator());
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0000D838 File Offset: 0x0000BA38
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x0000D840 File Offset: 0x0000BA40
		public int SecondsBeforeSave
		{
			get
			{
				return this.m_secondsBeforeSave;
			}
			set
			{
				this.m_secondsBeforeSave = value;
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000D849 File Offset: 0x0000BA49
		public Hashtable CopyTo()
		{
			return (Hashtable)this.m_runningJobs.Clone();
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000D85C File Offset: 0x0000BA5C
		public Hashtable MarkOlderJobs()
		{
			Hashtable hashtable = new Hashtable();
			object syncRoot = this.m_runningJobs.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.m_runningJobs.Values)
				{
					RunningJobContext runningJobContext = (RunningJobContext)obj;
					if ((DateTime.Now - runningJobContext.StartDate).TotalSeconds >= (double)this.SecondsBeforeSave)
					{
						object statusSyncRoot = runningJobContext.StatusSyncRoot;
						lock (statusSyncRoot)
						{
							if (runningJobContext.Status == JobStatusEnum.New)
							{
								runningJobContext.Status = JobStatusEnum.Running;
							}
						}
						hashtable.Add(runningJobContext.JobId, runningJobContext);
					}
				}
			}
			return hashtable;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000D95C File Offset: 0x0000BB5C
		public Hashtable GetNewLocalJobs()
		{
			Hashtable hashtable = new Hashtable();
			object syncRoot = this.m_runningJobs.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.m_runningJobs.Values)
				{
					RunningJobContext runningJobContext = (RunningJobContext)obj;
					if (runningJobContext.Status == JobStatusEnum.New)
					{
						hashtable.Add(runningJobContext.JobId, runningJobContext);
					}
				}
			}
			return hashtable;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000DA04 File Offset: 0x0000BC04
		public RunningJobContext AddJob(string jobId, ExternalItemPath requestPath, JobActionEnum action, JobType type, UserContext userContext, RequestContext optionalCtx)
		{
			RunningJobContext runningJobContext = new RunningJobContext(jobId, requestPath, action, type, this, userContext, optionalCtx);
			this.m_runningJobs.Add(runningJobContext.JobId, runningJobContext);
			return runningJobContext;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000DA34 File Offset: 0x0000BC34
		public void RemoveJob(string jobId)
		{
			bool flag = true;
			JobStatusEnum jobStatusEnum = JobStatusEnum.New;
			object syncRoot = this.m_runningJobs.SyncRoot;
			lock (syncRoot)
			{
				if (this.m_runningJobs.Contains(jobId))
				{
					jobStatusEnum = ((RunningJobContext)this.m_runningJobs[jobId]).Status;
					this.m_runningJobs.Remove(jobId);
				}
				else
				{
					flag = false;
				}
			}
			if (flag)
			{
				ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "RunningJobList.RemoveJob: " + jobId + " was removed");
				if ((jobStatusEnum == JobStatusEnum.Running || jobStatusEnum == JobStatusEnum.CancelRequested) && this.m_RemoveRunningJobsDelegate != null)
				{
					this.m_RemoveRunningJobsDelegate(jobId);
					return;
				}
			}
			else
			{
				ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "RunningJobList.RemoveJob; Could not find job id to remove. Id=" + jobId);
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000DB00 File Offset: 0x0000BD00
		public bool CancelJob(string jobId, ReportServerAbortInfo.AbortReason reason)
		{
			RunningJobContext runningJobContext = (RunningJobContext)this.m_runningJobs[jobId];
			if (runningJobContext != null)
			{
				return this.InternalCancel(runningJobContext, reason);
			}
			ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "RunningJobList.CancelJob; job: " + jobId + " not found locally locally; marking it in the database...");
			if (reason == ReportServerAbortInfo.AbortReason.JobCanceled)
			{
				int num = 0;
				if (this.m_MarkForCancelJobDelegate != null)
				{
					num = this.m_MarkForCancelJobDelegate(jobId);
				}
				return num == 1;
			}
			throw new InternalCatalogException("Unexpected error: job " + jobId + " is not local and AbortReason is not cancel: " + reason.ToString());
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000DB88 File Offset: 0x0000BD88
		public bool InternalCancel(RunningJobContext jobCtx, ReportServerAbortInfo.AbortReason reason)
		{
			ProcessingContext.JobsTracer.Trace(TraceLevel.Info, "RunningJobList.CancelJob; found job: " + jobCtx.JobId + "locally; Cancelling...");
			if (string.Compare(jobCtx.Machine, Environment.MachineName, StringComparison.Ordinal) != 0)
			{
				throw new InternalCatalogException("Found unexpected job for machine: " + jobCtx.Machine);
			}
			return jobCtx.Cancel(reason) > 0;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x0000DBF0 File Offset: 0x0000BDF0
		public RunningJobList.RemoveRunningJobsDelegate JobRemovalDelegate
		{
			get
			{
				return this.m_RemoveRunningJobsDelegate;
			}
			set
			{
				this.m_RemoveRunningJobsDelegate = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000DBF9 File Offset: 0x0000BDF9
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0000DC01 File Offset: 0x0000BE01
		public RunningJobList.MarkForCancelJobDelegate JobMarkerForCancel
		{
			get
			{
				return this.m_MarkForCancelJobDelegate;
			}
			set
			{
				this.m_MarkForCancelJobDelegate = value;
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000DC0A File Offset: 0x0000BE0A
		private RunningJobList()
		{
			this.m_runningJobs = Hashtable.Synchronized(new Hashtable());
		}

		// Token: 0x040002A3 RID: 675
		private static RunningJobList m_current = new RunningJobList();

		// Token: 0x040002A4 RID: 676
		private Hashtable m_runningJobs;

		// Token: 0x040002A5 RID: 677
		private RunningJobList.RemoveRunningJobsDelegate m_RemoveRunningJobsDelegate;

		// Token: 0x040002A6 RID: 678
		private RunningJobList.MarkForCancelJobDelegate m_MarkForCancelJobDelegate;

		// Token: 0x040002A7 RID: 679
		private int m_secondsBeforeSave;

		// Token: 0x020000F7 RID: 247
		// (Invoke) Token: 0x060007D3 RID: 2003
		public delegate int MarkForCancelJobDelegate(string jobId);

		// Token: 0x020000F8 RID: 248
		// (Invoke) Token: 0x060007D7 RID: 2007
		public delegate int RemoveRunningJobsDelegate(string jobId);
	}
}
