using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Annotations
{
	// Token: 0x02000114 RID: 276
	internal sealed class SemanticQueryDataShapeAnnotationAnalyzer : ResolvedSemanticQueryDataShapeCommandVisitor
	{
		// Token: 0x06000904 RID: 2308 RVA: 0x000249BC File Offset: 0x00022BBC
		private SemanticQueryDataShapeAnnotationAnalyzer()
		{
			this._queryDefinitionByName = new Dictionary<string, ResolvedQueryDefinition>(QueryNameComparer.Instance);
			this._letBindingUsageByQueryName = new Dictionary<string, IReadOnlyDictionary<string, IntermediateTableUsage>>(QueryNameComparer.Instance);
			this._expressionSourceUsageByQueryName = new Dictionary<string, IReadOnlyDictionary<string, IntermediateTableUsage>>(QueryNameComparer.Instance);
			this._visualCalculationExpressionCountByQueryName = new Dictionary<string, int>(QueryNameComparer.Instance);
			this._letBindingsReferencedInSelects = new HashSet<ResolvedQueryLetBinding>(ReferenceEqualityComparer<ResolvedQueryLetBinding>.Instance);
			this._letBindingsReferencedInFilters = new HashSet<ResolvedQueryLetBinding>(ReferenceEqualityComparer<ResolvedQueryLetBinding>.Instance);
			this._expressionSourcesReferencedInSelects = new HashSet<string>(QueryNameComparer.Instance);
			this._expressionSourcesReferencedInFilters = new HashSet<string>(QueryNameComparer.Instance);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00024A50 File Offset: 0x00022C50
		internal static bool TryCreateAnnotations(ResolvedSemanticQueryDataShapeCommand command, out SemanticQueryDataShapeAnnotations annotations)
		{
			SemanticQueryDataShapeAnnotationAnalyzer semanticQueryDataShapeAnnotationAnalyzer = new SemanticQueryDataShapeAnnotationAnalyzer();
			semanticQueryDataShapeAnnotationAnalyzer.Visit(command);
			annotations = new SemanticQueryDataShapeAnnotations(semanticQueryDataShapeAnnotationAnalyzer._queryDefinitionByName, semanticQueryDataShapeAnnotationAnalyzer._expressionSourceUsageByQueryName, semanticQueryDataShapeAnnotationAnalyzer._letBindingUsageByQueryName, semanticQueryDataShapeAnnotationAnalyzer._visualCalculationExpressionCountByQueryName);
			return true;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00024A8C File Offset: 0x00022C8C
		internal static bool TryCreateAnnotations(ResolvedQueryDefinition resolvedQuery, out SemanticQueryDataShapeAnnotations annotations)
		{
			SemanticQueryDataShapeAnnotationAnalyzer semanticQueryDataShapeAnnotationAnalyzer = new SemanticQueryDataShapeAnnotationAnalyzer();
			semanticQueryDataShapeAnnotationAnalyzer.Visit(resolvedQuery);
			annotations = new SemanticQueryDataShapeAnnotations(semanticQueryDataShapeAnnotationAnalyzer._queryDefinitionByName, semanticQueryDataShapeAnnotationAnalyzer._expressionSourceUsageByQueryName, semanticQueryDataShapeAnnotationAnalyzer._letBindingUsageByQueryName, semanticQueryDataShapeAnnotationAnalyzer._visualCalculationExpressionCountByQueryName);
			return true;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00024AC8 File Offset: 0x00022CC8
		protected override void Visit(ResolvedQueryDefinition query)
		{
			this._queryDefinitionByName.Add(query.Name, query);
			this.VisitLetClause(query.Let);
			this.VisitFromClause(query.From);
			this.VisitWhereClause(query);
			this.VisitSelectClause(query);
			this.VisitTransforms(query.Transform);
			this.UpdateExpressionSourceUsage(query);
			this.UpdateLetBindingUsage(query);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00024B28 File Offset: 0x00022D28
		private void VisitTransforms(IReadOnlyList<ResolvedQueryTransform> transforms)
		{
			if (transforms.IsNullOrEmpty<ResolvedQueryTransform>())
			{
				return;
			}
			foreach (ResolvedQueryTransform resolvedQueryTransform in transforms)
			{
				foreach (ResolvedQueryTransformTableColumn resolvedQueryTransformTableColumn in resolvedQueryTransform.Input.Table.Columns)
				{
					SemanticQueryDataShapeAnnotationExpressionAnalyzer.Analyze(resolvedQueryTransformTableColumn.Expression, this._expressionSourcesReferencedInSelects, this._letBindingsReferencedInSelects);
				}
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00024BC4 File Offset: 0x00022DC4
		private void UpdateExpressionSourceUsage(ResolvedQueryDefinition query)
		{
			Dictionary<string, IntermediateTableUsage> dictionary = null;
			foreach (ResolvedQuerySource resolvedQuerySource in query.From)
			{
				IntermediateTableUsage intermediateTableUsage = IntermediateTableUsage.None;
				if (this._expressionSourcesReferencedInSelects.Contains(resolvedQuerySource.Name))
				{
					intermediateTableUsage |= IntermediateTableUsage.Regrouping;
				}
				if (this._expressionSourcesReferencedInFilters.Contains(resolvedQuerySource.Name))
				{
					intermediateTableUsage |= IntermediateTableUsage.Filtering;
				}
				if (intermediateTableUsage != IntermediateTableUsage.None)
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<string, IntermediateTableUsage>(QueryNameComparer.Instance);
					}
					dictionary.Add(resolvedQuerySource.Name, intermediateTableUsage);
				}
			}
			if (dictionary != null)
			{
				this._expressionSourceUsageByQueryName.Add(query.Name, dictionary);
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00024C70 File Offset: 0x00022E70
		private void UpdateLetBindingUsage(ResolvedQueryDefinition query)
		{
			if (query.Let == null)
			{
				return;
			}
			Dictionary<string, IntermediateTableUsage> dictionary = null;
			foreach (ResolvedQueryLetBinding resolvedQueryLetBinding in query.Let)
			{
				IntermediateTableUsage intermediateTableUsage = IntermediateTableUsage.None;
				if (this._letBindingsReferencedInSelects.Contains(resolvedQueryLetBinding))
				{
					intermediateTableUsage |= IntermediateTableUsage.Regrouping;
				}
				if (this._letBindingsReferencedInFilters.Contains(resolvedQueryLetBinding))
				{
					intermediateTableUsage |= IntermediateTableUsage.Filtering;
				}
				if (intermediateTableUsage != IntermediateTableUsage.None)
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<string, IntermediateTableUsage>(QueryNameComparer.Instance);
					}
					dictionary.Add(resolvedQueryLetBinding.Name, intermediateTableUsage);
				}
			}
			if (dictionary != null)
			{
				this._letBindingUsageByQueryName.Add(query.Name, dictionary);
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00024D1C File Offset: 0x00022F1C
		private void VisitSelectClause(ResolvedQueryDefinition query)
		{
			int num;
			SemanticQueryDataShapeAnnotationExpressionAnalyzer.Analyze(query.Select, this._expressionSourcesReferencedInSelects, this._letBindingsReferencedInSelects, out num);
			if (num > 0)
			{
				this._visualCalculationExpressionCountByQueryName.Add(query.Name, num);
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00024D58 File Offset: 0x00022F58
		private void VisitLetClause(IReadOnlyList<ResolvedQueryLetBinding> letClause)
		{
			if (letClause == null)
			{
				return;
			}
			foreach (ResolvedQueryLetBinding resolvedQueryLetBinding in letClause)
			{
				ResolvedQuerySubqueryExpression resolvedQuerySubqueryExpression = resolvedQueryLetBinding.Expression as ResolvedQuerySubqueryExpression;
				if (resolvedQuerySubqueryExpression != null)
				{
					this.Visit(resolvedQuerySubqueryExpression.Subquery);
				}
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00024DB8 File Offset: 0x00022FB8
		private void VisitFromClause(IReadOnlyList<ResolvedQuerySource> fromClause)
		{
			if (fromClause == null)
			{
				return;
			}
			foreach (ResolvedQuerySource resolvedQuerySource in fromClause)
			{
				ResolvedExpressionSource resolvedExpressionSource = resolvedQuerySource as ResolvedExpressionSource;
				if (resolvedExpressionSource != null)
				{
					ResolvedQuerySubqueryExpression resolvedQuerySubqueryExpression = resolvedExpressionSource.Expression as ResolvedQuerySubqueryExpression;
					if (!(resolvedQuerySubqueryExpression == null))
					{
						this.Visit(resolvedQuerySubqueryExpression.Subquery);
					}
				}
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00024E28 File Offset: 0x00023028
		private void VisitWhereClause(ResolvedQueryDefinition query)
		{
			IntermediateTableFilterUsageAnnotationAnalyzer.Analyze(query.Where, this._expressionSourcesReferencedInFilters, this._letBindingsReferencedInFilters);
		}

		// Token: 0x04000499 RID: 1177
		private readonly Dictionary<string, ResolvedQueryDefinition> _queryDefinitionByName;

		// Token: 0x0400049A RID: 1178
		private readonly Dictionary<string, IReadOnlyDictionary<string, IntermediateTableUsage>> _letBindingUsageByQueryName;

		// Token: 0x0400049B RID: 1179
		private readonly Dictionary<string, IReadOnlyDictionary<string, IntermediateTableUsage>> _expressionSourceUsageByQueryName;

		// Token: 0x0400049C RID: 1180
		private readonly Dictionary<string, int> _visualCalculationExpressionCountByQueryName;

		// Token: 0x0400049D RID: 1181
		private readonly HashSet<ResolvedQueryLetBinding> _letBindingsReferencedInSelects;

		// Token: 0x0400049E RID: 1182
		private readonly HashSet<ResolvedQueryLetBinding> _letBindingsReferencedInFilters;

		// Token: 0x0400049F RID: 1183
		private readonly HashSet<string> _expressionSourcesReferencedInSelects;

		// Token: 0x040004A0 RID: 1184
		private readonly HashSet<string> _expressionSourcesReferencedInFilters;
	}
}
