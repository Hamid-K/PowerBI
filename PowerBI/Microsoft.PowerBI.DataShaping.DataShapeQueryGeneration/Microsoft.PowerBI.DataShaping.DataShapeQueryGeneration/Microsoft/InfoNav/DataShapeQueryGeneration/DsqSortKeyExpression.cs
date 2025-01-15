using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000069 RID: 105
	internal sealed class DsqSortKeyExpression : DsqSortKey
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x00011498 File Offset: 0x0000F698
		internal DsqSortKeyExpression(ExpressionNode expression, SortDirection direction, bool isMeasure, int? selectIndex, bool isScoped)
			: base(direction, selectIndex)
		{
			this.Expression = expression;
			this.IsMeasure = isMeasure;
			this.IsScoped = isScoped;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x000114B9 File Offset: 0x0000F6B9
		internal override ExpressionNode Expression { get; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x000114C1 File Offset: 0x0000F6C1
		internal override bool IsMeasure { get; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x000114C9 File Offset: 0x0000F6C9
		internal bool IsScoped { get; }

		// Token: 0x0600049B RID: 1179 RVA: 0x000114D1 File Offset: 0x0000F6D1
		internal override T Accept<T, TArg>(IDsqSortKeyVisitor<T, TArg> visitor, TArg arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000114DB File Offset: 0x0000F6DB
		internal override bool TryRebind(ProjectedDsqExpression projection, out DsqSortKey reboundExpression)
		{
			if (projection.MatchesExpression(this.Expression))
			{
				reboundExpression = new DsqSortKeyProjection(projection, base.Direction, base.SelectIndex);
				return true;
			}
			reboundExpression = null;
			return false;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00011505 File Offset: 0x0000F705
		internal override bool MatchesExpression(ExpressionNode expression)
		{
			return this.Expression.Equals(expression);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00011513 File Offset: 0x0000F713
		internal override DsqSortKey CloneWithOverrides(ExpressionNode expression)
		{
			return new DsqSortKeyExpression(expression, base.Direction, this.IsMeasure, base.SelectIndex, this.IsScoped);
		}
	}
}
