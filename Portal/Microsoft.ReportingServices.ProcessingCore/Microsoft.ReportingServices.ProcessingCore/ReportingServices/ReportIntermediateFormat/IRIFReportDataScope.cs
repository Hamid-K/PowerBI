using System;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004DD RID: 1245
	public interface IRIFReportDataScope : IRIFReportScope, IInstancePath, IRIFDataScope
	{
		// Token: 0x17001AA9 RID: 6825
		// (get) Token: 0x06003EBB RID: 16059
		bool IsDataIntersectionScope { get; }

		// Token: 0x17001AAA RID: 6826
		// (get) Token: 0x06003EBC RID: 16060
		bool IsScope { get; }

		// Token: 0x17001AAB RID: 6827
		// (get) Token: 0x06003EBD RID: 16061
		IRIFReportDataScope ParentReportScope { get; }

		// Token: 0x17001AAC RID: 6828
		// (get) Token: 0x06003EBE RID: 16062
		IReference<IOnDemandScopeInstance> CurrentStreamingScopeInstance { get; }

		// Token: 0x17001AAD RID: 6829
		// (get) Token: 0x06003EBF RID: 16063
		bool IsGroup { get; }

		// Token: 0x06003EC0 RID: 16064
		void BindToStreamingScopeInstance(IReference<IOnDemandScopeInstance> scopeInstance);

		// Token: 0x06003EC1 RID: 16065
		void BindToNoRowsScopeInstance(OnDemandProcessingContext odpContext);

		// Token: 0x06003EC2 RID: 16066
		void ClearStreamingScopeInstanceBinding();

		// Token: 0x17001AAE RID: 6830
		// (get) Token: 0x06003EC3 RID: 16067
		bool IsBoundToStreamingScopeInstance { get; }

		// Token: 0x06003EC4 RID: 16068
		void ResetAggregates(AggregatesImpl reportOmAggregates);

		// Token: 0x06003EC5 RID: 16069
		bool HasServerAggregate(string aggregateName);

		// Token: 0x06003EC6 RID: 16070
		bool IsSameOrChildScopeOf(IRIFReportDataScope candidateScope);

		// Token: 0x06003EC7 RID: 16071
		bool IsChildScopeOf(IRIFReportDataScope candidateScope);
	}
}
