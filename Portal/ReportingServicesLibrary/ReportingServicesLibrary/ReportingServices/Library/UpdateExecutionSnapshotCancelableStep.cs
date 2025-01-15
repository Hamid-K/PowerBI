using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002B3 RID: 691
	internal sealed class UpdateExecutionSnapshotCancelableStep : CancelableLibraryStep
	{
		// Token: 0x0600191F RID: 6431 RVA: 0x00064EB2 File Offset: 0x000630B2
		internal UpdateExecutionSnapshotCancelableStep(UpdateExecutionSnapshotCancelableStep.UpdateExecutionSnapshot func, ExternalItemPath report, JobType type, UserContext userContext)
			: base(UrlFriendlyUIDGenerator.Create(), report, JobActionEnum.SnapshotCreation, type, userContext)
		{
			this.m_function = func;
			this.m_report = report;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00064ED2 File Offset: 0x000630D2
		protected override void Execute()
		{
			ProcessingContext.JobContext.ExecutionInfo.Source = ExecutionLogExecType.Snapshot;
			this.m_function();
		}

		// Token: 0x04000913 RID: 2323
		private UpdateExecutionSnapshotCancelableStep.UpdateExecutionSnapshot m_function;

		// Token: 0x04000914 RID: 2324
		private ExternalItemPath m_report;

		// Token: 0x020004D8 RID: 1240
		// (Invoke) Token: 0x06002478 RID: 9336
		internal delegate void UpdateExecutionSnapshot();
	}
}
