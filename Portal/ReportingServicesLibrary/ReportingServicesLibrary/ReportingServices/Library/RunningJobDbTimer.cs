using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002BC RID: 700
	internal sealed class RunningJobDbTimer : TimerActionBase
	{
		// Token: 0x06001934 RID: 6452 RVA: 0x00065490 File Offset: 0x00063690
		public override void DoTimerAction()
		{
			Hashtable hashtable = RunningJobList.Current.MarkOlderJobs();
			Hashtable myRunningJobs = RunningJobsDb.GetMyRunningJobs(Globals.RunningJobType);
			RunningJobsResolver runningJobsResolver = new RunningJobsResolver(hashtable, myRunningJobs);
			if (runningJobsResolver.DbJobsToAdd.Count > 0 && base.ContinueExecuting)
			{
				if (ProcessingContext.JobsTracer.TraceInfo)
				{
					ProcessingContext.JobsTracer.Trace(TraceLevel.Info, "Adding: " + runningJobsResolver.DbJobsToAdd.Count.ToString(CultureInfo.InvariantCulture) + " running jobs to the database");
				}
				RunningJobsDb.AddRunningJobs(runningJobsResolver.DbJobsToAdd);
			}
			if (runningJobsResolver.DbJobsToRemove.Count > 0 && base.ContinueExecuting)
			{
				if (ProcessingContext.JobsTracer.TraceInfo)
				{
					ProcessingContext.JobsTracer.Trace(TraceLevel.Info, "Removing: " + runningJobsResolver.DbJobsToRemove.Count.ToString(CultureInfo.InvariantCulture) + " running jobs from the database");
				}
				RunningJobsDb.RemoveRunningJobs(runningJobsResolver.DbJobsToRemove);
			}
			if (runningJobsResolver.MemJobsToCancel.Count > 0 && base.ContinueExecuting)
			{
				if (ProcessingContext.JobsTracer.TraceInfo)
				{
					ProcessingContext.JobsTracer.Trace(TraceLevel.Info, "Cancelling: " + runningJobsResolver.MemJobsToCancel.Count.ToString(CultureInfo.InvariantCulture) + " running jobs as found in the database");
				}
				foreach (object obj in runningJobsResolver.MemJobsToCancel.Values)
				{
					RunningJobContext runningJobContext = (RunningJobContext)obj;
					if (!base.ContinueExecuting)
					{
						break;
					}
					RunningJobList.Current.InternalCancel(runningJobContext, ReportServerAbortInfo.AbortReason.JobCanceled);
				}
			}
		}
	}
}
