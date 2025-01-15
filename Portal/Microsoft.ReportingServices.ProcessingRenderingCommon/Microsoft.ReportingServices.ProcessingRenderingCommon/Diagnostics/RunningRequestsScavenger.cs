using System;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000091 RID: 145
	internal sealed class RunningRequestsScavenger : TimerActionBase
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x0000DE0C File Offset: 0x0000C00C
		public void Start(string timerTypeForTrace)
		{
			base.Start(ProcessingContext.Configuration.RunningRequestsScavengerCycle, timerTypeForTrace);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000DE1F File Offset: 0x0000C01F
		public void Update()
		{
			base.Update(ProcessingContext.Configuration.RunningRequestsScavengerCycle, ProcessingContext.Configuration.RunningRequestsScavengerCycle);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000DE3C File Offset: 0x0000C03C
		public override void DoTimerAction()
		{
			foreach (object obj in RunningJobList.Current.CopyTo().Values)
			{
				RunningJobContext runningJobContext = (RunningJobContext)obj;
				try
				{
					if (!base.ContinueExecuting)
					{
						break;
					}
					if (!runningJobContext.IsClientConnected())
					{
						if (runningJobContext.Cancel(ReportServerAbortInfo.AbortReason.JobOrphaned) == 0 && runningJobContext.Status != JobStatusEnum.CancelRequested)
						{
							ProcessingContext.JobsTracer.Trace(TraceLevel.Warning, "RunningJobContext.Cancel(JobOrphaned) failed for job {0}", new object[] { runningJobContext.JobId });
						}
					}
					else if (runningJobContext.IsExpired() && runningJobContext.Cancel(ReportServerAbortInfo.AbortReason.TimeoutExpired) == 0 && runningJobContext.Status != JobStatusEnum.CancelRequested)
					{
						ProcessingContext.JobsTracer.Trace(TraceLevel.Warning, "RunningJobContext.Cancel(TimeoutExpired) failed for job {0}", new object[] { runningJobContext.JobId });
					}
				}
				catch (Exception ex)
				{
					ProcessingContext.JobsTracer.Trace(TraceLevel.Error, "Exception in RunningRequestsScavenger.DoTimerAction: {0}", new object[] { ex });
					if (ex is ThreadAbortException)
					{
						Thread.ResetAbort();
					}
				}
			}
		}
	}
}
