using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007CC RID: 1996
	internal sealed class ProcessReportOdpWithCachedData : ProcessReportOdpInitial
	{
		// Token: 0x060070C0 RID: 28864 RVA: 0x001D5A98 File Offset: 0x001D3C98
		public ProcessReportOdpWithCachedData(IConfiguration configuration, ProcessingContext pc, Report report, ErrorContext errorContext, ReportProcessing.StoreServerParameters storeServerParameters, GlobalIDOwnerCollection globalIDOwnerCollection, ExecutionLogContext executionLogContext, DateTime executionTime, OnDemandMetadata odpMetadataFromDataCache)
			: base(configuration, pc, report, errorContext, storeServerParameters, globalIDOwnerCollection, executionLogContext, executionTime)
		{
			Global.Tracer.Assert(odpMetadataFromDataCache != null, "Must provide existing metadata to process with cached data");
			this.m_odpMetadataFromDataCache = odpMetadataFromDataCache;
		}

		// Token: 0x060070C1 RID: 28865 RVA: 0x001D5AD4 File Offset: 0x001D3CD4
		protected override OnDemandMetadata PrepareMetadata()
		{
			OnDemandMetadata onDemandMetadata = base.PrepareMetadata();
			onDemandMetadata.PrepareForCachedDataProcessing(this.m_odpMetadataFromDataCache);
			return onDemandMetadata;
		}

		// Token: 0x1700266D RID: 9837
		// (get) Token: 0x060070C2 RID: 28866 RVA: 0x001D5AE8 File Offset: 0x001D3CE8
		protected override bool ProcessWithCachedData
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04003A3B RID: 14907
		private readonly OnDemandMetadata m_odpMetadataFromDataCache;
	}
}
