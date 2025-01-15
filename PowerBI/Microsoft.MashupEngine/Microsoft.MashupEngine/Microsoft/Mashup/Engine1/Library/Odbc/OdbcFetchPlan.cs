using System;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200059F RID: 1439
	internal class OdbcFetchPlan
	{
		// Token: 0x06002D89 RID: 11657 RVA: 0x0008A6CC File Offset: 0x000888CC
		public OdbcFetchPlan(OdbcPageReaderColumnInfo[] columnInfos, bool useMultipleRowFetch, int maxRowCount, int maxCellByteLength)
		{
			this.ColumnInfos = columnInfos;
			this.UseMultipleRowFetch = useMultipleRowFetch;
			this.MaxRowCount = maxRowCount;
			this.MaxCellByteLength = maxCellByteLength;
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void BindColumnFailureHandler(OdbcPageReaderColumnInfo columnInfo, Exception exception)
		{
		}

		// Token: 0x040013D1 RID: 5073
		public readonly OdbcPageReaderColumnInfo[] ColumnInfos;

		// Token: 0x040013D2 RID: 5074
		public readonly int MaxRowCount;

		// Token: 0x040013D3 RID: 5075
		public readonly int MaxCellByteLength;

		// Token: 0x040013D4 RID: 5076
		public readonly bool UseMultipleRowFetch;
	}
}
