using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.ExpressionAnalysis;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000BA RID: 186
	internal static class QueryParametersClauseTranslator
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x0001938C File Offset: 0x0001758C
		internal static bool TryTranslate(ResolvedQueryDefinition resolvedQuery, DataShapeGenerationInternalContext internalContext, DataShapeBuilder dataShapeBuilder, out QueryParameterReferenceContext parameterRefContext)
		{
			if (resolvedQuery.Parameters.IsNullOrEmpty<ResolvedQueryParameterDeclaration>())
			{
				parameterRefContext = QueryParameterReferenceContext.Empty;
				return true;
			}
			Dictionary<string, IntermediateQueryParameter> dictionary = new Dictionary<string, IntermediateQueryParameter>(ConceptualNameComparer.Instance);
			ResolvedQueryTypeExpressionEvaluator resolvedQueryTypeExpressionEvaluator = new ResolvedQueryTypeExpressionEvaluator();
			foreach (ResolvedQueryParameterDeclaration resolvedQueryParameterDeclaration in resolvedQuery.Parameters)
			{
				ConceptualResultType conceptualResultType;
				if (!resolvedQueryTypeExpressionEvaluator.TryEvaluate(resolvedQueryParameterDeclaration.TypeExpression, out conceptualResultType))
				{
					internalContext.ErrorContext.Register(DataShapeGenerationMessages.InvalidQueryParameterTypeExpression(EngineMessageSeverity.Error, resolvedQueryParameterDeclaration.Name));
					parameterRefContext = QueryParameterReferenceContext.Empty;
					return false;
				}
				dataShapeBuilder.WithQueryParameterDeclaration(resolvedQueryParameterDeclaration.Name, conceptualResultType);
				dictionary.Add(resolvedQueryParameterDeclaration.Name, new IntermediateQueryParameter(resolvedQueryParameterDeclaration.Name, conceptualResultType));
			}
			parameterRefContext = new QueryParameterReferenceContext(dictionary);
			return true;
		}
	}
}
