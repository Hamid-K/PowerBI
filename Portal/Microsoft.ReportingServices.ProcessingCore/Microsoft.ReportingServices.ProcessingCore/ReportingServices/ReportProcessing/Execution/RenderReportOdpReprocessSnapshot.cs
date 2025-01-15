using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007D7 RID: 2007
	internal class RenderReportOdpReprocessSnapshot : RenderReportOdp
	{
		// Token: 0x06007109 RID: 28937 RVA: 0x001D6666 File Offset: 0x001D4866
		public RenderReportOdpReprocessSnapshot(ProcessingContext pc, RenderingContext rc, IConfiguration configuration, IChunkFactory originalSnapshotChunks)
			: base(pc, rc, configuration)
		{
			this.m_originalSnapshotChunks = originalSnapshotChunks;
		}

		// Token: 0x0600710A RID: 28938 RVA: 0x001D667C File Offset: 0x001D487C
		protected override void ProcessReport(ProcessingErrorContext errorContext, ExecutionLogContext executionLogContext, ref UserProfileState userProfileState)
		{
			OnDemandMetadata onDemandMetadata = null;
			Report report;
			GlobalIDOwnerCollection globalIDOwnerCollection = ChunkManager.OnDemandProcessingManager.DeserializeOdpReportSnapshot(base.PublicProcessingContext, this.m_originalSnapshotChunks, errorContext, true, false, base.Configuration, ref onDemandMetadata, out report);
			ProcessReportOdpSnapshotReprocessing processReportOdpSnapshotReprocessing = new ProcessReportOdpSnapshotReprocessing(base.Configuration, base.PublicProcessingContext, report, errorContext, base.PublicRenderingContext.StoreServerParametersCallback, globalIDOwnerCollection, executionLogContext, onDemandMetadata);
			this.m_odpReportSnapshot = processReportOdpSnapshotReprocessing.Execute(out this.m_odpContext);
		}

		// Token: 0x0600710B RID: 28939 RVA: 0x001D66DE File Offset: 0x001D48DE
		protected override void PrepareForExecution()
		{
			base.ValidateReportParameters();
		}

		// Token: 0x17002680 RID: 9856
		// (get) Token: 0x0600710C RID: 28940 RVA: 0x001D66E6 File Offset: 0x001D48E6
		protected override bool IsSnapshotReprocessing
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04003A4C RID: 14924
		private readonly IChunkFactory m_originalSnapshotChunks;
	}
}
