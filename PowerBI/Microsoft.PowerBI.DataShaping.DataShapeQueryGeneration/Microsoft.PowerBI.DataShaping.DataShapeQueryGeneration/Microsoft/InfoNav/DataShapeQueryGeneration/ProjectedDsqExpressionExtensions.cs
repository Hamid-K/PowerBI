using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000070 RID: 112
	internal static class ProjectedDsqExpressionExtensions
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x000121B5 File Offset: 0x000103B5
		internal static bool ContainsItemWithSameExpression(this ICollection<ProjectedDsqExpression> expressionGroup, ProjectedDsqExpression expression)
		{
			return expressionGroup.Find(expression.Value.DsqExpression) != null;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000121CC File Offset: 0x000103CC
		internal static ProjectedDsqExpression Find(this IEnumerable<ProjectedDsqExpression> expressionGroup, ExpressionNode expression)
		{
			foreach (ProjectedDsqExpression projectedDsqExpression in expressionGroup)
			{
				if (projectedDsqExpression.MatchesExpression(expression))
				{
					return projectedDsqExpression;
				}
			}
			return null;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00012220 File Offset: 0x00010420
		internal static QueryGroupSingleValue Find(this IEnumerable<QueryGroupSingleValue> values, ExpressionNode expression)
		{
			foreach (QueryGroupSingleValue queryGroupSingleValue in values)
			{
				if (queryGroupSingleValue.ProjectedExpression.MatchesExpression(expression))
				{
					return queryGroupSingleValue;
				}
			}
			return null;
		}
	}
}
