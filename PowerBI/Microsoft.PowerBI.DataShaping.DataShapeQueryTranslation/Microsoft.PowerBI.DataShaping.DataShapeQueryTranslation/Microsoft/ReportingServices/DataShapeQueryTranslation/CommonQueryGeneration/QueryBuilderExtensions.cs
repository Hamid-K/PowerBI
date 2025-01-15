using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration
{
	// Token: 0x02000117 RID: 279
	public static class QueryBuilderExtensions
	{
		// Token: 0x06000A8B RID: 2699 RVA: 0x00028C78 File Offset: 0x00026E78
		internal static bool TryFindInnermostCompatibleGroupReference(this QueryBuilder queryBuilder, IList<GroupReference> groupRefs, QueryExpression queryExpression, out GroupReference compatibleGroupRef)
		{
			for (int i = groupRefs.Count - 1; i >= 0; i--)
			{
				GroupReference groupReference = groupRefs[i];
				if (queryBuilder.CanAddGroupDetail(groupReference.Group, queryExpression))
				{
					compatibleGroupRef = groupReference;
					return true;
				}
			}
			compatibleGroupRef = null;
			return false;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00028CB8 File Offset: 0x00026EB8
		internal static string AddGroupDetailToQuery(this QueryBuilder queryBuilder, GroupReference qdmGroupRef, QueryExpression queryExpression, string fallbackCandidateName)
		{
			IEnumerable<QueryExpression> expressions = qdmGroupRef.Group.Keys.GetExpressions();
			queryBuilder.NamingContext.CreateOrReuseNameForDetail(expressions, queryExpression, null, fallbackCandidateName);
			return queryBuilder.AddOrReuseGroupDetail(qdmGroupRef.Group, queryExpression, false, false).Name;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00028CFC File Offset: 0x00026EFC
		internal static QueryExpression AddGroupKeyToQuery(QueryBuilder queryBuilder, IQueryExpressionGenerator expressionGenerator, TranslationErrorContext errorContext, Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey groupKey, Identifier id, out string groupKeyName)
		{
			QueryExpressionContext queryExpressionContext = expressionGenerator.TranslateExpression(groupKey.Value.ExpressionId.Value, new ExpressionContext(errorContext, ObjectType.GroupKey, id, "Value"));
			Contract.RetailAssert(!queryExpressionContext.CalculateAsMeasure, "Measures as group expressions are not supported.");
			QueryExpression queryExpression = queryExpressionContext.QueryExpression;
			groupKeyName = queryBuilder.NamingContext.CreateOrReuseNameForGroupKey(queryExpression, null, id.Value);
			return queryExpression;
		}
	}
}
