using System;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000C7 RID: 199
	internal sealed class ResolvedSemanticQueryDataShape
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x0001BB0F File Offset: 0x00019D0F
		internal ResolvedSemanticQueryDataShape(ResolvedQueryDefinition queryDefinition, DataShapeBinding binding, int? maxRowCount, ResolvedDataReduction resolvedDataReduction)
		{
			this.Query = queryDefinition;
			this.Binding = binding;
			this.MaxRowCount = maxRowCount;
			this.ResolvedDataReduction = resolvedDataReduction;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001BB34 File Offset: 0x00019D34
		public ResolvedQueryDefinition Query { get; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001BB3C File Offset: 0x00019D3C
		public DataShapeBinding Binding { get; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001BB44 File Offset: 0x00019D44
		public int? MaxRowCount { get; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001BB4C File Offset: 0x00019D4C
		public ResolvedDataReduction ResolvedDataReduction { get; }

		// Token: 0x0600073F RID: 1855 RVA: 0x0001BB54 File Offset: 0x00019D54
		internal ResolvedSemanticQueryDataShape Clone(ResolvedQueryDefinition newQuery = null)
		{
			return new ResolvedSemanticQueryDataShape(newQuery ?? this.Query, this.Binding, this.MaxRowCount, this.ResolvedDataReduction);
		}
	}
}
