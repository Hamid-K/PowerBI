using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000095 RID: 149
	internal interface IObjectCache
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060008C3 RID: 2243
		bool IsPopulated { get; }

		// Token: 0x060008C4 RID: 2244
		void Populate();

		// Token: 0x060008C5 RID: 2245
		DataRowCollection GetNonFilteredRows();

		// Token: 0x060008C6 RID: 2246
		DataRow[] GetFilteredRows(DataRow parentRow, string filter);

		// Token: 0x060008C7 RID: 2247
		void Refresh();

		// Token: 0x060008C8 RID: 2248
		void CheckCacheIsValid();

		// Token: 0x060008C9 RID: 2249
		void MarkNeedCheckForValidness();

		// Token: 0x060008CA RID: 2250
		void MarkAbandoned();

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060008CB RID: 2251
		DataSet CacheDataSet { get; }
	}
}
