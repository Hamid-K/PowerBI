using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200006A RID: 106
	internal sealed class DsqSortKeyProjection : DsqSortKey
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x00011533 File Offset: 0x0000F733
		internal DsqSortKeyProjection(ProjectedDsqExpression projection, SortDirection direction, int? selectIndex)
			: base(direction, selectIndex)
		{
			this.Projection = projection;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00011544 File Offset: 0x0000F744
		internal ProjectedDsqExpression Projection { get; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0001154C File Offset: 0x0000F74C
		internal override ExpressionNode Expression
		{
			get
			{
				return this.Projection.Value.DsqExpression;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0001155E File Offset: 0x0000F75E
		internal override bool IsMeasure
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00011561 File Offset: 0x0000F761
		internal override T Accept<T, TArg>(IDsqSortKeyVisitor<T, TArg> visitor, TArg arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001156B File Offset: 0x0000F76B
		internal override bool TryRebind(ProjectedDsqExpression projection, out DsqSortKey reboundExpression)
		{
			reboundExpression = null;
			return false;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00011571 File Offset: 0x0000F771
		internal override bool MatchesExpression(ExpressionNode expression)
		{
			return this.Projection.MatchesExpression(expression);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00011580 File Offset: 0x0000F780
		internal override DsqSortKey CloneWithOverrides(ExpressionNode expression)
		{
			return new DsqSortKeyProjection(this.Projection.CloneWithOverrides(this.Projection.Value.CloneWithOverrides(expression), null), base.Direction, base.SelectIndex);
		}
	}
}
