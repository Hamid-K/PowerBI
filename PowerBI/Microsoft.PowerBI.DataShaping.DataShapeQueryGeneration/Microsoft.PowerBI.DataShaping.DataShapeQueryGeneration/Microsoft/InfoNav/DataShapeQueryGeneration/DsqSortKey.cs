using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000066 RID: 102
	internal abstract class DsqSortKey
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x0001132E File Offset: 0x0000F52E
		protected DsqSortKey(SortDirection direction, int? selectIndex)
		{
			this.Direction = direction;
			this.SelectIndex = selectIndex;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000487 RID: 1159
		internal abstract ExpressionNode Expression { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000488 RID: 1160
		internal abstract bool IsMeasure { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00011344 File Offset: 0x0000F544
		internal int? SelectIndex { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0001134C File Offset: 0x0000F54C
		internal SortDirection Direction { get; }

		// Token: 0x0600048B RID: 1163
		internal abstract T Accept<T, TArg>(IDsqSortKeyVisitor<T, TArg> visitor, TArg arg);

		// Token: 0x0600048C RID: 1164
		internal abstract bool TryRebind(ProjectedDsqExpression projection, out DsqSortKey reboundExpression);

		// Token: 0x0600048D RID: 1165
		internal abstract bool MatchesExpression(ExpressionNode expression);

		// Token: 0x0600048E RID: 1166
		internal abstract DsqSortKey CloneWithOverrides(ExpressionNode expression);
	}
}
