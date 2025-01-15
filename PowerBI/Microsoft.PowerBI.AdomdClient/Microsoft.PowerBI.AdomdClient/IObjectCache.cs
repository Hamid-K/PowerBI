using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000095 RID: 149
	internal interface IObjectCache
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060008B6 RID: 2230
		bool IsPopulated { get; }

		// Token: 0x060008B7 RID: 2231
		void Populate();

		// Token: 0x060008B8 RID: 2232
		DataRowCollection GetNonFilteredRows();

		// Token: 0x060008B9 RID: 2233
		DataRow[] GetFilteredRows(DataRow parentRow, string filter);

		// Token: 0x060008BA RID: 2234
		void Refresh();

		// Token: 0x060008BB RID: 2235
		void CheckCacheIsValid();

		// Token: 0x060008BC RID: 2236
		void MarkNeedCheckForValidness();

		// Token: 0x060008BD RID: 2237
		void MarkAbandoned();

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060008BE RID: 2238
		DataSet CacheDataSet { get; }
	}
}
