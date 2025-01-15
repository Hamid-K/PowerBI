using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000D3 RID: 211
	internal sealed class _ColumnMapping
	{
		// Token: 0x06000EED RID: 3821 RVA: 0x0002F714 File Offset: 0x0002D914
		internal _ColumnMapping(int columnId, _SqlMetaData metadata)
		{
			this._sourceColumnOrdinal = columnId;
			this._metadata = metadata;
		}

		// Token: 0x0400065E RID: 1630
		internal readonly int _sourceColumnOrdinal;

		// Token: 0x0400065F RID: 1631
		internal readonly _SqlMetaData _metadata;
	}
}
