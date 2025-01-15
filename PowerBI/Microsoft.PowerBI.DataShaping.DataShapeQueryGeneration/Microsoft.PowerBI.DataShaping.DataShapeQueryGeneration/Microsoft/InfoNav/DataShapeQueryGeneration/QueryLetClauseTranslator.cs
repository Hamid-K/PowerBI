using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000B3 RID: 179
	internal static class QueryLetClauseTranslator
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x00018C74 File Offset: 0x00016E74
		internal static bool TryTranslate(ResolvedQueryDefinition resolvedQuery, DataShapeGenerationInternalContext internalContext, SemanticQueryDataShapeAnnotations annotations, IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByName, IReadOnlyList<DataShapeBindingHiddenProjections> hiddenProjections, bool filterEmptyGroups, DataShapeBuilderContext dataShapeBuilderContext, DataShapeBuilder dataShapeBuilder, QueryLetReferenceContext outerLetContext, QueryParameterReferenceContext parameterRefContext, out QueryLetReferenceContext letContext)
		{
			letContext = outerLetContext;
			IReadOnlyDictionary<string, IntermediateTableUsage> readOnlyDictionary;
			if (!annotations.LetBindingUsageByQueryName.TryGetValue(resolvedQuery.Name, out readOnlyDictionary))
			{
				letContext = outerLetContext;
				return true;
			}
			DsqTableExpressionGenerator dsqTableExpressionGenerator = new DsqTableExpressionGenerator(internalContext, annotations, suppressedJoinPredicatesByName, hiddenProjections, filterEmptyGroups, dataShapeBuilderContext, dataShapeBuilder, parameterRefContext);
			return QueryLetClauseTranslator.TryTranslate(resolvedQuery, ref letContext, readOnlyDictionary, dsqTableExpressionGenerator);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00018CC8 File Offset: 0x00016EC8
		private static bool TryTranslate(ResolvedQueryDefinition resolvedQuery, ref QueryLetReferenceContext letContext, IReadOnlyDictionary<string, IntermediateTableUsage> letBindingUsageByName, DsqTableExpressionGenerator tableExpressionGenerator)
		{
			foreach (ResolvedQueryLetBinding resolvedQueryLetBinding in resolvedQuery.Let)
			{
				IntermediateTableUsage intermediateTableUsage;
				if (letBindingUsageByName.TryGetValue(resolvedQueryLetBinding.Name, out intermediateTableUsage) && intermediateTableUsage != IntermediateTableUsage.None)
				{
					IIntermediateTableSchema intermediateTableSchema;
					if (!tableExpressionGenerator.TryGenerate(resolvedQueryLetBinding.Expression, intermediateTableUsage, in letContext, "Let", resolvedQueryLetBinding.Name, out intermediateTableSchema))
					{
						return false;
					}
					letContext = letContext.Add(resolvedQueryLetBinding.Name, intermediateTableSchema);
				}
			}
			return true;
		}
	}
}
