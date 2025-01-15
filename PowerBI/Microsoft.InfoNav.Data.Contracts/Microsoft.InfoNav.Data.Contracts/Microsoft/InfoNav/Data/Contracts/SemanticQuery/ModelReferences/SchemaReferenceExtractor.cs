using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.SemanticQuery.ModelReferences
{
	// Token: 0x02000096 RID: 150
	internal static class SchemaReferenceExtractor
	{
		// Token: 0x06000361 RID: 865 RVA: 0x00009AC7 File Offset: 0x00007CC7
		internal static void ExtractReferences(QueryDefinition definition, ISet<QueryExpression> referenceCollector)
		{
			new SchemaReferenceExtractor.DefinitionVisitor(definition, referenceCollector).Visit(definition);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00009AD6 File Offset: 0x00007CD6
		internal static void ExtractReferences(List<EntitySource> entitySources, List<QueryExpressionContainer> expressions, ISet<QueryExpression> referenceCollector)
		{
			SchemaReferenceExtractor.ExtractReferences(new QueryDefinition
			{
				From = entitySources,
				Select = expressions
			}, referenceCollector);
		}

		// Token: 0x02000308 RID: 776
		[ImmutableObject(true)]
		private sealed class DefinitionVisitor : DefaultQueryDefinitionVisitor
		{
			// Token: 0x06001957 RID: 6487 RVA: 0x0002D9EB File Offset: 0x0002BBEB
			internal DefinitionVisitor(QueryDefinition definition, ISet<QueryExpression> referenceCollector)
			{
				this._expressionExtractor = new ExpressionSchemaReferenceExtractor(definition.From, referenceCollector, false);
				this._references = referenceCollector;
			}

			// Token: 0x06001958 RID: 6488 RVA: 0x0002DA0D File Offset: 0x0002BC0D
			protected override void VisitEntitySource(EntitySource source)
			{
				if (!string.IsNullOrEmpty(source.Entity))
				{
					this._references.Add(new QuerySourceRefExpression
					{
						Entity = source.Entity
					});
				}
				base.VisitEntitySource(source);
			}

			// Token: 0x06001959 RID: 6489 RVA: 0x0002DA40 File Offset: 0x0002BC40
			protected override void VisitExpression(QueryExpressionContainer expression)
			{
				QuerySubqueryExpression subquery = expression.Subquery;
				if (subquery != null && subquery.Query != null)
				{
					new SchemaReferenceExtractor.DefinitionVisitor(subquery.Query, this._references).Visit(subquery.Query);
					return;
				}
				if (expression.Expression != null)
				{
					this._expressionExtractor.VisitExpression(expression.Expression);
				}
			}

			// Token: 0x0400095E RID: 2398
			private readonly ExpressionSchemaReferenceExtractor _expressionExtractor;

			// Token: 0x0400095F RID: 2399
			private readonly ISet<QueryExpression> _references;
		}
	}
}
