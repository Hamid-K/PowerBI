using System;
using System.Collections;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007D2 RID: 2002
	internal abstract class RenderReportOdp : RenderReport
	{
		// Token: 0x060070F0 RID: 28912 RVA: 0x001D628C File Offset: 0x001D448C
		public RenderReportOdp(ProcessingContext pc, RenderingContext rc, IConfiguration configuration)
			: base(pc, rc)
		{
			this.m_configuration = configuration;
		}

		// Token: 0x060070F1 RID: 28913 RVA: 0x001D62A0 File Offset: 0x001D44A0
		protected override Microsoft.ReportingServices.OnDemandReportRendering.Report PrepareROM(out RenderingContext odpRenderingContext)
		{
			odpRenderingContext = new RenderingContext(base.PublicRenderingContext.Format, this.m_odpReportSnapshot, base.PublicRenderingContext.EventInfo, this.m_odpContext);
			return new Microsoft.ReportingServices.OnDemandReportRendering.Report(this.m_odpReportSnapshot.Report, this.m_odpReportSnapshot.ReportInstance, odpRenderingContext, base.ReportName, base.PublicRenderingContext.ReportDescription);
		}

		// Token: 0x060070F2 RID: 28914 RVA: 0x001D6304 File Offset: 0x001D4504
		protected override void CleanupSuccessfulProcessing(ProcessingErrorContext errorContext)
		{
			ReportProcessing.CleanupOnDemandProcessing(this.m_odpContext, true);
		}

		// Token: 0x060070F3 RID: 28915 RVA: 0x001D6314 File Offset: 0x001D4514
		protected override OnDemandProcessingResult ConstructProcessingResult(bool eventInfoChanged, Hashtable renderProperties, ProcessingErrorContext errorContext, UserProfileState userProfileState, bool renderingInfoChanged, ExecutionLogContext executionLogContext)
		{
			return new FullOnDemandProcessingResult(this.m_odpReportSnapshot, this.m_odpContext.OdpMetadata.OdpChunkManager, this.m_odpContext.OdpMetadata.SnapshotHasChanged, base.PublicProcessingContext.ChunkFactory, base.PublicProcessingContext.Parameters, this.m_odpReportSnapshot.Report.EvaluateAutoRefresh(null, this.m_odpContext), base.GetNumberOfPages(renderProperties), errorContext.Messages, eventInfoChanged, base.PublicRenderingContext.EventInfo, base.GetUpdatedPaginationMode(renderProperties, base.PublicRenderingContext.ClientPaginationMode), base.PublicProcessingContext.ChunkFactory.ReportProcessingFlags, this.m_odpContext.HasUserProfileState, executionLogContext);
		}

		// Token: 0x060070F4 RID: 28916 RVA: 0x001D63C1 File Offset: 0x001D45C1
		protected override void FinalCleanup()
		{
			if (this.m_odpContext != null)
			{
				this.m_odpContext.FreeAllResources();
			}
		}

		// Token: 0x060070F5 RID: 28917 RVA: 0x001D63D6 File Offset: 0x001D45D6
		protected override void CleanupForException()
		{
			ReportProcessing.RequestErrorGroupTreeCleanup(this.m_odpContext);
		}

		// Token: 0x060070F6 RID: 28918 RVA: 0x001D63E4 File Offset: 0x001D45E4
		protected override void UpdateEventInfoInSnapshot()
		{
			Global.Tracer.Assert(this.m_odpReportSnapshot != null, "Snapshot must exist for ODP Engine");
			if (this.m_odpContext.NewSortFilterEventInfo != null && this.m_odpContext.NewSortFilterEventInfo.Count > 0)
			{
				this.m_odpReportSnapshot.SortFilterEventInfo = this.m_odpContext.NewSortFilterEventInfo;
				return;
			}
			this.m_odpReportSnapshot.SortFilterEventInfo = null;
		}

		// Token: 0x1700267A RID: 9850
		// (get) Token: 0x060070F7 RID: 28919 RVA: 0x001D644C File Offset: 0x001D464C
		protected OnDemandProcessingContext OdpContext
		{
			get
			{
				return this.m_odpContext;
			}
		}

		// Token: 0x1700267B RID: 9851
		// (get) Token: 0x060070F8 RID: 28920 RVA: 0x001D6454 File Offset: 0x001D4654
		protected override ProcessingEngine RunningProcessingEngine
		{
			get
			{
				return ProcessingEngine.OnDemandEngine;
			}
		}

		// Token: 0x1700267C RID: 9852
		// (get) Token: 0x060070F9 RID: 28921 RVA: 0x001D6457 File Offset: 0x001D4657
		protected IConfiguration Configuration
		{
			get
			{
				return this.m_configuration;
			}
		}

		// Token: 0x04003A46 RID: 14918
		protected OnDemandProcessingContext m_odpContext;

		// Token: 0x04003A47 RID: 14919
		protected ReportSnapshot m_odpReportSnapshot;

		// Token: 0x04003A48 RID: 14920
		private readonly IConfiguration m_configuration;
	}
}
