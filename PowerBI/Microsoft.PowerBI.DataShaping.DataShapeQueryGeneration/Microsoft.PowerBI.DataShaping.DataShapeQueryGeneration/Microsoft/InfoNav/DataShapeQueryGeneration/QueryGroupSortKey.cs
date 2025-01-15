using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000AB RID: 171
	internal sealed class QueryGroupSortKey
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x0001895C File Offset: 0x00016B5C
		internal QueryGroupSortKey(ExpressionNode expression, int? selectIndex)
		{
			this.Expression = expression;
			this.SelectIndex = selectIndex;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00018972 File Offset: 0x00016B72
		internal ExpressionNode Expression { get; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001897A File Offset: 0x00016B7A
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x00018982 File Offset: 0x00016B82
		internal int? SelectIndex { get; private set; }

		// Token: 0x0600065C RID: 1628 RVA: 0x0001898C File Offset: 0x00016B8C
		internal void UpdateSelectIndex(int? newSelectIndex)
		{
			if (newSelectIndex == null)
			{
				return;
			}
			if (this.SelectIndex != null)
			{
				return;
			}
			this.SelectIndex = newSelectIndex;
		}
	}
}
