using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007CF RID: 1999
	internal class ProcessReportOdpUserSort : ProcessReportOdpSnapshotReprocessing
	{
		// Token: 0x060070D1 RID: 28881 RVA: 0x001D5CB8 File Offset: 0x001D3EB8
		public ProcessReportOdpUserSort(IConfiguration configuration, ProcessingContext pc, Report report, ErrorContext errorContext, ReportProcessing.StoreServerParameters storeServerParameters, GlobalIDOwnerCollection globalIDOwnerCollection, ExecutionLogContext executionLogContext, OnDemandMetadata odpMetadataFromSnapshot, SortFilterEventInfoMap oldUserSortInformation, EventInformation newUserSortInformation, string oldUserSortEventSourceUniqueName)
			: base(configuration, pc, report, errorContext, storeServerParameters, globalIDOwnerCollection, executionLogContext, odpMetadataFromSnapshot)
		{
			this.m_oldUserSortInformation = oldUserSortInformation;
			this.m_newUserSortInformation = newUserSortInformation;
			this.m_oldUserSortEventSourceUniqueName = oldUserSortEventSourceUniqueName;
		}

		// Token: 0x060070D2 RID: 28882 RVA: 0x001D5CF0 File Offset: 0x001D3EF0
		protected override void CompleteOdpContext(OnDemandProcessingContext odpContext)
		{
			odpContext.OldSortFilterEventInfo = this.m_oldUserSortInformation;
			odpContext.UserSortFilterInfo = this.m_newUserSortInformation;
			odpContext.UserSortFilterEventSourceUniqueName = this.m_oldUserSortEventSourceUniqueName;
		}

		// Token: 0x060070D3 RID: 28883 RVA: 0x001D5D16 File Offset: 0x001D3F16
		protected override void PreProcessTablices(OnDemandProcessingContext odpContext, ReportSnapshot reportSnapshot)
		{
			Merge.PreProcessTablixes(base.ReportDefinition, odpContext, false);
			reportSnapshot.SortFilterEventInfo = odpContext.NewSortFilterEventInfo;
		}

		// Token: 0x04003A3D RID: 14909
		private readonly SortFilterEventInfoMap m_oldUserSortInformation;

		// Token: 0x04003A3E RID: 14910
		private readonly EventInformation m_newUserSortInformation;

		// Token: 0x04003A3F RID: 14911
		private readonly string m_oldUserSortEventSourceUniqueName;
	}
}
