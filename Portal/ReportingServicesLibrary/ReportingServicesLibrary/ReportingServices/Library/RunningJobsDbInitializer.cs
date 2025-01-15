using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002BD RID: 701
	internal class RunningJobsDbInitializer
	{
		// Token: 0x06001936 RID: 6454 RVA: 0x0006563C File Offset: 0x0006383C
		public RunningJobsDbInitializer()
		{
			RunningJobList.Current.SecondsBeforeSave = Globals.Configuration.RunningRequestsAge;
			RunningJobList.Current.JobMarkerForCancel = new RunningJobList.MarkForCancelJobDelegate(RunningJobsDb.MarkForCancelRequest);
			RunningJobList.Current.JobRemovalDelegate = new RunningJobList.RemoveRunningJobsDelegate(RunningJobsDb.RemoveRunningJob);
		}
	}
}
