using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006C1 RID: 1729
	internal interface IAggregateHolder
	{
		// Token: 0x06005CF7 RID: 23799
		DataAggregateInfoList[] GetAggregateLists();

		// Token: 0x06005CF8 RID: 23800
		DataAggregateInfoList[] GetPostSortAggregateLists();

		// Token: 0x06005CF9 RID: 23801
		void ClearIfEmpty();
	}
}
