using System;
using System.Collections;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000090 RID: 144
	internal sealed class RunningJobsResolver
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x0000DC2E File Offset: 0x0000BE2E
		public RunningJobsResolver(Hashtable memJobs, Hashtable dbJobs)
		{
			this.Resolve(memJobs, dbJobs);
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000DC5F File Offset: 0x0000BE5F
		public Hashtable MemJobsToCancel
		{
			get
			{
				return this.m_MemJobsToCancel;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000DC67 File Offset: 0x0000BE67
		public Hashtable DbJobsToAdd
		{
			get
			{
				return this.m_DbJobsToAdd;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000DC6F File Offset: 0x0000BE6F
		public Hashtable DbJobsToRemove
		{
			get
			{
				return this.m_DbJobsToRemove;
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000DC78 File Offset: 0x0000BE78
		private void Resolve(Hashtable memJobs, Hashtable dbJobs)
		{
			foreach (object obj in memJobs.Values)
			{
				RunningJobContext runningJobContext = (RunningJobContext)obj;
				RunningJobContext runningJobContext2 = (RunningJobContext)dbJobs[runningJobContext.JobId];
				if (runningJobContext2 == null)
				{
					this.m_DbJobsToAdd.Add(runningJobContext.JobId, runningJobContext);
					ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "RunningJobsResolver.Resolve: job: {0} was added to the DB_to_add list", new object[] { runningJobContext.JobId });
				}
				else if (runningJobContext.Status != runningJobContext2.Status)
				{
					if (runningJobContext2.Status == JobStatusEnum.CancelRequested)
					{
						this.m_MemJobsToCancel.Add(runningJobContext.JobId, runningJobContext);
						ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "RunningJobsResolver.Resolve: job: {0} was added to the local_cancelation_list", new object[] { runningJobContext.JobId });
					}
				}
				else
				{
					ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "RunningJobsResolver.Resolve: job: {0} is present and unchanged", new object[] { runningJobContext.JobId });
				}
			}
			foreach (object obj2 in dbJobs.Values)
			{
				RunningJobContext runningJobContext3 = (RunningJobContext)obj2;
				if ((RunningJobContext)memJobs[runningJobContext3.JobId] == null)
				{
					this.m_DbJobsToRemove.Add(runningJobContext3.JobId, runningJobContext3);
					ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "RunningJobsResolver.Resolve: job: {0} was added to the DB_to_remove list", new object[] { runningJobContext3.JobId });
				}
			}
		}

		// Token: 0x040002A8 RID: 680
		private Hashtable m_DbJobsToAdd = new Hashtable();

		// Token: 0x040002A9 RID: 681
		private Hashtable m_DbJobsToRemove = new Hashtable();

		// Token: 0x040002AA RID: 682
		private Hashtable m_MemJobsToCancel = new Hashtable();
	}
}
