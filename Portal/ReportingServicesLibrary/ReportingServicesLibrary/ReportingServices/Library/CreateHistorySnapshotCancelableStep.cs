using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002B2 RID: 690
	internal sealed class CreateHistorySnapshotCancelableStep : CancelableLibraryStep
	{
		// Token: 0x0600191D RID: 6429 RVA: 0x00064E75 File Offset: 0x00063075
		internal CreateHistorySnapshotCancelableStep(CreateHistorySnapshotCancelableStep.CreateSnapshot func, ExternalItemPath report, JobType jobType, UserContext userContext)
			: base(UrlFriendlyUIDGenerator.Create(), report, JobActionEnum.ReportHistoryCreation, jobType, userContext)
		{
			this.m_function = func;
			this.m_report = report;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x00064E95 File Offset: 0x00063095
		protected override void Execute()
		{
			ProcessingContext.JobContext.ExecutionInfo.Source = ExecutionLogExecType.History;
			this.m_function();
		}

		// Token: 0x04000911 RID: 2321
		private CreateHistorySnapshotCancelableStep.CreateSnapshot m_function;

		// Token: 0x04000912 RID: 2322
		private ExternalItemPath m_report;

		// Token: 0x020004D7 RID: 1239
		// (Invoke) Token: 0x06002474 RID: 9332
		internal delegate void CreateSnapshot();
	}
}
