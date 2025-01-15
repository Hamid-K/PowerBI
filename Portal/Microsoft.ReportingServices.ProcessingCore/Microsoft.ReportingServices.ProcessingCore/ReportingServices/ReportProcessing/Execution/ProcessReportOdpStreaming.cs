using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007C9 RID: 1993
	internal class ProcessReportOdpStreaming : ProcessReportOdpInitial
	{
		// Token: 0x060070AF RID: 28847 RVA: 0x001D58AC File Offset: 0x001D3AAC
		public ProcessReportOdpStreaming(IConfiguration configuration, ProcessingContext pc, Report report, ErrorContext errorContext, ReportProcessing.StoreServerParameters storeServerParameters, GlobalIDOwnerCollection globalIDOwnerCollection, ExecutionLogContext executionLogContext, DateTime executionTime, IAbortHelper abortHelper)
			: base(configuration, pc, report, errorContext, storeServerParameters, globalIDOwnerCollection, executionLogContext, executionTime)
		{
			this.m_abortHelper = abortHelper;
		}

		// Token: 0x060070B0 RID: 28848 RVA: 0x001D58D4 File Offset: 0x001D3AD4
		protected override void PreProcessSnapshot(OnDemandProcessingContext odpContext, Merge odpMerge, ReportInstance reportInstance, ReportSnapshot reportSnapshot)
		{
			base.SetupInitialOdpState(odpContext, reportInstance, reportSnapshot);
		}

		// Token: 0x060070B1 RID: 28849 RVA: 0x001D58E0 File Offset: 0x001D3AE0
		protected override IAbortHelper GetAbortHelper()
		{
			return this.m_abortHelper ?? base.GetAbortHelper();
		}

		// Token: 0x1700266B RID: 9835
		// (get) Token: 0x060070B2 RID: 28850 RVA: 0x001D58F2 File Offset: 0x001D3AF2
		protected override OnDemandProcessingContext.Mode OnDemandProcessingMode
		{
			get
			{
				return OnDemandProcessingContext.Mode.Streaming;
			}
		}

		// Token: 0x060070B3 RID: 28851 RVA: 0x001D58F5 File Offset: 0x001D3AF5
		protected override void CleanupAbortHandler(OnDemandProcessingContext odpContext)
		{
		}

		// Token: 0x04003A39 RID: 14905
		private readonly IAbortHelper m_abortHelper;
	}
}
