using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000073 RID: 115
	internal sealed class TargetComparer : IEqualityComparer<ResolvedQueryExpression>
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x000122F0 File Offset: 0x000104F0
		internal TargetComparer(DsqExpressionGenerator expressionGenerator)
		{
			this._expressionGenerator = expressionGenerator;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00012300 File Offset: 0x00010500
		public bool Equals(ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			if (left == null && right == null)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			left = this.NormalizeExpression(left);
			right = this.NormalizeExpression(right);
			return left.Equals(right);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001234E File Offset: 0x0001054E
		public int GetHashCode(ResolvedQueryExpression obj)
		{
			if (obj == null)
			{
				return 0;
			}
			obj = this.NormalizeExpression(obj);
			return obj.GetHashCode();
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001236C File Offset: 0x0001056C
		private ResolvedQueryExpression NormalizeExpression(ResolvedQueryExpression expr)
		{
			IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn;
			if (!this._expressionGenerator.TryGetAsTransformColumn(expr, out intermediateQueryTransformTableColumn))
			{
				return expr;
			}
			return intermediateQueryTransformTableColumn.UnderlyingExpression;
		}

		// Token: 0x040002AB RID: 683
		private readonly DsqExpressionGenerator _expressionGenerator;
	}
}
