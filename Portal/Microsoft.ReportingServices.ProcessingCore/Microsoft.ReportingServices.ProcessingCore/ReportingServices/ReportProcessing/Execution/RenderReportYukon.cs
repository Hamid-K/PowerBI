using System;
using System.Collections;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007DA RID: 2010
	internal abstract class RenderReportYukon : RenderReport
	{
		// Token: 0x06007118 RID: 28952 RVA: 0x001D68F1 File Offset: 0x001D4AF1
		public RenderReportYukon(ProcessingContext pc, Microsoft.ReportingServices.ReportProcessing.RenderingContext rc, ReportProcessing processing)
			: base(pc, rc)
		{
			this.m_processing = processing;
		}

		// Token: 0x06007119 RID: 28953 RVA: 0x001D6904 File Offset: 0x001D4B04
		protected override Microsoft.ReportingServices.OnDemandReportRendering.Report PrepareROM(out Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext odpRenderingContext)
		{
			odpRenderingContext = new Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext(base.PublicRenderingContext.Format, this.m_reportSnapshot, base.PublicProcessingContext.ChunkFactory, base.PublicRenderingContext.EventInfo);
			return new Microsoft.ReportingServices.OnDemandReportRendering.Report(this.m_reportSnapshot.Report, this.m_reportSnapshot.ReportInstance, this.m_renderingContext, odpRenderingContext, base.ReportName, base.PublicRenderingContext.ReportDescription);
		}

		// Token: 0x0600711A RID: 28954 RVA: 0x001D6973 File Offset: 0x001D4B73
		protected override void FinalCleanup()
		{
			if (this.m_chunkManager != null)
			{
				this.m_chunkManager.Close();
			}
		}

		// Token: 0x17002681 RID: 9857
		// (get) Token: 0x0600711B RID: 28955 RVA: 0x001D6988 File Offset: 0x001D4B88
		protected override ProcessingEngine RunningProcessingEngine
		{
			get
			{
				return ProcessingEngine.YukonEngine;
			}
		}

		// Token: 0x0600711C RID: 28956 RVA: 0x001D698C File Offset: 0x001D4B8C
		protected override OnDemandProcessingResult ConstructProcessingResult(bool eventInfoChanged, Hashtable renderProperties, ProcessingErrorContext errorContext, UserProfileState userProfileState, bool renderingInfoChanged, ExecutionLogContext executionLogContext)
		{
			return new YukonProcessingResult(this.m_reportSnapshot, this.m_context.ChunkManager, base.PublicProcessingContext.ChunkFactory, base.PublicProcessingContext.Parameters, this.m_reportSnapshot.Report.AutoRefresh, base.GetNumberOfPages(renderProperties), errorContext.Messages, true, this.m_renderingContext.RenderingInfoManager, eventInfoChanged, base.PublicRenderingContext.EventInfo, base.GetUpdatedPaginationMode(renderProperties, base.PublicRenderingContext.ClientPaginationMode), base.PublicProcessingContext.ChunkFactory.ReportProcessingFlags, userProfileState | this.m_renderingContext.UsedUserProfileState, executionLogContext);
		}

		// Token: 0x17002682 RID: 9858
		// (get) Token: 0x0600711D RID: 28957 RVA: 0x001D6A2C File Offset: 0x001D4C2C
		protected ReportProcessing Processing
		{
			get
			{
				return this.m_processing;
			}
		}

		// Token: 0x17002683 RID: 9859
		// (get) Token: 0x0600711E RID: 28958 RVA: 0x001D6A34 File Offset: 0x001D4C34
		protected Uri ReportUri
		{
			get
			{
				return base.PublicRenderingContext.ReportUri;
			}
		}

		// Token: 0x04003A4F RID: 14927
		protected ChunkManager.RenderingChunkManager m_chunkManager;

		// Token: 0x04003A50 RID: 14928
		protected ReportProcessing.ProcessingContext m_context;

		// Token: 0x04003A51 RID: 14929
		protected Microsoft.ReportingServices.ReportRendering.RenderingContext m_renderingContext;

		// Token: 0x04003A52 RID: 14930
		protected ReportSnapshot m_reportSnapshot;

		// Token: 0x04003A53 RID: 14931
		private readonly ReportProcessing m_processing;
	}
}
