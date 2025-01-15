using System;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004DE RID: 1246
	internal interface IRIFReportIntersectionScope : IRIFReportDataScope, IRIFReportScope, IInstancePath, IRIFDataScope
	{
		// Token: 0x17001AAF RID: 6831
		// (get) Token: 0x06003EC8 RID: 16072
		IRIFReportDataScope ParentRowReportScope { get; }

		// Token: 0x17001AB0 RID: 6832
		// (get) Token: 0x06003EC9 RID: 16073
		IRIFReportDataScope ParentColumnReportScope { get; }

		// Token: 0x17001AB1 RID: 6833
		// (get) Token: 0x06003ECA RID: 16074
		bool IsColumnOuterGrouping { get; }

		// Token: 0x06003ECB RID: 16075
		void BindToStreamingScopeInstance(IReference<IOnDemandMemberInstance> parentRowScopeInstance, IReference<IOnDemandMemberInstance> parentColumnScopeInstance);
	}
}
