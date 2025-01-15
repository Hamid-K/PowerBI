using System;
using System.Collections.Generic;
using Microsoft.DataShaping.SemanticQueryTranslation.Utils;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation.Grouping
{
	// Token: 0x02000024 RID: 36
	internal sealed class GroupingDaxTranslator
	{
		// Token: 0x06000115 RID: 277 RVA: 0x000063C4 File Offset: 0x000045C4
		internal static bool TryTranslate(SemanticQueryTranslatorContext context, ResolvedGroupingDefinition groupingDefinition, out SemanticQueryToDaxTranslationResult result)
		{
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression;
			if (!GroupingDaxTranslator.TryBuildSwitchExpression(context, groupingDefinition, out queryExpression))
			{
				result = null;
				return false;
			}
			string text;
			if (!SemanticQueryToDaxTranslator.TryTranslateQueryExpressionToDax(context, queryExpression, out text))
			{
				SemanticQueryTranslationUtils.EnsureContextError(context.ErrorContext, SemanticQueryTranslationMessages.GroupingTranslationError(EngineMessageSeverity.Error));
				result = null;
				return false;
			}
			result = SemanticQueryToDaxTranslationResultUtils.ForSingleExpression(text, context.ErrorContext);
			return true;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006414 File Offset: 0x00004614
		private static bool TryBuildSwitchExpression(SemanticQueryTranslatorContext context, ResolvedGroupingDefinition groupingDefinition, out Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression switchExpr)
		{
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression;
			if (!GroupingDaxTranslator.TryBuildGroupedColumnExpression(context, groupingDefinition.GroupedColumns, out queryExpression))
			{
				switchExpr = null;
				return false;
			}
			List<QuerySwitchCase> list;
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression2;
			if (!GroupingDaxTranslator.TryBuildGroupingCases(queryExpression, groupingDefinition.GroupItems, context, out list, out queryExpression2))
			{
				switchExpr = null;
				return false;
			}
			if (queryExpression2 == null)
			{
				queryExpression2 = queryExpression;
			}
			switchExpr = Literals.True.Switch(list, queryExpression2);
			return true;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006464 File Offset: 0x00004664
		private static bool TryBuildGroupingCases(Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression groupedColumnExpr, IReadOnlyList<ResolvedGroupItem> groupItems, SemanticQueryTranslatorContext context, out List<QuerySwitchCase> groupingCases, out Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression defaultCase)
		{
			int count = groupItems.Count;
			int num = ((groupItems[count - 1].Expression == null) ? (count - 1) : count);
			defaultCase = null;
			groupingCases = new List<QuerySwitchCase>(num);
			bool flag = false;
			for (int i = 0; i < count; i++)
			{
				ResolvedGroupItem resolvedGroupItem = groupItems[i];
				if (resolvedGroupItem.Expression != null)
				{
					Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = SemanticQueryExpressionTranslator.TranslateExpression(context.ErrorContext, context.Model, context.Schema, context.FeatureSwitchProvider, resolvedGroupItem.Expression, false);
					groupingCases.Add(new QuerySwitchCase(queryExpression, QueryExpressionBuilder.Literal(resolvedGroupItem.DisplayName)));
				}
				else if (resolvedGroupItem.BlankDefaultPlaceholder)
				{
					QueryIsNullExpression queryIsNullExpression = groupedColumnExpr.IsNull();
					groupingCases.Add(new QuerySwitchCase(queryIsNullExpression, QueryExpressionBuilder.Literal(resolvedGroupItem.DisplayName)));
					flag = true;
				}
				else
				{
					defaultCase = QueryExpressionBuilder.Literal(resolvedGroupItem.DisplayName);
				}
			}
			if (defaultCase == null)
			{
				ConceptualPrimitiveType? primitiveTypeKind = groupedColumnExpr.ConceptualResultType.GetPrimitiveTypeKind();
				ConceptualPrimitiveType conceptualPrimitiveType = ConceptualPrimitiveType.Text;
				if (!((primitiveTypeKind.GetValueOrDefault() == conceptualPrimitiveType) & (primitiveTypeKind != null)))
				{
					context.ErrorContext.Register(SemanticQueryTranslationMessages.GroupingTranslationErrorDefaultCaseDataType(EngineMessageSeverity.Error));
					groupingCases = null;
					defaultCase = null;
					return false;
				}
			}
			if (groupingCases.Count <= (flag ? 1 : 0))
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.GroupingTranslationErrorNoMeaningfulGroups(EngineMessageSeverity.Error));
				groupingCases = null;
				defaultCase = null;
				return false;
			}
			return true;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000065CC File Offset: 0x000047CC
		private static bool TryBuildGroupedColumnExpression(SemanticQueryTranslatorContext context, IReadOnlyList<ResolvedQueryExpression> gropedColumns, out Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression groupedColumnExpr)
		{
			groupedColumnExpr = SemanticQueryExpressionTranslator.TranslateExpression(context.ErrorContext, context.Model, context.Schema, context.FeatureSwitchProvider, gropedColumns[0], false);
			ConceptualPrimitiveType? primitiveTypeKind = groupedColumnExpr.ConceptualResultType.GetPrimitiveTypeKind();
			ConceptualPrimitiveType conceptualPrimitiveType = ConceptualPrimitiveType.Binary;
			if ((primitiveTypeKind.GetValueOrDefault() == conceptualPrimitiveType) & (primitiveTypeKind != null))
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.GroupingTranslationErrorBinaryColumn(EngineMessageSeverity.Error));
				groupedColumnExpr = null;
				return false;
			}
			return true;
		}
	}
}
