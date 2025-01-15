using System;
using System.Collections.Generic;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x020000A1 RID: 161
	internal sealed class ReadOnlyRowCache : IReadOnlyRowCache
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x0000D567 File Offset: 0x0000B767
		internal ReadOnlyRowCache(IList<IDataRow> rows)
		{
			this._rows = rows;
		}

		// Token: 0x17000173 RID: 371
		public IDataRow this[int index]
		{
			get
			{
				return this._rows[index];
			}
		}

		// Token: 0x0400022F RID: 559
		private readonly IList<IDataRow> _rows;
	}
}
