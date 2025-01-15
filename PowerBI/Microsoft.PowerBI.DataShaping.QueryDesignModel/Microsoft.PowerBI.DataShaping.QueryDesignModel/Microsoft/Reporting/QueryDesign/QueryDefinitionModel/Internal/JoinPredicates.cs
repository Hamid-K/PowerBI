using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000E9 RID: 233
	internal static class JoinPredicates
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00023763 File Offset: 0x00021963
		public static IEqualityComparer<IJoinPredicate> Comparer
		{
			get
			{
				return JoinPredicates._comparer;
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0002376A File Offset: 0x0002196A
		public static IJoinPredicate CreateJoinPredicateForMeasureExpression(QueryExpression measureExpr)
		{
			return new ExpressionJoinPredicate(Measure.CreateJoinPredicateExpressionForMeasureExpression(measureExpr), Measure.IsMeasureExpressionAnchored(measureExpr));
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0002377D File Offset: 0x0002197D
		internal static IEnumerable<QueryExpression> ToPredicateExpressions(this IEnumerable<IJoinPredicate> joinPredicates)
		{
			return joinPredicates.Select((IJoinPredicate p) => p.ToPredicateExpression());
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x000237A4 File Offset: 0x000219A4
		internal static bool AreEquivalent(IEnumerable<QueryExpression> x, IEnumerable<QueryExpression> y)
		{
			ArgumentValidation.CheckNotNull<IEnumerable<QueryExpression>>(x, "x");
			ArgumentValidation.CheckNotNull<IEnumerable<QueryExpression>>(y, "y");
			int num = x.Count<QueryExpression>();
			int num2 = y.Count<QueryExpression>();
			return num == num2 && !x.Except(y).Any<QueryExpression>();
		}

		// Token: 0x040009AC RID: 2476
		private static IEqualityComparer<IJoinPredicate> _comparer = new JoinPredicates.JoinPredicateComparer();

		// Token: 0x02000300 RID: 768
		private sealed class JoinPredicateComparer : IEqualityComparer<IJoinPredicate>
		{
			// Token: 0x06001D50 RID: 7504 RVA: 0x00050B87 File Offset: 0x0004ED87
			public bool Equals(IJoinPredicate x, IJoinPredicate y)
			{
				return x == y || (x != null && y != null && x.ToPredicateExpression().Equals(y.ToPredicateExpression()));
			}

			// Token: 0x06001D51 RID: 7505 RVA: 0x00050BA8 File Offset: 0x0004EDA8
			public int GetHashCode(IJoinPredicate obj)
			{
				return obj.ToPredicateExpression().GetHashCode();
			}
		}
	}
}
