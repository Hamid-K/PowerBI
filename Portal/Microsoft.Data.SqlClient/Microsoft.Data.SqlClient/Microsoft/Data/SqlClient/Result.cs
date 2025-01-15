using System;
using System.Collections.Generic;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000D5 RID: 213
	internal sealed class Result
	{
		// Token: 0x06000EF1 RID: 3825 RVA: 0x0002F750 File Offset: 0x0002D950
		internal Result(_SqlMetaDataSet metadata)
		{
			this._metadata = metadata;
			this._rowset = new List<Row>();
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x0002F76A File Offset: 0x0002D96A
		internal int Count
		{
			get
			{
				return this._rowset.Count;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0002F777 File Offset: 0x0002D977
		internal _SqlMetaDataSet MetaData
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x17000804 RID: 2052
		internal Row this[int index]
		{
			get
			{
				return this._rowset[index];
			}
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0002F78D File Offset: 0x0002D98D
		internal void AddRow(Row row)
		{
			this._rowset.Add(row);
		}

		// Token: 0x04000661 RID: 1633
		private readonly _SqlMetaDataSet _metadata;

		// Token: 0x04000662 RID: 1634
		private readonly List<Row> _rowset;
	}
}
