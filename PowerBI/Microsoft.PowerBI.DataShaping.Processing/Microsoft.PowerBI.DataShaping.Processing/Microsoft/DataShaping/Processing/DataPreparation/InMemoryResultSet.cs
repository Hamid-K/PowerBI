using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.Pipeline;
using Microsoft.DataShaping.Processing.QueryExecution;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.DataPreparation
{
	// Token: 0x02000086 RID: 134
	internal sealed class InMemoryResultSet : IResultSet
	{
		// Token: 0x06000362 RID: 866 RVA: 0x0000B438 File Offset: 0x00009638
		internal InMemoryResultSet(IResultSet source)
		{
			this._source = source;
			this._schema = this._source.Schema;
			this._rows = new List<IDataRow>();
			this._currentRowIndex = 0;
			this._isInCachingMode = true;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000B474 File Offset: 0x00009674
		public IDataRow ReadRow()
		{
			if (this._isInCachingMode)
			{
				IDataRow dataRow = this._source.ReadRow();
				if (dataRow != null)
				{
					this._rows.Add(dataRow);
				}
				return dataRow;
			}
			if (this._currentRowIndex >= this._rows.Count)
			{
				return null;
			}
			IList<IDataRow> rows = this._rows;
			int currentRowIndex = this._currentRowIndex;
			this._currentRowIndex = currentRowIndex + 1;
			return rows[currentRowIndex];
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000B4D7 File Offset: 0x000096D7
		internal void StopCachingAndResetRowIndex()
		{
			this._currentRowIndex = 0;
			if (this._isInCachingMode && this._source != null)
			{
				this._isInCachingMode = false;
				this._source = null;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000B4FE File Offset: 0x000096FE
		internal bool IsInCachingMode
		{
			get
			{
				return this._isInCachingMode;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000B506 File Offset: 0x00009706
		public long RowCount
		{
			get
			{
				return (long)this._rows.Count;
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000B514 File Offset: 0x00009714
		internal IReadOnlyRowCache ToReadOnlyRowCache()
		{
			return new ReadOnlyRowCache(this._rows);
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000B521 File Offset: 0x00009721
		public ResultSetSchema Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x040001E4 RID: 484
		private readonly IList<IDataRow> _rows;

		// Token: 0x040001E5 RID: 485
		private readonly ResultSetSchema _schema;

		// Token: 0x040001E6 RID: 486
		private IResultSet _source;

		// Token: 0x040001E7 RID: 487
		private int _currentRowIndex;

		// Token: 0x040001E8 RID: 488
		private bool _isInCachingMode;
	}
}
