using System;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200082C RID: 2092
	public interface IOnDemandScopeInstance : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600756F RID: 30063
		void SetupEnvironment();

		// Token: 0x06007570 RID: 30064
		IOnDemandMemberOwnerInstanceReference GetDataRegionInstance(DataRegion rifDataRegion);

		// Token: 0x06007571 RID: 30065
		IReference<IDataCorrelation> GetIdcReceiver(IRIFReportDataScope scope);

		// Token: 0x17002795 RID: 10133
		// (get) Token: 0x06007572 RID: 30066
		bool IsNoRows { get; }

		// Token: 0x17002796 RID: 10134
		// (get) Token: 0x06007573 RID: 30067
		bool IsMostRecentlyCreatedScopeInstance { get; }

		// Token: 0x17002797 RID: 10135
		// (get) Token: 0x06007574 RID: 30068
		bool HasUnProcessedServerAggregate { get; }
	}
}
