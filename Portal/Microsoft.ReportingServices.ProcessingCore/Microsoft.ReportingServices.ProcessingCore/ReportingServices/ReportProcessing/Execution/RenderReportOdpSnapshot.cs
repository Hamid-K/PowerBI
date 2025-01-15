using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007D5 RID: 2005
	internal class RenderReportOdpSnapshot : RenderReportOdp
	{
		// Token: 0x06007101 RID: 28929 RVA: 0x001D6559 File Offset: 0x001D4759
		public RenderReportOdpSnapshot(ProcessingContext pc, RenderingContext rc, IConfiguration configuration)
			: base(pc, rc, configuration)
		{
		}

		// Token: 0x06007102 RID: 28930 RVA: 0x001D6564 File Offset: 0x001D4764
		protected override void PrepareForExecution()
		{
			EventInformation eventInformation;
			bool flag;
			ReportProcessing.ProcessOdpToggleEvent(base.PublicRenderingContext.ShowHideToggle, base.PublicProcessingContext.ChunkFactory, base.PublicRenderingContext.EventInfo, out eventInformation, out flag);
			if (flag)
			{
				base.PublicRenderingContext.EventInfo = eventInformation;
			}
		}

		// Token: 0x06007103 RID: 28931 RVA: 0x001D65AC File Offset: 0x001D47AC
		protected override void ProcessReport(ProcessingErrorContext errorContext, ExecutionLogContext executionLogContext, ref UserProfileState userProfileState)
		{
			OnDemandMetadata onDemandMetadata = null;
			Report report;
			GlobalIDOwnerCollection globalIDOwnerCollection = ChunkManager.OnDemandProcessingManager.DeserializeOdpReportSnapshot(base.PublicProcessingContext, null, errorContext, true, true, base.Configuration, ref onDemandMetadata, out report);
			this.m_odpReportSnapshot = onDemandMetadata.ReportSnapshot;
			new ProcessReportOdpSnapshot(base.Configuration, base.PublicProcessingContext, report, errorContext, base.PublicRenderingContext.StoreServerParametersCallback, globalIDOwnerCollection, executionLogContext, onDemandMetadata).Execute(out this.m_odpContext);
		}

		// Token: 0x1700267E RID: 9854
		// (get) Token: 0x06007104 RID: 28932 RVA: 0x001D660E File Offset: 0x001D480E
		protected virtual bool IsRenderStream
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007105 RID: 28933 RVA: 0x001D6611 File Offset: 0x001D4811
		protected override void CleanupSuccessfulProcessing(ProcessingErrorContext errorContext)
		{
			if (!this.IsRenderStream)
			{
				errorContext.Combine(this.m_odpReportSnapshot.Warnings);
			}
			base.CleanupSuccessfulProcessing(errorContext);
		}
	}
}
