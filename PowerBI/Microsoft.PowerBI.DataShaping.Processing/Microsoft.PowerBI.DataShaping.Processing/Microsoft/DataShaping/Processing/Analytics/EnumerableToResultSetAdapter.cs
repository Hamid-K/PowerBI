using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.QueryExecution;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Analytics
{
	// Token: 0x020000B2 RID: 178
	internal class EnumerableToResultSetAdapter : IResultSet, IDisposable
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x0000DED3 File Offset: 0x0000C0D3
		internal EnumerableToResultSetAdapter(IEnumerable<IDataRow> input, ResultSetSchema schema)
		{
			this._inputEnumerator = input.GetEnumerator();
			this._schema = schema;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000DEEE File Offset: 0x0000C0EE
		public virtual IDataRow ReadRow()
		{
			if (!this._inputEnumerator.MoveNext())
			{
				return null;
			}
			this._rowCount += 1L;
			return this._inputEnumerator.Current;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000DF19 File Offset: 0x0000C119
		public long RowCount
		{
			get
			{
				return this._rowCount;
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000DF21 File Offset: 0x0000C121
		private bool IsClosed()
		{
			return this._inputEnumerator == null;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000DF2C File Offset: 0x0000C12C
		internal void Close()
		{
			if (this.IsClosed())
			{
				return;
			}
			this._inputEnumerator.Dispose();
			this._inputEnumerator = null;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000DF49 File Offset: 0x0000C149
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000DF51 File Offset: 0x0000C151
		public ResultSetSchema Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x04000256 RID: 598
		private readonly ResultSetSchema _schema;

		// Token: 0x04000257 RID: 599
		private IEnumerator<IDataRow> _inputEnumerator;

		// Token: 0x04000258 RID: 600
		private long _rowCount;
	}
}
