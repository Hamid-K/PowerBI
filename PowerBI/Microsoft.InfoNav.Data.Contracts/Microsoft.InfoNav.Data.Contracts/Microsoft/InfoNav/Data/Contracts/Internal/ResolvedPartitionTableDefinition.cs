using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001EE RID: 494
	[ImmutableObject(true)]
	internal sealed class ResolvedPartitionTableDefinition
	{
		// Token: 0x06000D6D RID: 3437 RVA: 0x0001A686 File Offset: 0x00018886
		internal ResolvedPartitionTableDefinition(ResolvedQueryDefinition tableDefinition, IReadOnlyList<string> itemIdColumns, string partitionIdColumn, IReadOnlyList<ResolvedPartition> partitions, string defaultPartitionPrefix)
		{
			this._tableDefinition = tableDefinition;
			this._itemIdColumns = itemIdColumns;
			this._partitionIdColumn = partitionIdColumn;
			this._partitions = partitions;
			this._defaultPartitionPrefix = defaultPartitionPrefix;
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0001A6B3 File Offset: 0x000188B3
		internal ResolvedQueryDefinition TableDefinition
		{
			get
			{
				return this._tableDefinition;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0001A6BB File Offset: 0x000188BB
		internal IReadOnlyList<string> ItemIdColumns
		{
			get
			{
				return this._itemIdColumns;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0001A6C3 File Offset: 0x000188C3
		internal string PartitionIdColumn
		{
			get
			{
				return this._partitionIdColumn;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0001A6CB File Offset: 0x000188CB
		internal IReadOnlyList<ResolvedPartition> Partitions
		{
			get
			{
				return this._partitions;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0001A6D3 File Offset: 0x000188D3
		public string DefaultPartitionPrefix
		{
			get
			{
				return this._defaultPartitionPrefix;
			}
		}

		// Token: 0x040006DC RID: 1756
		private readonly ResolvedQueryDefinition _tableDefinition;

		// Token: 0x040006DD RID: 1757
		private readonly IReadOnlyList<string> _itemIdColumns;

		// Token: 0x040006DE RID: 1758
		private readonly string _partitionIdColumn;

		// Token: 0x040006DF RID: 1759
		private readonly IReadOnlyList<ResolvedPartition> _partitions;

		// Token: 0x040006E0 RID: 1760
		private readonly string _defaultPartitionPrefix;
	}
}
