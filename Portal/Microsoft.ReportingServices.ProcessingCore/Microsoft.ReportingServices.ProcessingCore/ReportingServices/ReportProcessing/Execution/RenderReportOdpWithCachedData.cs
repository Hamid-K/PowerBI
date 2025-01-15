using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007D8 RID: 2008
	internal class RenderReportOdpWithCachedData : RenderReportOdp
	{
		// Token: 0x0600710D RID: 28941 RVA: 0x001D66E9 File Offset: 0x001D48E9
		public RenderReportOdpWithCachedData(ProcessingContext pc, RenderingContext rc, DateTime executionTimeStamp, IConfiguration configuration, IChunkFactory dataCacheChunks)
			: base(pc, rc, configuration)
		{
			this.m_executionTimeStamp = executionTimeStamp;
			this.m_dataCacheChunks = dataCacheChunks;
		}

		// Token: 0x0600710E RID: 28942 RVA: 0x001D6704 File Offset: 0x001D4904
		protected override void ProcessReport(ProcessingErrorContext errorContext, ExecutionLogContext executionLogContext, ref UserProfileState userProfileState)
		{
			GlobalIDOwnerCollection globalIDOwnerCollection = new GlobalIDOwnerCollection();
			OnDemandMetadata onDemandMetadata = ChunkManager.OnDemandProcessingManager.DeserializeOnDemandMetadata(this.m_dataCacheChunks, globalIDOwnerCollection);
			globalIDOwnerCollection = new GlobalIDOwnerCollection();
			Report report = ReportProcessing.DeserializeKatmaiReport(base.PublicProcessingContext.ChunkFactory, false, globalIDOwnerCollection);
			ProcessReportOdpWithCachedData processReportOdpWithCachedData = new ProcessReportOdpWithCachedData(base.Configuration, base.PublicProcessingContext, report, errorContext, base.PublicRenderingContext.StoreServerParametersCallback, globalIDOwnerCollection, executionLogContext, this.m_executionTimeStamp, onDemandMetadata);
			this.m_odpReportSnapshot = processReportOdpWithCachedData.Execute(out this.m_odpContext);
		}

		// Token: 0x0600710F RID: 28943 RVA: 0x001D6779 File Offset: 0x001D4979
		protected override void PrepareForExecution()
		{
			base.ValidateReportParameters();
		}

		// Token: 0x04003A4D RID: 14925
		private readonly DateTime m_executionTimeStamp;

		// Token: 0x04003A4E RID: 14926
		private readonly IChunkFactory m_dataCacheChunks;
	}
}
