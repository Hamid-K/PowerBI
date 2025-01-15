using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.SemanticQuery.ModelReferences
{
	// Token: 0x02000095 RID: 149
	[ImmutableObject(true)]
	internal sealed class ExpressionSchemaReferenceExtractor : DefaultQueryExpressionVisitor
	{
		// Token: 0x06000355 RID: 853 RVA: 0x00009824 File Offset: 0x00007A24
		internal ExpressionSchemaReferenceExtractor(IReadOnlyList<EntitySource> sources, ISet<QueryExpression> referenceCollector, bool includeReferencesFromSubqueries = false)
		{
			this._sourceRewriter = EmbeddedToStandaloneQueryExpressionRewriter.Create(sources);
			this._references = referenceCollector;
			this._includeSubqueryReferences = includeReferencesFromSubqueries;
			this._sourceMap = sources.Select((EntitySource s) => Util.ToKeyValuePair<string, EntitySource>(s.Name, s)).ToDictionary(QueryNameComparer.Instance);
			this._visitedSubquerySources = (this._includeSubqueryReferences ? new HashSet<string>(QueryNameComparer.Instance) : null);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000098A4 File Offset: 0x00007AA4
		protected internal override void Visit(QuerySourceRefExpression expression)
		{
			base.Visit(expression);
			if (this.IsSchemaBoundSource(expression))
			{
				this.AddAsStandaloneExpression(expression);
				return;
			}
			QueryDefinition queryDefinition;
			if (this._includeSubqueryReferences && this.TryGetSubqueryFromSource(expression, out queryDefinition) && this._visitedSubquerySources.Add(expression.Source))
			{
				SchemaReferenceExtractor.ExtractReferences(queryDefinition, this._references);
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00009900 File Offset: 0x00007B00
		protected internal override void Visit(QueryColumnExpression expression)
		{
			base.Visit(expression);
			if (this.IsSchemaColumn(expression))
			{
				this.AddAsStandaloneExpression(expression);
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000991E File Offset: 0x00007B1E
		protected internal override void Visit(QueryMeasureExpression expression)
		{
			base.Visit(expression);
			this.AddAsStandaloneExpression(expression);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00009933 File Offset: 0x00007B33
		protected internal override void Visit(QueryPropertyVariationSourceExpression expression)
		{
			base.Visit(expression);
			this.AddAsStandaloneExpression(expression);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00009948 File Offset: 0x00007B48
		protected internal override void Visit(QueryHierarchyExpression expression)
		{
			base.Visit(expression);
			this.AddAsStandaloneExpression(expression);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000995D File Offset: 0x00007B5D
		protected internal override void Visit(QueryHierarchyLevelExpression expression)
		{
			base.Visit(expression);
			this.AddAsStandaloneExpression(expression);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00009974 File Offset: 0x00007B74
		private void AddAsStandaloneExpression(QueryExpressionContainer expression)
		{
			if (expression.Expression == null)
			{
				return;
			}
			QueryExpression queryExpression = expression.Expression.Accept<QueryExpression>(this._sourceRewriter);
			this._references.Add(queryExpression);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000099B0 File Offset: 0x00007BB0
		private bool IsSchemaColumn(QueryColumnExpression column)
		{
			QueryExpressionContainer expression = column.Expression;
			return expression != null && expression.SourceRef != null && this.IsSchemaBoundSource(expression.SourceRef);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000099EC File Offset: 0x00007BEC
		private bool IsSchemaBoundSource(QuerySourceRefExpression sourceRef)
		{
			EntitySource entitySource;
			return !string.IsNullOrEmpty(sourceRef.Source) && this._sourceMap.TryGetValue(sourceRef.Source, out entitySource) && !string.IsNullOrEmpty(entitySource.Entity);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00009A2C File Offset: 0x00007C2C
		private bool TryGetSubqueryFromSource(QuerySourceRefExpression sourceRef, out QueryDefinition subquery)
		{
			EntitySource entitySource;
			if (string.IsNullOrEmpty(sourceRef.Source) || !this._sourceMap.TryGetValue(sourceRef.Source, out entitySource) || entitySource.Expression == null || entitySource.Expression.Subquery == null || entitySource.Expression.Subquery.Query == null)
			{
				subquery = null;
				return false;
			}
			subquery = entitySource.Expression.Subquery.Query;
			return true;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00009AAC File Offset: 0x00007CAC
		private static bool IsValidStandaloneExpresion(QueryExpressionContainer expression)
		{
			ExpressionSchemaReferenceExtractor.ErrorContext errorContext = new ExpressionSchemaReferenceExtractor.ErrorContext();
			new QueryExpressionValidator(errorContext).ValidateStandaloneExpression(expression);
			return !errorContext.HasError;
		}

		// Token: 0x040001CE RID: 462
		private readonly EmbeddedToStandaloneQueryExpressionRewriter _sourceRewriter;

		// Token: 0x040001CF RID: 463
		private readonly ISet<QueryExpression> _references;

		// Token: 0x040001D0 RID: 464
		private readonly bool _includeSubqueryReferences;

		// Token: 0x040001D1 RID: 465
		private readonly IReadOnlyDictionary<string, EntitySource> _sourceMap;

		// Token: 0x040001D2 RID: 466
		private readonly ISet<string> _visitedSubquerySources;

		// Token: 0x02000306 RID: 774
		private sealed class ErrorContext : IErrorContext
		{
			// Token: 0x17000546 RID: 1350
			// (get) Token: 0x06001950 RID: 6480 RVA: 0x0002D9AE File Offset: 0x0002BBAE
			public bool HasError
			{
				get
				{
					return this._hasError;
				}
			}

			// Token: 0x06001951 RID: 6481 RVA: 0x0002D9B6 File Offset: 0x0002BBB6
			public void RegisterError(string messageTemplate, params object[] args)
			{
				this._hasError = true;
			}

			// Token: 0x06001952 RID: 6482 RVA: 0x0002D9BF File Offset: 0x0002BBBF
			public void RegisterWarning(string messageTemplate, params object[] args)
			{
			}

			// Token: 0x0400095B RID: 2395
			private bool _hasError;
		}
	}
}
