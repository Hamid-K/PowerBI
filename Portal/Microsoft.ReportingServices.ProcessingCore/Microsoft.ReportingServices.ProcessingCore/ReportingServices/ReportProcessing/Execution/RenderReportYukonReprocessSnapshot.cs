using System;
using System.Collections;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007DF RID: 2015
	internal class RenderReportYukonReprocessSnapshot : RenderReportYukon
	{
		// Token: 0x0600712D RID: 28973 RVA: 0x001D6F7A File Offset: 0x001D517A
		public RenderReportYukonReprocessSnapshot(ProcessingContext pc, RenderingContext rc, ReportProcessing processing, IChunkFactory originalSnapshotChunks)
			: base(pc, rc, processing)
		{
			this.m_originalSnapshotChunks = originalSnapshotChunks;
		}

		// Token: 0x0600712E RID: 28974 RVA: 0x001D6F90 File Offset: 0x001D5190
		protected override void ProcessReport(ProcessingErrorContext errorContext, ExecutionLogContext executionLogContext, ref UserProfileState userProfileState)
		{
			ChunkFactoryAdapter chunkFactoryAdapter = new ChunkFactoryAdapter(base.PublicProcessingContext.ChunkFactory);
			ChunkFactoryAdapter chunkFactoryAdapter2 = new ChunkFactoryAdapter(this.m_originalSnapshotChunks);
			Hashtable hashtable = null;
			DateTime dateTime;
			Report report = ReportProcessing.DeserializeReportFromSnapshot(new ReportProcessing.GetReportChunk(chunkFactoryAdapter2.GetReportChunk), out dateTime, out hashtable);
			this.m_reportSnapshot = base.Processing.ProcessReport(report, base.PublicProcessingContext, true, false, new ReportProcessing.GetReportChunk(chunkFactoryAdapter2.GetReportChunk), errorContext, dateTime, null, out this.m_context, out userProfileState);
			Global.Tracer.Assert(this.m_context != null, "(null != context)");
			this.m_chunkManager = new ChunkManager.RenderingChunkManager(new ReportProcessing.GetReportChunk(chunkFactoryAdapter.GetReportChunk), null, hashtable, null, report.IntermediateFormatVersion);
			this.m_renderingContext = new RenderingContext(this.m_reportSnapshot, base.PublicRenderingContext.Format, dateTime, report.EmbeddedImages, report.ImageStreamNames, base.PublicRenderingContext.EventInfo, base.PublicProcessingContext.ReportContext, base.ReportUri, null, new ReportProcessing.GetReportChunk(chunkFactoryAdapter.GetReportChunk), this.m_chunkManager, base.PublicProcessingContext.GetResourceCallback, new ReportProcessing.GetChunkMimeType(chunkFactoryAdapter.GetChunkMimeType), base.PublicRenderingContext.StoreServerParametersCallback, false, base.PublicProcessingContext.AllowUserProfileState, base.PublicRenderingContext.ReportRuntimeSetup, base.PublicProcessingContext.JobContext, base.PublicProcessingContext.DataProtection);
		}

		// Token: 0x0600712F RID: 28975 RVA: 0x001D70E6 File Offset: 0x001D52E6
		protected override void PrepareForExecution()
		{
			base.ValidateReportParameters();
		}

		// Token: 0x17002686 RID: 9862
		// (get) Token: 0x06007130 RID: 28976 RVA: 0x001D70EE File Offset: 0x001D52EE
		protected override bool IsSnapshotReprocessing
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04003A57 RID: 14935
		private readonly IChunkFactory m_originalSnapshotChunks;
	}
}
