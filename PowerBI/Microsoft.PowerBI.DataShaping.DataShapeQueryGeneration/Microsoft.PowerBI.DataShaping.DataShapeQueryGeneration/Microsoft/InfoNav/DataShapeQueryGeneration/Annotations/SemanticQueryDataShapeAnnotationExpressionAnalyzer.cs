using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Annotations
{
	// Token: 0x02000115 RID: 277
	internal sealed class SemanticQueryDataShapeAnnotationExpressionAnalyzer : DefaultResolvedQueryExpressionVisitor
	{
		// Token: 0x0600090F RID: 2319 RVA: 0x00024E41 File Offset: 0x00023041
		private SemanticQueryDataShapeAnnotationExpressionAnalyzer(HashSet<string> referencedExpressionSources, HashSet<ResolvedQueryLetBinding> referencedLetBindings)
		{
			this._referencedExpressionSources = referencedExpressionSources;
			this._referencedLetBindings = referencedLetBindings;
			this._visualCalculationExpressionCount = 0;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00024E60 File Offset: 0x00023060
		internal static void Analyze(IReadOnlyList<ResolvedQuerySelect> selects, HashSet<string> referencedExpressionSources, HashSet<ResolvedQueryLetBinding> referencedLetBindings, out int visualCalculationExpressionCount)
		{
			visualCalculationExpressionCount = 0;
			if (selects.IsNullOrEmpty<ResolvedQuerySelect>())
			{
				return;
			}
			SemanticQueryDataShapeAnnotationExpressionAnalyzer semanticQueryDataShapeAnnotationExpressionAnalyzer = new SemanticQueryDataShapeAnnotationExpressionAnalyzer(referencedExpressionSources, referencedLetBindings);
			foreach (ResolvedQuerySelect resolvedQuerySelect in selects)
			{
				resolvedQuerySelect.Expression.Accept(semanticQueryDataShapeAnnotationExpressionAnalyzer);
			}
			visualCalculationExpressionCount = semanticQueryDataShapeAnnotationExpressionAnalyzer._visualCalculationExpressionCount;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00024EC8 File Offset: 0x000230C8
		internal static void Analyze(ResolvedQueryExpression expression, HashSet<string> referencedExpressionSources, HashSet<ResolvedQueryLetBinding> referencedLetBindings)
		{
			SemanticQueryDataShapeAnnotationExpressionAnalyzer semanticQueryDataShapeAnnotationExpressionAnalyzer = new SemanticQueryDataShapeAnnotationExpressionAnalyzer(referencedExpressionSources, referencedLetBindings);
			expression.Accept(semanticQueryDataShapeAnnotationExpressionAnalyzer);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00024EE4 File Offset: 0x000230E4
		public override void Visit(ResolvedQueryExpressionSourceRefExpression expression)
		{
			this._referencedExpressionSources.Add(expression.SourceName);
			ResolvedQueryLetRefExpression resolvedQueryLetRefExpression = expression.Expression as ResolvedQueryLetRefExpression;
			if (resolvedQueryLetRefExpression != null)
			{
				this._referencedLetBindings.Add(resolvedQueryLetRefExpression.Binding);
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00024F24 File Offset: 0x00023124
		public override void Visit(ResolvedQueryNativeVisualCalculationExpression expression)
		{
			this._visualCalculationExpressionCount++;
			base.Visit(expression);
		}

		// Token: 0x040004A1 RID: 1185
		private readonly HashSet<string> _referencedExpressionSources;

		// Token: 0x040004A2 RID: 1186
		private readonly HashSet<ResolvedQueryLetBinding> _referencedLetBindings;

		// Token: 0x040004A3 RID: 1187
		private int _visualCalculationExpressionCount;
	}
}
