using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration
{
	// Token: 0x0200011A RID: 282
	internal static class QueryParameterGenerator
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x00029174 File Offset: 0x00027374
		internal static void Generate(IReadOnlyList<QueryParameterDeclaration> parameters, IQueryBuilder queryBuilder, WritableGeneratedQueryParameterMap parameterMap)
		{
			if (parameters.IsNullOrEmpty<QueryParameterDeclaration>())
			{
				return;
			}
			BatchQueryGenerationNamingContext batchQueryGenerationNamingContext = new BatchQueryGenerationNamingContext();
			foreach (QueryParameterDeclaration queryParameterDeclaration in parameters)
			{
				string text = batchQueryGenerationNamingContext.CreateAndRegisterUniqueName(queryParameterDeclaration.Name);
				QueryParameterReferenceExpression queryParameterReferenceExpression = queryBuilder.DeclareQueryParameter(queryParameterDeclaration.Type, text);
				parameterMap.Add(queryParameterDeclaration.Name, queryParameterReferenceExpression);
			}
		}
	}
}
