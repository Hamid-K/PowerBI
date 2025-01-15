using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001F0 RID: 496
	[ImmutableObject(true)]
	internal sealed class ResolvedPartitionTableResult
	{
		// Token: 0x06000D76 RID: 3446 RVA: 0x0001A701 File Offset: 0x00018901
		internal ResolvedPartitionTableResult(string tableName, string partitionIdColumn, IReadOnlyList<IReadOnlyList<ResolvedPartitionTableIdentityMapping>> itemIdMappings)
		{
			this._tableName = tableName;
			this._partitionIdColumn = partitionIdColumn;
			this._itemIdMappings = itemIdMappings;
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x0001A71E File Offset: 0x0001891E
		internal string TableName
		{
			get
			{
				return this._tableName;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0001A726 File Offset: 0x00018926
		internal string PartitionIdColumn
		{
			get
			{
				return this._partitionIdColumn;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x0001A72E File Offset: 0x0001892E
		internal IReadOnlyList<IReadOnlyList<ResolvedPartitionTableIdentityMapping>> ItemIdMappings
		{
			get
			{
				return this._itemIdMappings;
			}
		}

		// Token: 0x040006E3 RID: 1763
		private readonly string _tableName;

		// Token: 0x040006E4 RID: 1764
		private readonly string _partitionIdColumn;

		// Token: 0x040006E5 RID: 1765
		private readonly IReadOnlyList<IReadOnlyList<ResolvedPartitionTableIdentityMapping>> _itemIdMappings;
	}
}
