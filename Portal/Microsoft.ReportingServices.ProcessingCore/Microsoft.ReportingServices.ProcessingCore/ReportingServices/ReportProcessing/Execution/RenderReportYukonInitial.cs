using System;
using System.Collections;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007DB RID: 2011
	internal class RenderReportYukonInitial : RenderReportYukon
	{
		// Token: 0x0600711F RID: 28959 RVA: 0x001D6A41 File Offset: 0x001D4C41
		public RenderReportYukonInitial(ProcessingContext pc, RenderingContext rc, DateTime executionTimeStamp, ReportProcessing processing, IChunkFactory yukonCompiledDefinition)
			: base(pc, rc, processing)
		{
			this.m_executionTimeStamp = executionTimeStamp;
			this.m_yukonCompiledDefinition = yukonCompiledDefinition;
		}

		// Token: 0x06007120 RID: 28960 RVA: 0x001D6A5C File Offset: 0x001D4C5C
		protected override void PrepareForExecution()
		{
			base.ValidateReportParameters();
			ReportProcessing.CheckReportCredentialsAndConnectionUserDependency(base.PublicProcessingContext);
		}

		// Token: 0x06007121 RID: 28961 RVA: 0x001D6A70 File Offset: 0x001D4C70
		protected override void ProcessReport(ProcessingErrorContext errorContext, ExecutionLogContext executionLogContext, ref UserProfileState userProfileState)
		{
			object obj = new ChunkFactoryAdapter(this.m_yukonCompiledDefinition);
			ChunkFactoryAdapter chunkFactoryAdapter = new ChunkFactoryAdapter(base.PublicProcessingContext.ChunkFactory);
			Hashtable hashtable = null;
			Report report = ReportProcessing.DeserializeReport(new ReportProcessing.GetReportChunk(obj.GetReportChunk), out hashtable);
			this.m_reportSnapshot = base.Processing.ProcessReport(report, base.PublicProcessingContext, false, false, new ReportProcessing.GetReportChunk(chunkFactoryAdapter.GetReportChunk), errorContext, this.m_executionTimeStamp, null, out this.m_context, out userProfileState);
			Global.Tracer.Assert(this.m_context != null, "(null != m_context)");
			executionLogContext.AddLegacyDataProcessingTime(this.m_context.DataProcessingDurationMs);
			this.m_chunkManager = new ChunkManager.RenderingChunkManager(new ReportProcessing.GetReportChunk(chunkFactoryAdapter.GetReportChunk), null, hashtable, null, report.IntermediateFormatVersion);
			this.m_renderingContext = new RenderingContext(this.m_reportSnapshot, base.PublicRenderingContext.Format, this.m_executionTimeStamp, report.EmbeddedImages, report.ImageStreamNames, base.PublicRenderingContext.EventInfo, base.PublicProcessingContext.ReportContext, base.ReportUri, base.RenderingParameters, new ReportProcessing.GetReportChunk(chunkFactoryAdapter.GetReportChunk), this.m_chunkManager, base.PublicProcessingContext.GetResourceCallback, new ReportProcessing.GetChunkMimeType(chunkFactoryAdapter.GetChunkMimeType), base.PublicRenderingContext.StoreServerParametersCallback, false, base.PublicProcessingContext.AllowUserProfileState, base.PublicRenderingContext.ReportRuntimeSetup, base.PublicProcessingContext.JobContext, base.PublicProcessingContext.DataProtection);
		}

		// Token: 0x04003A54 RID: 14932
		private readonly DateTime m_executionTimeStamp;

		// Token: 0x04003A55 RID: 14933
		private readonly IChunkFactory m_yukonCompiledDefinition;
	}
}
