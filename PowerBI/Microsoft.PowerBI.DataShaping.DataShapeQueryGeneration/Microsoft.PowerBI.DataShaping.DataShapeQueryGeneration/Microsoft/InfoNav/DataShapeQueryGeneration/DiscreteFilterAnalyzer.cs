using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000027 RID: 39
	internal sealed class DiscreteFilterAnalyzer : DefaultResolvedQueryExpressionVisitor<DiscreteFilterState>
	{
		// Token: 0x06000187 RID: 391 RVA: 0x0000919D File Offset: 0x0000739D
		private DiscreteFilterAnalyzer()
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000091A8 File Offset: 0x000073A8
		internal static DiscreteFilterState Analyze(IReadOnlyList<ResolvedQueryFilter> filters)
		{
			if (filters == null || filters.Count == 0)
			{
				return DiscreteFilterState.Undefined;
			}
			DiscreteFilterState discreteFilterState = DiscreteFilterAnalyzer.Instance.Analyze(filters[0]);
			for (int i = 1; i < filters.Count; i++)
			{
				DiscreteFilterState discreteFilterState2 = DiscreteFilterAnalyzer.Instance.Analyze(filters[i]);
				discreteFilterState = discreteFilterState.And(discreteFilterState2);
			}
			return discreteFilterState;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00009204 File Offset: 0x00007404
		private DiscreteFilterState Analyze(ResolvedQueryFilter filter)
		{
			return filter.Condition.Accept<DiscreteFilterState>(this);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00009214 File Offset: 0x00007414
		public override DiscreteFilterState Visit(ResolvedQueryAndExpression expression)
		{
			DiscreteFilterState discreteFilterState = expression.Left.Accept<DiscreteFilterState>(this);
			DiscreteFilterState discreteFilterState2 = expression.Right.Accept<DiscreteFilterState>(this);
			return discreteFilterState.And(discreteFilterState2);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00009240 File Offset: 0x00007440
		public override DiscreteFilterState Visit(ResolvedQueryOrExpression expression)
		{
			DiscreteFilterState discreteFilterState = expression.Left.Accept<DiscreteFilterState>(this);
			DiscreteFilterState discreteFilterState2 = expression.Right.Accept<DiscreteFilterState>(this);
			return discreteFilterState.Or(discreteFilterState2);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000926C File Offset: 0x0000746C
		public override DiscreteFilterState Visit(ResolvedQueryComparisonExpression expression)
		{
			if (expression.ComparisonKind == QueryComparisonKind.Equal)
			{
				IConceptualColumn conceptualColumn = DiscreteFilterAnalyzer.ExtractColumn(expression.Left);
				ResolvedQueryExpression right = expression.Right;
				if (conceptualColumn != null && DiscreteFilterAnalyzer.IsAllowedValue(right))
				{
					DiscreteFilterState discreteFilterState = new DiscreteFilterState();
					discreteFilterState.AddValue(conceptualColumn, right);
					return discreteFilterState;
				}
			}
			return DiscreteFilterState.Undefined;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000092B4 File Offset: 0x000074B4
		public override DiscreteFilterState Visit(ResolvedQueryInExpression expression)
		{
			if (!expression.HasValues)
			{
				return DiscreteFilterState.Undefined;
			}
			DiscreteFilterState discreteFilterState = new DiscreteFilterState();
			IReadOnlyList<ResolvedQueryExpression> expressions = expression.Expressions;
			IReadOnlyList<IReadOnlyList<ResolvedQueryExpression>> values = expression.Values;
			for (int i = 0; i < expressions.Count; i++)
			{
				IConceptualColumn conceptualColumn = DiscreteFilterAnalyzer.ExtractColumn(expressions[i]);
				if (conceptualColumn == null)
				{
					return DiscreteFilterState.Undefined;
				}
				for (int j = 0; j < values.Count; j++)
				{
					ResolvedQueryExpression resolvedQueryExpression = values[j][i];
					if (!DiscreteFilterAnalyzer.IsAllowedValue(resolvedQueryExpression))
					{
						return DiscreteFilterState.Undefined;
					}
					discreteFilterState.AddValue(conceptualColumn, resolvedQueryExpression);
				}
			}
			return discreteFilterState;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000934C File Offset: 0x0000754C
		private static IConceptualColumn ExtractColumn(ResolvedQueryExpression expression)
		{
			ResolvedQueryPropertyExpression resolvedQueryPropertyExpression = expression as ResolvedQueryPropertyExpression;
			if (resolvedQueryPropertyExpression != null)
			{
				return resolvedQueryPropertyExpression.Property as IConceptualColumn;
			}
			return null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00009376 File Offset: 0x00007576
		private static bool IsAllowedValue(ResolvedQueryExpression expression)
		{
			return expression is ResolvedQueryLiteralExpression || expression is ResolvedQueryDefaultValueExpression;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000938B File Offset: 0x0000758B
		protected override DiscreteFilterState VisitUnhandledExpression(ResolvedQueryExpression expression)
		{
			return DiscreteFilterState.Undefined;
		}

		// Token: 0x040000C5 RID: 197
		internal static readonly DiscreteFilterAnalyzer Instance = new DiscreteFilterAnalyzer();
	}
}
