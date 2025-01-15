using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.QueryExecution;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Analytics
{
	// Token: 0x020000B7 RID: 183
	internal sealed class ResultSetEnumerator : IEnumerator<IDataRow>, IDisposable, IEnumerator
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x0000E157 File Offset: 0x0000C357
		internal ResultSetEnumerator(IResultSet input)
		{
			this._input = input;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000E166 File Offset: 0x0000C366
		public bool MoveNext()
		{
			if (this._wasExhausted)
			{
				return false;
			}
			this._currentRow = this._input.ReadRow();
			if (this._currentRow == null)
			{
				this._wasExhausted = true;
			}
			return this._currentRow != null;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000E19B File Offset: 0x0000C39B
		public void Reset()
		{
			throw new NotSupportedException("Reseting the enumerator is not supported");
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000E1A7 File Offset: 0x0000C3A7
		void IDisposable.Dispose()
		{
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000E1A9 File Offset: 0x0000C3A9
		public IDataRow Current
		{
			get
			{
				return this._currentRow;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000E1B1 File Offset: 0x0000C3B1
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x04000262 RID: 610
		private readonly IResultSet _input;

		// Token: 0x04000263 RID: 611
		private IDataRow _currentRow;

		// Token: 0x04000264 RID: 612
		private bool _wasExhausted;
	}
}
