using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.QueryExecution;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Analytics
{
	// Token: 0x020000B6 RID: 182
	internal sealed class ResultSetEnumerable : IEnumerable<IDataRow>, IEnumerable
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x0000E111 File Offset: 0x0000C311
		internal ResultSetEnumerable(IResultSet input)
		{
			this._input = input;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000E120 File Offset: 0x0000C320
		public IEnumerator<IDataRow> GetEnumerator()
		{
			return this.CreateEnumerator();
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000E128 File Offset: 0x0000C328
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.CreateEnumerator();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000E130 File Offset: 0x0000C330
		private IEnumerator<IDataRow> CreateEnumerator()
		{
			Contract.RetailAssert(!this._enumeratorWasCalled, "Cannot call the enumerator more than once.");
			this._enumeratorWasCalled = true;
			return new ResultSetEnumerator(this._input);
		}

		// Token: 0x04000260 RID: 608
		private readonly IResultSet _input;

		// Token: 0x04000261 RID: 609
		private bool _enumeratorWasCalled;
	}
}
