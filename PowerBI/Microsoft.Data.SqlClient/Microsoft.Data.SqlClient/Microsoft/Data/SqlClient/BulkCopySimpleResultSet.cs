using System;
using System.Collections.Generic;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000D6 RID: 214
	internal sealed class BulkCopySimpleResultSet
	{
		// Token: 0x06000EF6 RID: 3830 RVA: 0x0002F79B File Offset: 0x0002D99B
		internal BulkCopySimpleResultSet()
		{
			this._results = new List<Result>();
		}

		// Token: 0x17000805 RID: 2053
		internal Result this[int idx]
		{
			get
			{
				return this._results[idx];
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0002F7BC File Offset: 0x0002D9BC
		internal void SetMetaData(_SqlMetaDataSet metadata)
		{
			this._resultSet = new Result(metadata);
			this._results.Add(this._resultSet);
			this._indexmap = new int[this._resultSet.MetaData.Length];
			for (int i = 0; i < this._indexmap.Length; i++)
			{
				this._indexmap[i] = i;
			}
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0002F81D File Offset: 0x0002DA1D
		internal int[] CreateIndexMap()
		{
			return this._indexmap;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0002F828 File Offset: 0x0002DA28
		internal object[] CreateRowBuffer()
		{
			Row row = new Row(this._resultSet.MetaData.Length);
			this._resultSet.AddRow(row);
			return row.DataFields;
		}

		// Token: 0x04000663 RID: 1635
		private readonly List<Result> _results;

		// Token: 0x04000664 RID: 1636
		private Result _resultSet;

		// Token: 0x04000665 RID: 1637
		private int[] _indexmap;
	}
}
