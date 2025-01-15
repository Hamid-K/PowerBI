using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Annotations
{
	// Token: 0x02000112 RID: 274
	internal sealed class IntermediateTableFilterUsageAnnotationAnalyzer : DefaultResolvedQueryExpressionVisitor
	{
		// Token: 0x06000901 RID: 2305 RVA: 0x000248F2 File Offset: 0x00022AF2
		private IntermediateTableFilterUsageAnnotationAnalyzer(HashSet<string> referencedExpressionSources, HashSet<ResolvedQueryLetBinding> referencedLetBindings)
		{
			this._referencedExpressionSources = referencedExpressionSources;
			this._referencedLetBindings = referencedLetBindings;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00024908 File Offset: 0x00022B08
		internal static void Analyze(IReadOnlyCollection<ResolvedQueryFilter> filters, HashSet<string> referencedExpressionSources, HashSet<ResolvedQueryLetBinding> referencedLetBindings)
		{
			if (filters.IsNullOrEmpty<ResolvedQueryFilter>())
			{
				return;
			}
			IntermediateTableFilterUsageAnnotationAnalyzer intermediateTableFilterUsageAnnotationAnalyzer = new IntermediateTableFilterUsageAnnotationAnalyzer(referencedExpressionSources, referencedLetBindings);
			foreach (ResolvedQueryFilter resolvedQueryFilter in filters)
			{
				resolvedQueryFilter.Condition.Accept(intermediateTableFilterUsageAnnotationAnalyzer);
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00024964 File Offset: 0x00022B64
		public override void Visit(ResolvedQueryInExpression expression)
		{
			base.Visit(expression);
			ResolvedQueryExpressionSourceRefExpression resolvedQueryExpressionSourceRefExpression = expression.Table as ResolvedQueryExpressionSourceRefExpression;
			if (resolvedQueryExpressionSourceRefExpression != null)
			{
				this._referencedExpressionSources.Add(resolvedQueryExpressionSourceRefExpression.SourceName);
				ResolvedQueryLetRefExpression resolvedQueryLetRefExpression = resolvedQueryExpressionSourceRefExpression.Expression as ResolvedQueryLetRefExpression;
				if (resolvedQueryLetRefExpression != null)
				{
					this._referencedLetBindings.Add(resolvedQueryLetRefExpression.Binding);
				}
			}
		}

		// Token: 0x04000493 RID: 1171
		private readonly HashSet<ResolvedQueryLetBinding> _referencedLetBindings;

		// Token: 0x04000494 RID: 1172
		private readonly HashSet<string> _referencedExpressionSources;
	}
}
