using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007D3 RID: 2003
	internal class RenderReportOdpInitial : RenderReportOdp
	{
		// Token: 0x060070FA RID: 28922 RVA: 0x001D645F File Offset: 0x001D465F
		public RenderReportOdpInitial(ProcessingContext pc, RenderingContext rc, DateTime executionTimeStamp, IConfiguration configuration)
			: base(pc, rc, configuration)
		{
			this.m_executionTimeStamp = executionTimeStamp;
		}

		// Token: 0x060070FB RID: 28923 RVA: 0x001D6472 File Offset: 0x001D4672
		protected override void PrepareForExecution()
		{
			base.ValidateReportParameters();
			ReportProcessing.CheckReportCredentialsAndConnectionUserDependency(base.PublicProcessingContext);
		}

		// Token: 0x060070FC RID: 28924 RVA: 0x001D6488 File Offset: 0x001D4688
		protected override void ProcessReport(ProcessingErrorContext errorContext, ExecutionLogContext executionLogContext, ref UserProfileState userProfileState)
		{
			GlobalIDOwnerCollection globalIDOwnerCollection;
			Report reportDefinition = this.GetReportDefinition(out globalIDOwnerCollection);
			ProcessReportOdpInitial processReportOdpInitial = new ProcessReportOdpInitial(base.Configuration, base.PublicProcessingContext, reportDefinition, errorContext, base.PublicRenderingContext.StoreServerParametersCallback, globalIDOwnerCollection, executionLogContext, this.m_executionTimeStamp);
			this.m_odpReportSnapshot = processReportOdpInitial.Execute(out this.m_odpContext);
		}

		// Token: 0x060070FD RID: 28925 RVA: 0x001D64D7 File Offset: 0x001D46D7
		protected Report GetReportDefinition(out GlobalIDOwnerCollection globalIDOwnerCollection)
		{
			globalIDOwnerCollection = new GlobalIDOwnerCollection();
			return ReportProcessing.DeserializeKatmaiReport(base.PublicProcessingContext.ChunkFactory, false, globalIDOwnerCollection);
		}

		// Token: 0x1700267D RID: 9853
		// (get) Token: 0x060070FE RID: 28926 RVA: 0x001D64F3 File Offset: 0x001D46F3
		protected DateTime ExecutionTimeStamp
		{
			get
			{
				return this.m_executionTimeStamp;
			}
		}

		// Token: 0x04003A49 RID: 14921
		private readonly DateTime m_executionTimeStamp;
	}
}
