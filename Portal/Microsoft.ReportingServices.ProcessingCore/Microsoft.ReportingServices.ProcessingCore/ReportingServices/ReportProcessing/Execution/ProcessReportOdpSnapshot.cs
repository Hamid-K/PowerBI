using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007CD RID: 1997
	internal class ProcessReportOdpSnapshot : ProcessReportOdp
	{
		// Token: 0x060070C3 RID: 28867 RVA: 0x001D5AEC File Offset: 0x001D3CEC
		public ProcessReportOdpSnapshot(IConfiguration configuration, ProcessingContext pc, Report report, ErrorContext errorContext, ReportProcessing.StoreServerParameters storeServerParameters, GlobalIDOwnerCollection globalIDOwnerCollection, ExecutionLogContext executionLogContext, OnDemandMetadata odpMetadataFromSnapshot)
			: base(configuration, pc, report, errorContext, storeServerParameters, globalIDOwnerCollection, executionLogContext)
		{
			Global.Tracer.Assert(odpMetadataFromSnapshot != null, "Must provide existing metadata when processing an existing snapshot");
			Global.Tracer.Assert(odpMetadataFromSnapshot.OdpChunkManager != null && odpMetadataFromSnapshot.ReportSnapshot != null, "Must provide chunk manager and ReportSnapshot when processing an existing snapshot");
			this.m_odpMetadataFromSnapshot = odpMetadataFromSnapshot;
		}

		// Token: 0x060070C4 RID: 28868 RVA: 0x001D5B4B File Offset: 0x001D3D4B
		protected override OnDemandMetadata PrepareMetadata()
		{
			Global.Tracer.Assert(this.m_odpMetadataFromSnapshot.ReportInstance != null, "Processing an existing snapshot with no ReportInstance");
			return this.m_odpMetadataFromSnapshot;
		}

		// Token: 0x060070C5 RID: 28869 RVA: 0x001D5B70 File Offset: 0x001D3D70
		protected override void SetupReportLanguage(Merge odpMerge, ReportInstance reportInstance)
		{
			odpMerge.EvaluateReportLanguage(reportInstance, reportInstance.Language);
		}

		// Token: 0x060070C6 RID: 28870 RVA: 0x001D5B80 File Offset: 0x001D3D80
		protected override void PreProcessSnapshot(OnDemandProcessingContext odpContext, Merge odpMerge, ReportInstance reportInstance, ReportSnapshot reportSnapshot)
		{
			base.SetupInitialOdpState(odpContext, reportInstance, reportSnapshot);
			if (base.ReportDefinition.HasSubReports)
			{
				SubReportInitializer.InitializeSubReportOdpContext(base.ReportDefinition, odpContext);
				SubReportInitializer.InitializeSubReports(base.ReportDefinition, reportInstance, odpContext, false, false);
			}
			this.PreProcessTablices(odpContext, reportSnapshot);
			reportInstance.CalculateAndStoreReportVariables(odpContext);
			odpContext.OdpMetadata.SetUpdatedVariableValues(odpContext, reportInstance);
		}

		// Token: 0x060070C7 RID: 28871 RVA: 0x001D5BDD File Offset: 0x001D3DDD
		protected virtual void PreProcessTablices(OnDemandProcessingContext odpContext, ReportSnapshot reportSnapshot)
		{
		}

		// Token: 0x1700266E RID: 9838
		// (get) Token: 0x060070C8 RID: 28872 RVA: 0x001D5BDF File Offset: 0x001D3DDF
		protected OnDemandMetadata OdpMetadataFromSnapshot
		{
			get
			{
				return this.m_odpMetadataFromSnapshot;
			}
		}

		// Token: 0x1700266F RID: 9839
		// (get) Token: 0x060070C9 RID: 28873 RVA: 0x001D5BE7 File Offset: 0x001D3DE7
		protected override bool SnapshotProcessing
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002670 RID: 9840
		// (get) Token: 0x060070CA RID: 28874 RVA: 0x001D5BEA File Offset: 0x001D3DEA
		protected override bool ReprocessSnapshot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002671 RID: 9841
		// (get) Token: 0x060070CB RID: 28875 RVA: 0x001D5BED File Offset: 0x001D3DED
		protected override bool ProcessWithCachedData
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002672 RID: 9842
		// (get) Token: 0x060070CC RID: 28876 RVA: 0x001D5BF0 File Offset: 0x001D3DF0
		protected override OnDemandProcessingContext.Mode OnDemandProcessingMode
		{
			get
			{
				return OnDemandProcessingContext.Mode.Full;
			}
		}

		// Token: 0x04003A3C RID: 14908
		private readonly OnDemandMetadata m_odpMetadataFromSnapshot;
	}
}
