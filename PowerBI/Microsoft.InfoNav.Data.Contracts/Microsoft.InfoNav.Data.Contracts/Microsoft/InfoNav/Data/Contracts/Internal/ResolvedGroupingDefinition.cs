using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001E9 RID: 489
	[ImmutableObject(true)]
	internal sealed class ResolvedGroupingDefinition
	{
		// Token: 0x06000D5C RID: 3420 RVA: 0x0001A599 File Offset: 0x00018799
		internal ResolvedGroupingDefinition(IReadOnlyList<ResolvedQuerySource> sources, IReadOnlyList<ResolvedQueryExpression> groupedColumns, IReadOnlyList<ResolvedGroupItem> groupItems, ResolvedBinItem binItem, ResolvedPartitionTable partitionTable)
		{
			this._sources = sources;
			this._groupedColumns = groupedColumns;
			this._groupItems = groupItems;
			this._binItem = binItem;
			this._partitionTable = partitionTable;
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0001A5C6 File Offset: 0x000187C6
		internal IReadOnlyList<ResolvedQuerySource> Sources
		{
			get
			{
				return this._sources;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0001A5CE File Offset: 0x000187CE
		internal IReadOnlyList<ResolvedQueryExpression> GroupedColumns
		{
			get
			{
				return this._groupedColumns;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0001A5D6 File Offset: 0x000187D6
		internal IReadOnlyList<ResolvedGroupItem> GroupItems
		{
			get
			{
				return this._groupItems;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0001A5DE File Offset: 0x000187DE
		internal ResolvedBinItem BinItem
		{
			get
			{
				return this._binItem;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x0001A5E6 File Offset: 0x000187E6
		internal ResolvedPartitionTable PartitionTable
		{
			get
			{
				return this._partitionTable;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0001A5EE File Offset: 0x000187EE
		internal GroupingDefinitionKind DefinitionKind
		{
			get
			{
				if (this.GroupItems != null)
				{
					return GroupingDefinitionKind.GroupItems;
				}
				if (this.BinItem == null)
				{
					return GroupingDefinitionKind.PartitionTable;
				}
				return GroupingDefinitionKind.BinItem;
			}
		}

		// Token: 0x040006CC RID: 1740
		private readonly IReadOnlyList<ResolvedQuerySource> _sources;

		// Token: 0x040006CD RID: 1741
		private readonly IReadOnlyList<ResolvedQueryExpression> _groupedColumns;

		// Token: 0x040006CE RID: 1742
		private readonly IReadOnlyList<ResolvedGroupItem> _groupItems;

		// Token: 0x040006CF RID: 1743
		private readonly ResolvedBinItem _binItem;

		// Token: 0x040006D0 RID: 1744
		private readonly ResolvedPartitionTable _partitionTable;
	}
}
