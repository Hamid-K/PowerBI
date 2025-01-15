using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004DB RID: 1243
	internal interface IAggregateHolder
	{
		// Token: 0x06003EB0 RID: 16048
		List<DataAggregateInfo> GetAggregateList();

		// Token: 0x06003EB1 RID: 16049
		List<DataAggregateInfo> GetPostSortAggregateList();

		// Token: 0x06003EB2 RID: 16050
		void ClearIfEmpty();

		// Token: 0x17001AA7 RID: 6823
		// (get) Token: 0x06003EB3 RID: 16051
		DataScopeInfo DataScopeInfo { get; }
	}
}
