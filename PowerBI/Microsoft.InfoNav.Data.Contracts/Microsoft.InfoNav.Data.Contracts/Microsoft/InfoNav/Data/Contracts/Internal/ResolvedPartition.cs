using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001EC RID: 492
	[ImmutableObject(true)]
	internal sealed class ResolvedPartition
	{
		// Token: 0x06000D67 RID: 3431 RVA: 0x0001A63A File Offset: 0x0001883A
		internal ResolvedPartition(string displayName, IReadOnlyList<ResolvedQueryExpression> partitionIds)
		{
			this._displayName = displayName;
			this._partitionIds = partitionIds;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0001A650 File Offset: 0x00018850
		internal string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0001A658 File Offset: 0x00018858
		internal IReadOnlyList<ResolvedQueryExpression> PartitionIds
		{
			get
			{
				return this._partitionIds;
			}
		}

		// Token: 0x040006D8 RID: 1752
		private readonly string _displayName;

		// Token: 0x040006D9 RID: 1753
		private readonly IReadOnlyList<ResolvedQueryExpression> _partitionIds;
	}
}
