using System;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200026D RID: 621
	internal static class QueryResolutionMessages
	{
		// Token: 0x060012D7 RID: 4823 RVA: 0x00021848 File Offset: 0x0001FA48
		internal static QueryResolutionMessage InvalidQueryDefinition(string cause)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve QueryDefinition because it could not be validated: {0}", cause), null);
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0002185B File Offset: 0x0001FA5B
		internal static QueryResolutionMessage InvalidExpressionType(string type)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve QueryDefinition due to invalid {0} expression. Query must be upgraded to latest version first.", type), null);
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0002186E File Offset: 0x0001FA6E
		internal static QueryResolutionMessage InvalidPropertyExpression(string type)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve QueryDefinition due to invalid {0} expression. Expected expression to be SourceRef.", type), null);
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00021881 File Offset: 0x0001FA81
		internal static QueryResolutionMessage CouldNotResolveColumnReference(string property, string sourceEntity)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve QueryDefinition due to invalid Column reference '{0}' in the SourceEntity '{1}'. This error often happens after the Column or Entity was renamed or deleted. Please update the query/visual to use the new name or undo the name change.", property.MarkAsModelInfo(), sourceEntity.MarkAsModelInfo()), new ScrubbedEntityPropertyReference(sourceEntity, property, null));
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x000218A6 File Offset: 0x0001FAA6
		internal static QueryResolutionMessage CouldNotResolveMeasureReference(string property, string sourceEntity, string schemaId)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve QueryDefinition due to invalid Measure reference '{0}' in the SourceEntity '{2}'.'{1}'. This error often happens after the Measure or Entity was renamed or deleted. Please update the query/visual to use the new name or undo the name change.", property.MarkAsModelInfo(), sourceEntity.MarkAsModelInfo(), schemaId), new ScrubbedEntityPropertyReference(sourceEntity, property, schemaId));
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000218CC File Offset: 0x0001FACC
		internal static QueryResolutionMessage CouldNotResolveSourceSchemaReference(string schemaName)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve QueryDefinition due to invalid Schema reference '{0}'.", schemaName), null);
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x000218DF File Offset: 0x0001FADF
		internal static QueryResolutionMessage CouldNotResolveSourceEntityReference(string sourceEntity)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve QueryDefinition due to invalid SourceEntity reference '{0}'. This error often happens after the Entity was renamed or deleted. Please update the query/visual to use the new name or undo the name change.", sourceEntity.MarkAsModelInfo()), new ScrubbedString(sourceEntity));
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x000218FC File Offset: 0x0001FAFC
		internal static QueryResolutionMessage CouldNotResolveSourceExpression(string sourceName)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve QueryDefinition due to invalid source expression '{0}'.", sourceName), null);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0002190F File Offset: 0x0001FB0F
		internal static QueryResolutionMessage CouldNotResolveLetExpression(string letName)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve QueryDefinition due to invalid let expression '{0}'.", letName.MarkAsModelInfo()), null);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00021927 File Offset: 0x0001FB27
		internal static QueryResolutionMessage CouldNotResolveParameterExpression(string parameterName)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve QueryDefinition due to invalid parameter expression '{0}'.", parameterName.MarkAsModelInfo()), null);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0002193F File Offset: 0x0001FB3F
		internal static QueryResolutionMessage CouldNotResolveHierarchyReference(string hierarchy, string sourceEntity)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve QueryDefinition due to invalid Hierarchy reference '{0}' in the SourceEntity '{1}'. This error often happens after the Hierarchy or Entity was renamed or deleted. Please update the query/visual to use the new name or undo the name change.", hierarchy.MarkAsModelInfo(), sourceEntity.MarkAsModelInfo()), new ScrubbedString(StringUtil.FormatInvariant("'{0}'[{1}]", sourceEntity, hierarchy)));
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0002196D File Offset: 0x0001FB6D
		internal static QueryResolutionMessage CouldNotResolveHierarchyLevelReference(string level, string hierarchy, string sourceEntity)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve QueryDefinition due to invalid Hierarchy Level reference '{0}' in the Hierarchy '{1}' of SourceEntity '{2}'. This error often happens after the HierarchyLevel, Hierarchy, or Entity was renamed or deleted. Please update the query/visual to use the new name or undo the name change.", level.MarkAsModelInfo(), hierarchy.MarkAsModelInfo(), sourceEntity.MarkAsModelInfo()), new ScrubbedString(StringUtil.FormatInvariant("'{0}'[{1}][{2}]", sourceEntity, hierarchy, level)));
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x000219A2 File Offset: 0x0001FBA2
		internal static QueryResolutionMessage InvalidHierarchyExpression()
		{
			return new QueryResolutionMessage("Could not resolve QueryDefinition due to invalid Hierarchy expression. Expected expression to be SourceRef.", null);
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x000219AF File Offset: 0x0001FBAF
		internal static QueryResolutionMessage InvalidHierarchyLevelExpression()
		{
			return new QueryResolutionMessage("Could not resolve QueryDefinition due to invalid HierarchyLevel expression. Expected expression to be Hierarchy.", null);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x000219BC File Offset: 0x0001FBBC
		internal static QueryResolutionMessage InvalidPropertyVariationSourceExpression()
		{
			return new QueryResolutionMessage("Could not resolve QueryDefinition due to invalid VariationSource expression. Expected expression to be SourceRef.", null);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x000219C9 File Offset: 0x0001FBC9
		internal static QueryResolutionMessage CouldNotResolveVariationSourcePropertyReference(string property, string sourceEntity)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve QueryDefinition due to invalid Property reference '{0}' in the SourceEntity '{1}'. This error often happens after the Property or Entity was renamed or deleted. Please update the query/visual to use the new name or undo the name change.", property.MarkAsModelInfo(), sourceEntity.MarkAsModelInfo()), new ScrubbedString(StringUtil.FormatInvariant("'{0}'[{1}]", sourceEntity, property)));
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000219F7 File Offset: 0x0001FBF7
		internal static QueryResolutionMessage CouldNotResolveVariationSourceReference(string variation, string property, string sourceEntity)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve QueryDefinition due to invalid VariationSource reference '{0}' on Property '{1}' in the SourceEntity '{2}'. This error often happens after the Property or Entity was renamed or deleted. Please update the query/visual to use the new name or undo the name change.", variation.MarkAsModelInfo(), property.MarkAsModelInfo(), sourceEntity.MarkAsModelInfo()), new ScrubbedString(StringUtil.FormatInvariant("'{0}'[{1}].{2}", sourceEntity, property, variation)));
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00021A2C File Offset: 0x0001FC2C
		internal static QueryResolutionMessage InvalidPrimitiveValue()
		{
			return new QueryResolutionMessage("Could not resolve QueryDefinition due to invalid Primitive Value specified.", null);
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00021A39 File Offset: 0x0001FC39
		internal static QueryResolutionMessage CouldNotResolveSemanticQueryDefinition()
		{
			return new QueryResolutionMessage("Could not resolve QueryDefinition due to an unknown error.", null);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00021A46 File Offset: 0x0001FC46
		internal static QueryResolutionMessage InvalidQueryFilters()
		{
			return new QueryResolutionMessage("Could not resolve invalid QueryFilter.", null);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00021A53 File Offset: 0x0001FC53
		internal static QueryResolutionMessage CouldNotResolveSubqueryDefinition()
		{
			return new QueryResolutionMessage("Could not resolve SubqueryDefinition due to an unknown error.", null);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00021A60 File Offset: 0x0001FC60
		internal static QueryResolutionMessage UnexpectedTransformTableRef(string tableName)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve the QueryDefinition. The query contains a reference to transform table '{0}' in an unexpected location.", tableName.MarkAsModelInfo()), null);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00021A78 File Offset: 0x0001FC78
		internal static QueryResolutionMessage UnknownTransformTableRef(string tableName)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve the QueryDefinition. The query contains a reference to unknown transform table '{0}'. An expression can only refer to a tranform table that appears earlier in the query.", tableName.MarkAsModelInfo()), null);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00021A90 File Offset: 0x0001FC90
		internal static QueryResolutionMessage UnknownTransformTableColumnRef(string tableName, string columnName)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve the QueryDefinition. The query contains a reference to column '{1}' in transform table '{0}'. The column does not exist in the specified table.", tableName.MarkAsModelInfo(), columnName.MarkAsModelInfo()), null);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00021AAE File Offset: 0x0001FCAE
		internal static QueryResolutionMessage DuplicateExpressionName(string expressionName)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve the QueryDefinition. The query contains at least two expressions in its select clause with identical name '{0}'.", expressionName.MarkAsModelInfo()), null);
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00021AC6 File Offset: 0x0001FCC6
		internal static QueryResolutionMessage DuplicateNativeReferenceName(string nativeReferenceName)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve the QueryDefinition. The query contains at least two expressions in its select clause with identical native reference name '{0}'.", nativeReferenceName.MarkAsModelInfo()), null);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00021ADE File Offset: 0x0001FCDE
		internal static QueryResolutionMessage DuplicateSourceName(string sourceName)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve the QueryDefinition. The query contains at least two sources with identical name '{0}'. Source names must be unique across the query and all its subqueries.", sourceName.MarkAsModelInfo()), null);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00021AF6 File Offset: 0x0001FCF6
		internal static QueryResolutionMessage DuplicateLetName(string letName)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve the QueryDefinition. The query contains at least two let bindings with identical name '{0}'. Let names must be unique across the query and all its subqueries.", letName.MarkAsModelInfo()), null);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00021B0E File Offset: 0x0001FD0E
		internal static QueryResolutionMessage DuplicateParameterName(string parameterName)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve the QueryDefinition. The query contains at least two parameter declarations with identical name '{0}'. Parameter names must be unique.", parameterName.MarkAsModelInfo()), null);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00021B26 File Offset: 0x0001FD26
		internal static QueryResolutionMessage QueryParameterDeclaredOnSubquery()
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve the QueryDefinition. The query declares a query parameter on a subquery. Query parameters declarations are only allowed on the top-level query.", new object[0]), null);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00021B3E File Offset: 0x0001FD3E
		internal static QueryResolutionMessage DuplicateTransformName(string transformName)
		{
			return new QueryResolutionMessage(StringUtil.FormatInvariant("Could not resolve the QueryDefinition. The query contains at least two transforms with identical name '{0}'. Transform names must be unique across the query.", transformName.MarkAsModelInfo()), null);
		}
	}
}
