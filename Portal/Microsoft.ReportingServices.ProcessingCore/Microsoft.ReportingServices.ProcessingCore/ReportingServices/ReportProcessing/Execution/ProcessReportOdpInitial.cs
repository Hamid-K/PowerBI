using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007C8 RID: 1992
	internal class ProcessReportOdpInitial : ProcessReportOdp
	{
		// Token: 0x060070A7 RID: 28839 RVA: 0x001D5716 File Offset: 0x001D3916
		public ProcessReportOdpInitial(IConfiguration configuration, ProcessingContext pc, Report report, ErrorContext errorContext, ReportProcessing.StoreServerParameters storeServerParameters, GlobalIDOwnerCollection globalIDOwnerCollection, ExecutionLogContext executionLogContext, DateTime executionTime)
			: base(configuration, pc, report, errorContext, storeServerParameters, globalIDOwnerCollection, executionLogContext)
		{
			this.m_executionTime = executionTime;
		}

		// Token: 0x060070A8 RID: 28840 RVA: 0x001D5734 File Offset: 0x001D3934
		protected override OnDemandMetadata PrepareMetadata()
		{
			OnDemandMetadata onDemandMetadata = new OnDemandMetadata(base.ReportDefinition);
			ReportSnapshot reportSnapshot = new ReportSnapshot(base.ReportDefinition, base.PublicProcessingContext.ReportContext.ItemName, base.PublicProcessingContext.Parameters, base.PublicProcessingContext.RequestUserName, this.m_executionTime, base.PublicProcessingContext.ReportContext.HostRootUri, base.PublicProcessingContext.ReportContext.ParentPath, base.PublicProcessingContext.UserLanguage.Name);
			onDemandMetadata.ReportSnapshot = reportSnapshot;
			return onDemandMetadata;
		}

		// Token: 0x060070A9 RID: 28841 RVA: 0x001D57BB File Offset: 0x001D39BB
		protected override void SetupReportLanguage(Merge odpMerge, ReportInstance reportInstance)
		{
			odpMerge.EvaluateReportLanguage(reportInstance, null);
		}

		// Token: 0x060070AA RID: 28842 RVA: 0x001D57C8 File Offset: 0x001D39C8
		protected override void PreProcessSnapshot(OnDemandProcessingContext odpContext, Merge odpMerge, ReportInstance reportInstance, ReportSnapshot reportSnapshot)
		{
			if (base.ReportDefinition.HasSubReports)
			{
				ReportProcessing.FetchSubReports(base.ReportDefinition, odpContext.ChunkFactory, odpContext.ErrorContext, odpContext.OdpMetadata, odpContext.ReportContext, odpContext.SubReportCallback, 0, odpContext.SnapshotProcessing, odpContext.ProcessWithCachedData, base.GlobalIDOwnerCollection, base.PublicProcessingContext.QueryParameters);
				SubReportInitializer.InitializeSubReportOdpContext(base.ReportDefinition, odpContext);
			}
			odpMerge.FetchData(reportInstance, false);
			reportInstance.CalculateAndStoreReportVariables(odpContext);
			if (base.ReportDefinition.HasSubReports)
			{
				SubReportInitializer.InitializeSubReports(base.ReportDefinition, reportInstance, odpContext, false, false);
			}
			base.SetupInitialOdpState(odpContext, reportInstance, reportSnapshot);
			if (base.ReportDefinition.HasSubReports || (!base.ReportDefinition.DeferVariableEvaluation && base.ReportDefinition.HasVariables))
			{
				Merge.PreProcessTablixes(base.ReportDefinition, odpContext, true);
			}
		}

		// Token: 0x17002667 RID: 9831
		// (get) Token: 0x060070AB RID: 28843 RVA: 0x001D58A0 File Offset: 0x001D3AA0
		protected override bool SnapshotProcessing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002668 RID: 9832
		// (get) Token: 0x060070AC RID: 28844 RVA: 0x001D58A3 File Offset: 0x001D3AA3
		protected override bool ReprocessSnapshot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002669 RID: 9833
		// (get) Token: 0x060070AD RID: 28845 RVA: 0x001D58A6 File Offset: 0x001D3AA6
		protected override bool ProcessWithCachedData
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700266A RID: 9834
		// (get) Token: 0x060070AE RID: 28846 RVA: 0x001D58A9 File Offset: 0x001D3AA9
		protected override OnDemandProcessingContext.Mode OnDemandProcessingMode
		{
			get
			{
				return OnDemandProcessingContext.Mode.Full;
			}
		}

		// Token: 0x04003A38 RID: 14904
		private readonly DateTime m_executionTime;
	}
}
