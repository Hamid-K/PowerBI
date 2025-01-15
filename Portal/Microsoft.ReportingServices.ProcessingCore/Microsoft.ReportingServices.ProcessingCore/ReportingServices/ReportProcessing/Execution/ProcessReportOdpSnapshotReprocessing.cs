using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007CE RID: 1998
	internal class ProcessReportOdpSnapshotReprocessing : ProcessReportOdpSnapshot
	{
		// Token: 0x060070CD RID: 28877 RVA: 0x001D5BF4 File Offset: 0x001D3DF4
		public ProcessReportOdpSnapshotReprocessing(IConfiguration configuration, ProcessingContext pc, Report report, ErrorContext errorContext, ReportProcessing.StoreServerParameters storeServerParameters, GlobalIDOwnerCollection globalIDOwnerCollection, ExecutionLogContext executionLogContext, OnDemandMetadata odpMetadataFromSnapshot)
			: base(configuration, pc, report, errorContext, storeServerParameters, globalIDOwnerCollection, executionLogContext, odpMetadataFromSnapshot)
		{
		}

		// Token: 0x060070CE RID: 28878 RVA: 0x001D5C14 File Offset: 0x001D3E14
		protected override OnDemandMetadata PrepareMetadata()
		{
			return new OnDemandMetadata(base.OdpMetadataFromSnapshot, base.ReportDefinition)
			{
				ReportSnapshot = new ReportSnapshot(base.ReportDefinition, base.PublicProcessingContext.ReportContext.ItemName, base.PublicProcessingContext.Parameters, base.PublicProcessingContext.RequestUserName, base.OdpMetadataFromSnapshot.ReportSnapshot.ExecutionTime, base.PublicProcessingContext.ReportContext.HostRootUri, base.PublicProcessingContext.ReportContext.ParentPath, base.PublicProcessingContext.UserLanguage.Name)
			};
		}

		// Token: 0x060070CF RID: 28879 RVA: 0x001D5CA9 File Offset: 0x001D3EA9
		protected override void SetupReportLanguage(Merge odpMerge, ReportInstance reportInstance)
		{
			odpMerge.EvaluateReportLanguage(reportInstance, null);
		}

		// Token: 0x17002673 RID: 9843
		// (get) Token: 0x060070D0 RID: 28880 RVA: 0x001D5CB3 File Offset: 0x001D3EB3
		protected override bool ReprocessSnapshot
		{
			get
			{
				return true;
			}
		}
	}
}
