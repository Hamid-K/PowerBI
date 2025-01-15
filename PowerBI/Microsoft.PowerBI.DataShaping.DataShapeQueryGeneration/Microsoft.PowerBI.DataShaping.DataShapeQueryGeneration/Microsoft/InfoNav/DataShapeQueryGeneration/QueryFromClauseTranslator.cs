using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000A3 RID: 163
	internal static class QueryFromClauseTranslator
	{
		// Token: 0x06000604 RID: 1540 RVA: 0x000176A8 File Offset: 0x000158A8
		internal static bool TryTranslate(ResolvedQueryDefinition resolvedQuery, DataShapeGenerationInternalContext internalContext, SemanticQueryDataShapeAnnotations annotations, IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByName, IReadOnlyList<DataShapeBindingHiddenProjections> hiddenProjections, bool filterEmptyGroups, DataShapeBuilderContext dataShapeBuilderContext, DataShapeBuilder dataShapeBuilder, in QueryLetReferenceContext letContext, QueryParameterReferenceContext parameterRefContext, out QuerySourceExpressionReferenceContext sourceContext)
		{
			IReadOnlyDictionary<string, IntermediateTableUsage> readOnlyDictionary;
			if (!annotations.ExpressionSourceUsageByQueryName.TryGetValue(resolvedQuery.Name, out readOnlyDictionary))
			{
				sourceContext = QuerySourceExpressionReferenceContext.Empty;
				return true;
			}
			DsqTableExpressionGenerator dsqTableExpressionGenerator = new DsqTableExpressionGenerator(internalContext, annotations, suppressedJoinPredicatesByName, hiddenProjections, filterEmptyGroups, dataShapeBuilderContext, dataShapeBuilder, parameterRefContext);
			return QueryFromClauseTranslator.TryTranslate(resolvedQuery, in letContext, readOnlyDictionary, dsqTableExpressionGenerator, out sourceContext);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000176F4 File Offset: 0x000158F4
		private static bool TryTranslate(ResolvedQueryDefinition resolvedQuery, in QueryLetReferenceContext letContext, IReadOnlyDictionary<string, IntermediateTableUsage> sourceUsageByName, DsqTableExpressionGenerator tableExpressionTranslator, out QuerySourceExpressionReferenceContext sourceContext)
		{
			Dictionary<string, IIntermediateTableSchema> dictionary = null;
			foreach (ResolvedQuerySource resolvedQuerySource in resolvedQuery.From)
			{
				ResolvedExpressionSource resolvedExpressionSource = resolvedQuerySource as ResolvedExpressionSource;
				IntermediateTableUsage intermediateTableUsage;
				if (resolvedExpressionSource != null && sourceUsageByName.TryGetValue(resolvedQuerySource.Name, out intermediateTableUsage) && intermediateTableUsage != IntermediateTableUsage.None)
				{
					IIntermediateTableSchema intermediateTableSchema;
					if (!tableExpressionTranslator.TryGenerate(resolvedExpressionSource.Expression, intermediateTableUsage, in letContext, "From", resolvedQuerySource.Name, out intermediateTableSchema))
					{
						sourceContext = null;
						return false;
					}
					Util.AddToLazyDictionary<string, IIntermediateTableSchema>(ref dictionary, resolvedQuerySource.Name, intermediateTableSchema, QueryNameComparer.Instance);
				}
			}
			if (dictionary == null)
			{
				sourceContext = QuerySourceExpressionReferenceContext.Empty;
			}
			else
			{
				sourceContext = new QuerySourceExpressionReferenceContext(dictionary);
			}
			return true;
		}
	}
}
