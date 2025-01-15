using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.SemanticQueryTranslation.Utils;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation.Clustering
{
	// Token: 0x02000026 RID: 38
	internal sealed class ClusteringColumnGenerator
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00006814 File Offset: 0x00004A14
		internal static bool TryGenerate(SemanticQueryTranslatorContext context, string clusterTableName, string clusterIdColumnName, IReadOnlyList<ClusteringLookupTuple> lookupValues, IReadOnlyList<KeyValuePair<ResolvedQueryExpression, string>> displayNames, string defaultDisplayPrefix, out string dax)
		{
			IReadOnlyList<ConceptualTypeColumn> readOnlyList = ClusteringColumnGenerator.CreateColumnsForNewEntity(lookupValues, clusterIdColumnName);
			EntitySet entitySet = null;
			IConceptualEntity conceptualEntity = null;
			if (context.UseConceptualSchema)
			{
				conceptualEntity = TransientEdmItemFactory.BuildEntity(clusterTableName, context.Schema, readOnlyList);
			}
			else
			{
				entitySet = TransientEdmItemFactory.BuildEntitySet(clusterTableName, context.Model.Version, readOnlyList);
			}
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = QdmHelpers.BuildFieldExpression(entitySet, clusterIdColumnName, conceptualEntity);
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression2 = ClusteringColumnGenerator.CreateLookupValue(context, queryExpression, lookupValues, entitySet, conceptualEntity);
			if (!displayNames.IsNullOrEmpty<KeyValuePair<ResolvedQueryExpression, string>>())
			{
				queryExpression2 = ClusteringColumnGenerator.ApplyDisplayNames(context, displayNames, queryExpression2, defaultDisplayPrefix);
			}
			else if (!defaultDisplayPrefix.IsNullOrEmpty<char>())
			{
				queryExpression2 = ClusteringColumnGenerator.CreatePrefixedExpression(queryExpression2, defaultDisplayPrefix);
			}
			return SemanticQueryToDaxTranslator.TryTranslateQueryExpressionToDax(context, queryExpression2, out dax);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000068A4 File Offset: 0x00004AA4
		private static IReadOnlyList<ConceptualTypeColumn> CreateColumnsForNewEntity(IReadOnlyList<ClusteringLookupTuple> lookupValues, string clusterIdColumnName)
		{
			List<ConceptualTypeColumn> list = lookupValues.Select((ClusteringLookupTuple v) => new ConceptualTypeColumn(ConceptualPrimitiveResultType.Text, v.DaxColumnName)).ToList<ConceptualTypeColumn>();
			list.Add(new ConceptualTypeColumn(ConceptualPrimitiveResultType.Text, clusterIdColumnName));
			return list;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000068E4 File Offset: 0x00004AE4
		private static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression ApplyDisplayNames(SemanticQueryTranslatorContext context, IReadOnlyList<KeyValuePair<ResolvedQueryExpression, string>> displayNames, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression clusterColumnExpression, string defaultDisplayPrefix)
		{
			QueryExpressionWithLocalVariablesBuilder queryExpressionWithLocalVariablesBuilder = new QueryExpressionWithLocalVariablesBuilder();
			QueryVariableReferenceExpression queryVariableReferenceExpression = queryExpressionWithLocalVariablesBuilder.DeclareVariable(clusterColumnExpression, "ClusterValue");
			IReadOnlyList<QuerySwitchCase> readOnlyList = ClusteringColumnGenerator.CreateSwitchCases(context, displayNames, queryVariableReferenceExpression.ConceptualResultType);
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = ClusteringColumnGenerator.CreateDefaultSwitchValue(queryVariableReferenceExpression, defaultDisplayPrefix);
			QuerySwitchExpression querySwitchExpression = queryVariableReferenceExpression.Switch(readOnlyList, queryExpression);
			queryExpressionWithLocalVariablesBuilder.SetResult(querySwitchExpression);
			return queryExpressionWithLocalVariablesBuilder.ToQueryExpression();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000692E File Offset: 0x00004B2E
		private static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression CreateDefaultSwitchValue(Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression expr, string prefix)
		{
			if (!prefix.IsNullOrEmpty<char>())
			{
				return ClusteringColumnGenerator.CreatePrefixedExpression(expr, prefix);
			}
			return expr.ConvertToString();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006946 File Offset: 0x00004B46
		private static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression CreatePrefixedExpression(Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression expr, string prefix)
		{
			return QueryExpressionBuilder.Literal(prefix).Concatenate(expr);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000695C File Offset: 0x00004B5C
		private static IReadOnlyList<QuerySwitchCase> CreateSwitchCases(SemanticQueryTranslatorContext context, IReadOnlyList<KeyValuePair<ResolvedQueryExpression, string>> displayNames, ConceptualResultType keyExpressionResultType)
		{
			List<QuerySwitchCase> list = new List<QuerySwitchCase>(displayNames.Count);
			foreach (KeyValuePair<ResolvedQueryExpression, string> keyValuePair in displayNames)
			{
				Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = (ClusteringColumnGenerator.IsNullLiteralExpression(keyValuePair.Key) ? ClusteringColumnGenerator.CreateNullExpression(keyExpressionResultType) : SemanticQueryExpressionTranslator.TranslateExpression(context.ErrorContext, context.Model, context.Schema, context.FeatureSwitchProvider, keyValuePair.Key, false));
				Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression2 = ((keyValuePair.Value == null) ? ClusteringColumnGenerator.CreateNullExpression(ConceptualPrimitiveResultType.Text) : QueryExpressionBuilder.Literal(keyValuePair.Value));
				QuerySwitchCase querySwitchCase = new QuerySwitchCase(queryExpression, queryExpression2);
				list.Add(querySwitchCase);
			}
			return list.AsReadOnlyCollection<QuerySwitchCase>();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006A24 File Offset: 0x00004C24
		private static bool IsNullLiteralExpression(ResolvedQueryExpression expression)
		{
			ResolvedQueryLiteralExpression resolvedQueryLiteralExpression = expression as ResolvedQueryLiteralExpression;
			return resolvedQueryLiteralExpression != null && resolvedQueryLiteralExpression.Value == NullPrimitiveValue.Instance;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006A50 File Offset: 0x00004C50
		private static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression CreateNullExpression(ConceptualResultType type)
		{
			return type.Null();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006A58 File Offset: 0x00004C58
		private static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression CreateLookupValue(SemanticQueryTranslatorContext context, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression clusterColumn, IReadOnlyList<ClusteringLookupTuple> clusteringLookupTuples, EntitySet clusterEntitySet, IConceptualEntity clusterEntity)
		{
			List<QueryLookupTuple> list = new List<QueryLookupTuple>(clusteringLookupTuples.Count);
			foreach (ClusteringLookupTuple clusteringLookupTuple in clusteringLookupTuples)
			{
				Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = QdmHelpers.BuildFieldExpression(clusterEntitySet, clusteringLookupTuple.DaxColumnName, clusterEntity);
				Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression2 = SemanticQueryExpressionTranslator.TranslateExpression(context.ErrorContext, context.Model, context.Schema, context.FeatureSwitchProvider, clusteringLookupTuple.ColumnExpression, false);
				list.Add(new QueryLookupTuple(queryExpression, queryExpression2));
			}
			return clusterColumn.LookupValue(list);
		}

		// Token: 0x0400007E RID: 126
		private const string ClusterValueVariableName = "ClusterValue";
	}
}
