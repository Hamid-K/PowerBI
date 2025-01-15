using System;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.QueryExecution
{
	// Token: 0x0200005E RID: 94
	internal sealed class DiagnosticDataRowBuffer
	{
		// Token: 0x06000245 RID: 581 RVA: 0x00006864 File Offset: 0x00004A64
		internal DiagnosticDataRowBuffer(int count)
		{
			this._buffer = new IDataRow[count];
			this._lastIndex = 0;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000687F File Offset: 0x00004A7F
		internal void PutRow(IDataRow row)
		{
			this._lastIndex = (this._lastIndex + 1) % this._buffer.Length;
			this._buffer[this._lastIndex] = row;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000068A8 File Offset: 0x00004AA8
		internal IDataRow GetRow(int n)
		{
			int num = (this._lastIndex + (this._buffer.Length - n)) % this._buffer.Length;
			return this._buffer[num];
		}

		// Token: 0x04000162 RID: 354
		private readonly IDataRow[] _buffer;

		// Token: 0x04000163 RID: 355
		private int _lastIndex;
	}
}
