using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation.Clustering
{
	// Token: 0x02000029 RID: 41
	internal sealed class ClusteringTranslationRequest
	{
		// Token: 0x06000133 RID: 307 RVA: 0x0000705C File Offset: 0x0000525C
		internal ClusteringTranslationRequest(IReadOnlyList<ResolvedQueryExpression> groupedColumns, ResolvedPartitionTable partitionTable)
		{
			this.GroupedColumns = groupedColumns;
			this.PartitionTable = partitionTable;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00007072 File Offset: 0x00005272
		// (set) Token: 0x06000135 RID: 309 RVA: 0x0000707A File Offset: 0x0000527A
		public IReadOnlyList<ResolvedQueryExpression> GroupedColumns { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00007083 File Offset: 0x00005283
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000708B File Offset: 0x0000528B
		public ResolvedPartitionTable PartitionTable { get; private set; }
	}
}
