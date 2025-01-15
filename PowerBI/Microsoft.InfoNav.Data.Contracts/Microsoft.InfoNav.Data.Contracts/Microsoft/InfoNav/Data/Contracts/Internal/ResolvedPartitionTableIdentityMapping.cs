using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001EF RID: 495
	[ImmutableObject(true)]
	internal sealed class ResolvedPartitionTableIdentityMapping
	{
		// Token: 0x06000D73 RID: 3443 RVA: 0x0001A6DB File Offset: 0x000188DB
		internal ResolvedPartitionTableIdentityMapping(ResolvedQueryExpression partitionTableColumn, ResolvedQueryExpression sourceTableColumn)
		{
			this._partitionTableColumn = partitionTableColumn;
			this._sourceTableColumn = sourceTableColumn;
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0001A6F1 File Offset: 0x000188F1
		internal ResolvedQueryExpression PartitionTableColumn
		{
			get
			{
				return this._partitionTableColumn;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0001A6F9 File Offset: 0x000188F9
		public ResolvedQueryExpression SourceTableColumn
		{
			get
			{
				return this._sourceTableColumn;
			}
		}

		// Token: 0x040006E1 RID: 1761
		private readonly ResolvedQueryExpression _partitionTableColumn;

		// Token: 0x040006E2 RID: 1762
		private readonly ResolvedQueryExpression _sourceTableColumn;
	}
}
