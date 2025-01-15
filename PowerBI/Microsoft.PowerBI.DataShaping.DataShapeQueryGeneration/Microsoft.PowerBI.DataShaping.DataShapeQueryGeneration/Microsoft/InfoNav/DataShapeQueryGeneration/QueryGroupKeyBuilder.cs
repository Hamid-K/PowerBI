using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000AA RID: 170
	internal sealed class QueryGroupKeyBuilder
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x000188E0 File Offset: 0x00016AE0
		internal QueryGroupKeyBuilder(ExpressionNode expression, IConceptualColumn underlyingColumn, int? selectIndex)
		{
			this.Expression = expression;
			this.Column = underlyingColumn;
			this.SelectIndex = selectIndex;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x000188FD File Offset: 0x00016AFD
		internal ExpressionNode Expression { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00018905 File Offset: 0x00016B05
		internal IConceptualColumn Column { get; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001890D File Offset: 0x00016B0D
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x00018915 File Offset: 0x00016B15
		internal bool ShowItemsWithNoData { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001891E File Offset: 0x00016B1E
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x00018926 File Offset: 0x00016B26
		internal bool IsIdentityKey { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001892F File Offset: 0x00016B2F
		internal int? SelectIndex { get; }

		// Token: 0x06000657 RID: 1623 RVA: 0x00018937 File Offset: 0x00016B37
		internal QueryGroupKey ToKey()
		{
			return new QueryGroupKey(this.Column, this.Expression, this.SelectIndex, this.IsIdentityKey, this.ShowItemsWithNoData);
		}
	}
}
