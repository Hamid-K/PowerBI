using System;
using System.Collections;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007DD RID: 2013
	internal class RenderReportYukonSnapshot : RenderReportYukon
	{
		// Token: 0x06007124 RID: 28964 RVA: 0x001D6C63 File Offset: 0x001D4E63
		public RenderReportYukonSnapshot(ProcessingContext pc, RenderingContext rc, ReportProcessing processing)
			: base(pc, rc, processing)
		{
		}

		// Token: 0x06007125 RID: 28965 RVA: 0x001D6C70 File Offset: 0x001D4E70
		protected override void ProcessReport(ProcessingErrorContext errorContext, ExecutionLogContext executionLogContext, ref UserProfileState userProfileState)
		{
			ChunkFactoryAdapter chunkFactoryAdapter = new ChunkFactoryAdapter(base.PublicProcessingContext.ChunkFactory);
			Hashtable hashtable;
			Hashtable hashtable2;
			IntermediateFormatReader.State state;
			bool flag;
			this.m_reportSnapshot = ReportProcessing.DeserializeReportSnapshot(new ReportProcessing.GetReportChunk(chunkFactoryAdapter.GetReportChunk), new ReportProcessing.CreateReportChunk(chunkFactoryAdapter.CreateReportChunk), base.PublicProcessingContext.GetResourceCallback, base.PublicRenderingContext, base.PublicProcessingContext.DataProtection, out hashtable, out hashtable2, out state, out flag);
			Global.Tracer.Assert(this.m_reportSnapshot != null, "(null != reportSnapshot)");
			Global.Tracer.Assert(this.m_reportSnapshot.Report != null, "(null != reportSnapshot.Report)");
			Global.Tracer.Assert(this.m_reportSnapshot.ReportInstance != null, "(null != reportSnapshot.ReportInstance)");
			this.m_chunkManager = new ChunkManager.RenderingChunkManager(new ReportProcessing.GetReportChunk(chunkFactoryAdapter.GetReportChunk), hashtable, hashtable2, state, this.m_reportSnapshot.Report.IntermediateFormatVersion);
			bool flag2;
			EventInformation eventInformation;
			base.Processing.ProcessShowHideToggle(base.PublicRenderingContext.ShowHideToggle, this.m_reportSnapshot, base.PublicRenderingContext.EventInfo, this.m_chunkManager, out flag2, out eventInformation);
			if (flag2)
			{
				base.PublicRenderingContext.EventInfo = eventInformation;
			}
			bool flag3 = this.IsRenderStream || !flag;
			this.m_renderingContext = new RenderingContext(this.m_reportSnapshot, base.PublicRenderingContext.Format, this.m_reportSnapshot.ExecutionTime, this.m_reportSnapshot.Report.EmbeddedImages, this.m_reportSnapshot.Report.ImageStreamNames, base.PublicRenderingContext.EventInfo, base.PublicRenderingContext.ReportContext, base.PublicRenderingContext.ReportUri, base.RenderingParameters, new ReportProcessing.GetReportChunk(chunkFactoryAdapter.GetReportChunk), this.m_chunkManager, base.PublicProcessingContext.GetResourceCallback, new ReportProcessing.GetChunkMimeType(chunkFactoryAdapter.GetChunkMimeType), base.PublicRenderingContext.StoreServerParametersCallback, flag3, base.PublicRenderingContext.AllowUserProfileState, base.PublicRenderingContext.ReportRuntimeSetup, base.PublicProcessingContext.JobContext, base.PublicProcessingContext.DataProtection);
		}

		// Token: 0x17002684 RID: 9860
		// (get) Token: 0x06007126 RID: 28966 RVA: 0x001D6E77 File Offset: 0x001D5077
		protected virtual bool IsRenderStream
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007127 RID: 28967 RVA: 0x001D6E7A File Offset: 0x001D507A
		protected override void PrepareForExecution()
		{
		}

		// Token: 0x06007128 RID: 28968 RVA: 0x001D6E7C File Offset: 0x001D507C
		protected override void CleanupSuccessfulProcessing(ProcessingErrorContext errorContext)
		{
			if (!this.IsRenderStream)
			{
				errorContext.Combine(this.m_reportSnapshot.Warnings);
			}
		}

		// Token: 0x06007129 RID: 28969 RVA: 0x001D6E98 File Offset: 0x001D5098
		protected override OnDemandProcessingResult ConstructProcessingResult(bool eventInfoChanged, Hashtable renderProperties, ProcessingErrorContext errorContext, UserProfileState userProfileState, bool renderingInfoChanged, ExecutionLogContext executionLogContext)
		{
			ReportInstanceInfo reportInstanceInfo = (ReportInstanceInfo)this.m_reportSnapshot.ReportInstance.GetInstanceInfo(this.m_renderingContext.ChunkManager);
			return new YukonProcessingResult(renderingInfoChanged, base.PublicProcessingContext.ChunkFactory, this.m_reportSnapshot.HasShowHide, this.m_renderingContext.RenderingInfoManager, eventInfoChanged, base.PublicRenderingContext.EventInfo, reportInstanceInfo.Parameters, errorContext.Messages, this.m_reportSnapshot.Report.AutoRefresh, base.GetNumberOfPages(renderProperties), base.GetUpdatedPaginationMode(renderProperties, base.PublicRenderingContext.ClientPaginationMode), base.PublicProcessingContext.ChunkFactory.ReportProcessingFlags, this.m_renderingContext.UsedUserProfileState, executionLogContext);
		}
	}
}
