using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.QueryExecution
{
	// Token: 0x02000069 RID: 105
	internal sealed class ResultSetSchema
	{
		// Token: 0x0600026E RID: 622 RVA: 0x00007274 File Offset: 0x00005474
		internal ResultSetSchema(IReadOnlyList<Type> columnTypes)
		{
			this._columnTypes = columnTypes;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00007283 File Offset: 0x00005483
		internal Type GetColumnType(int index)
		{
			return this._columnTypes[index];
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00007291 File Offset: 0x00005491
		internal IReadOnlyList<Type> GetColumnTypes()
		{
			return this._columnTypes;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00007299 File Offset: 0x00005499
		internal int Count
		{
			get
			{
				return this._columnTypes.Count;
			}
		}

		// Token: 0x04000180 RID: 384
		private readonly IReadOnlyList<Type> _columnTypes;
	}
}
