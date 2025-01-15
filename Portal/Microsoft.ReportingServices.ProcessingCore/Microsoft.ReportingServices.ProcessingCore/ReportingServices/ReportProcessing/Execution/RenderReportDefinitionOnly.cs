using System;
using System.Collections;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007D9 RID: 2009
	internal class RenderReportDefinitionOnly : RenderReportOdpInitial
	{
		// Token: 0x06007110 RID: 28944 RVA: 0x001D6781 File Offset: 0x001D4981
		public RenderReportDefinitionOnly(ProcessingContext pc, RenderingContext rc, DateTime executionTimeStamp, IConfiguration configuration)
			: base(pc, rc, executionTimeStamp, configuration)
		{
		}

		// Token: 0x06007111 RID: 28945 RVA: 0x001D678E File Offset: 0x001D498E
		protected override void CleanupForException()
		{
		}

		// Token: 0x06007112 RID: 28946 RVA: 0x001D6790 File Offset: 0x001D4990
		protected override void FinalCleanup()
		{
		}

		// Token: 0x06007113 RID: 28947 RVA: 0x001D6792 File Offset: 0x001D4992
		protected override void PrepareForExecution()
		{
			base.ValidateReportParameters();
			ReportProcessing.CheckReportCredentialsAndConnectionUserDependency(base.PublicProcessingContext);
		}

		// Token: 0x06007114 RID: 28948 RVA: 0x001D67A5 File Offset: 0x001D49A5
		protected override void UpdateEventInfoInSnapshot()
		{
		}

		// Token: 0x06007115 RID: 28949 RVA: 0x001D67A8 File Offset: 0x001D49A8
		protected override void ProcessReport(ProcessingErrorContext errorContext, ExecutionLogContext executionLogContext, ref UserProfileState userProfileState)
		{
			GlobalIDOwnerCollection globalIDOwnerCollection;
			Microsoft.ReportingServices.ReportIntermediateFormat.Report reportDefinition = base.GetReportDefinition(out globalIDOwnerCollection);
			ProcessReportDefinitionOnly processReportDefinitionOnly = new ProcessReportDefinitionOnly(base.Configuration, base.PublicProcessingContext, reportDefinition, errorContext, base.PublicRenderingContext.StoreServerParametersCallback, globalIDOwnerCollection, executionLogContext, base.ExecutionTimeStamp);
			this.m_odpReportSnapshot = processReportDefinitionOnly.Execute(out this.m_odpContext);
		}

		// Token: 0x06007116 RID: 28950 RVA: 0x001D67F8 File Offset: 0x001D49F8
		protected override OnDemandProcessingResult ConstructProcessingResult(bool eventInfoChanged, Hashtable renderProperties, ProcessingErrorContext errorContext, UserProfileState userProfileState, bool renderingInfoChanged, ExecutionLogContext executionLogContext)
		{
			return new DefinitionOnlyOnDemandProcessingResult(this.m_odpReportSnapshot, this.m_odpContext.OdpMetadata.OdpChunkManager, this.m_odpContext.OdpMetadata.SnapshotHasChanged, base.PublicProcessingContext.ChunkFactory, base.PublicProcessingContext.Parameters, 0, base.GetNumberOfPages(renderProperties), errorContext.Messages, eventInfoChanged, base.PublicRenderingContext.EventInfo, base.GetUpdatedPaginationMode(renderProperties, base.PublicRenderingContext.ClientPaginationMode), base.PublicProcessingContext.ChunkFactory.ReportProcessingFlags, this.m_odpContext.HasUserProfileState, executionLogContext);
		}

		// Token: 0x06007117 RID: 28951 RVA: 0x001D6890 File Offset: 0x001D4A90
		protected override Microsoft.ReportingServices.OnDemandReportRendering.Report PrepareROM(out RenderingContext odpRenderingContext)
		{
			odpRenderingContext = new RenderingContext(base.PublicRenderingContext.Format, this.m_odpReportSnapshot, base.PublicRenderingContext.EventInfo, this.m_odpContext);
			odpRenderingContext.InstanceAccessDisallowed = true;
			return new Microsoft.ReportingServices.OnDemandReportRendering.Report(this.m_odpReportSnapshot.Report, odpRenderingContext, base.ReportName, base.PublicRenderingContext.ReportDescription);
		}
	}
}
