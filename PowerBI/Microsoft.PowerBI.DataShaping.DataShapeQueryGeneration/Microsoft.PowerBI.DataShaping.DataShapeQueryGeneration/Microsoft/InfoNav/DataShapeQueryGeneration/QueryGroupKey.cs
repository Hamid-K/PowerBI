using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000A9 RID: 169
	internal sealed class QueryGroupKey
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x0001886B File Offset: 0x00016A6B
		internal QueryGroupKey(IConceptualColumn field, ExpressionNode expression, int? selectIndex, bool isIdentityKey, bool showItemsWithNoData)
		{
			this.Field = field;
			this.Expression = expression;
			this.SelectIndex = selectIndex;
			this.IsIdentityKey = isIdentityKey;
			this.ShowItemsWithNoData = showItemsWithNoData;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00018898 File Offset: 0x00016A98
		internal IConceptualColumn Field { get; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x000188A0 File Offset: 0x00016AA0
		internal ExpressionNode Expression { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x000188A8 File Offset: 0x00016AA8
		internal int? SelectIndex { get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x000188B0 File Offset: 0x00016AB0
		internal bool IsIdentityKey { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x000188B8 File Offset: 0x00016AB8
		internal bool ShowItemsWithNoData { get; }

		// Token: 0x0600064E RID: 1614 RVA: 0x000188C0 File Offset: 0x00016AC0
		internal QueryGroupKey Clone(ExpressionNode expression)
		{
			return new QueryGroupKey(this.Field, expression, this.SelectIndex, this.IsIdentityKey, this.ShowItemsWithNoData);
		}
	}
}
