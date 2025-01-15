using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000C6 RID: 198
	internal static class SemanticQueryProjectionGenerator
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x0001B6E4 File Offset: 0x000198E4
		internal static bool TryRun(QueryTranslationContext context, out QueryProjections projections)
		{
			QuerySortGenerator querySortGenerator;
			if (!QuerySortGenerator.TryParse(context, false, out querySortGenerator))
			{
				projections = null;
				return false;
			}
			QueryProjectionsBuilder queryProjectionsBuilder = new QueryProjectionsBuilder(querySortGenerator.HasMeasure, context.SharedContext.ErrorContext);
			if (!SemanticQueryProjectionGenerator.TryProcessSelect(context, queryProjectionsBuilder, querySortGenerator))
			{
				projections = null;
				return false;
			}
			QueryProjectionsBuilder queryProjectionsBuilder2 = queryProjectionsBuilder;
			ResolvedDataReduction resolvedDataReduction = context.ResolvedDataReduction;
			ResolvedDataReductionDataWindow resolvedDataReductionDataWindow = ((resolvedDataReduction != null) ? resolvedDataReduction.Primary : null) as ResolvedDataReductionDataWindow;
			projections = queryProjectionsBuilder2.ToProjections((resolvedDataReductionDataWindow != null) ? resolvedDataReductionDataWindow.RestartTokens : null);
			querySortGenerator.RegisterUnhandledOrderBy(context.SharedContext.ErrorContext);
			return true;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001B764 File Offset: 0x00019964
		private static bool TryProcessSelect(QueryTranslationContext context, QueryProjectionsBuilder builder, QuerySortGenerator sort)
		{
			if (context.DataShapeBinding != null)
			{
				if (!QueryBindingProjectionGenerator.TryGenerateProjections(builder, context, sort))
				{
					return false;
				}
			}
			else if (!QueryAutomaticProjectionGenerator.TryProcessSelect(builder, context, sort))
			{
				return false;
			}
			if (builder.PrimaryMemberCount == 0 && builder.SecondaryMemberCount > 0)
			{
				context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.SecondaryGroupsWithoutPrimary(EngineMessageSeverity.Error));
				return false;
			}
			return true;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001B7C4 File Offset: 0x000199C4
		internal static bool TryExtractProjectionMeasure(QueryTranslationContext context, int? selectIndex, string nativeReferenceName, ResolvedQueryExpression expr, bool suppressJoinPredicate, bool isHiddenProjection, AllowedExpressionContentFlags allowedContent, out ProjectedDsqExpression measureExpr, out IReadOnlyList<ResolvedQueryFilter> filters)
		{
			return SemanticQueryProjectionGenerator.TryExtractMeasure(context.Expressions, context.SharedContext.ErrorContext, selectIndex, nativeReferenceName, expr, suppressJoinPredicate, isHiddenProjection, allowedContent, new ExpressionContext(context.QueryDefinition.Name, SemanticQueryObjectType.Select, selectIndex), out measureExpr, out filters);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001B80C File Offset: 0x00019A0C
		internal static bool TryExtractMeasure(DsqExpressionGenerator expressionGenerator, DataShapeGenerationErrorContext errorContext, int? selectIndex, string nativeReferenceName, ResolvedQueryExpression expr, bool suppressJoinPredicate, bool isHiddenProjection, AllowedExpressionContentFlags allowedContent, ExpressionContext expressionContext, out ProjectedDsqExpression measureExpr)
		{
			IReadOnlyList<ResolvedQueryFilter> readOnlyList;
			return SemanticQueryProjectionGenerator.TryExtractMeasure(expressionGenerator, errorContext, selectIndex, nativeReferenceName, expr, suppressJoinPredicate, isHiddenProjection, allowedContent, expressionContext, out measureExpr, out readOnlyList);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001B830 File Offset: 0x00019A30
		internal static bool TryExtractMeasure(DsqExpressionGenerator expressionGenerator, DataShapeGenerationErrorContext errorContext, int? selectIndex, string nativeReferenceName, ResolvedQueryExpression expr, bool suppressJoinPredicate, bool isHiddenProjection, AllowedExpressionContentFlags allowedContent, ExpressionContext expressionContext, out ProjectedDsqExpression measureExpr, out IReadOnlyList<ResolvedQueryFilter> filters)
		{
			if (expr is ResolvedQueryAggregationExpression || expr is ResolvedQueryPercentileExpression || expr is ResolvedQueryArithmeticExpression || expr is ResolvedQueryScopedEvalExpression || expr is ResolvedQueryFilteredEvalExpression || expr is ResolvedQueryNativeFormatExpression || expr is ResolvedQueryNativeVisualCalculationExpression)
			{
				allowedContent = (suppressJoinPredicate ? (allowedContent & ~AllowedExpressionContentFlags.VisualCalculations) : allowedContent);
				GeneratedDsqExpression generatedDsqExpression;
				if (ResolvedQueryExpressionValidator.Validate(expr, errorContext, allowedContent, expressionContext) && expressionGenerator.TryGenerate(expr, out generatedDsqExpression) && generatedDsqExpression.HasAggregate)
				{
					measureExpr = new ProjectedDsqExpression(selectIndex, new ProjectedDsqExpressionValue(generatedDsqExpression.Expression, SemanticQueryProjectionGenerator.GetFormatStringOrDefault(expr as ResolvedQueryAggregationExpression), null), suppressJoinPredicate, generatedDsqExpression.IsScalar, nativeReferenceName, isHiddenProjection);
					filters = generatedDsqExpression.Filters;
					return true;
				}
			}
			filters = null;
			IConceptualMeasure conceptualMeasure;
			if (expr.TryGetAsProperty(out conceptualMeasure))
			{
				bool flag = conceptualMeasure.ConceptualDataType.IsScalar();
				measureExpr = new ProjectedDsqExpression(selectIndex, new ProjectedDsqExpressionValue(conceptualMeasure.DsqExpression(), conceptualMeasure.FormatString, conceptualMeasure), suppressJoinPredicate, new bool?(flag), nativeReferenceName, isHiddenProjection);
				return true;
			}
			IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn;
			if (expressionGenerator.TryGetAsTransformColumn(expr, out intermediateQueryTransformTableColumn) && (intermediateQueryTransformTableColumn.ActAs == TransformTableColumnActAs.Measure || intermediateQueryTransformTableColumn.ActAs == TransformTableColumnActAs.Detail))
			{
				ExpressionNode expressionNode = intermediateQueryTransformTableColumn.DsqExpression();
				measureExpr = new ProjectedDsqExpression(selectIndex, new ProjectedDsqExpressionValue(expressionNode, intermediateQueryTransformTableColumn.FormatString, intermediateQueryTransformTableColumn.UnderlyingConceptualColumn), suppressJoinPredicate, intermediateQueryTransformTableColumn.IsScalar, nativeReferenceName, isHiddenProjection);
				return true;
			}
			measureExpr = null;
			return false;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001B980 File Offset: 0x00019B80
		private static string GetFormatStringOrDefault(ResolvedQueryAggregationExpression expr)
		{
			if (expr == null)
			{
				return null;
			}
			QueryAggregateFunction function = expr.Function;
			if (function == QueryAggregateFunction.Count || function == QueryAggregateFunction.CountNonNull)
			{
				return null;
			}
			IConceptualProperty conceptualProperty;
			if (!expr.Expression.TryGetAsProperty(out conceptualProperty))
			{
				return null;
			}
			return conceptualProperty.FormatString;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001B9C0 File Offset: 0x00019BC0
		internal static void PrepareMeasureForUseInQuery(ResolvedQueryExpression selection, ProjectedDsqExpression measure, DsqExpressionGenerator expressionGenerator)
		{
			if (measure.IsContextOnly)
			{
				return;
			}
			IConceptualMeasure conceptualMeasure;
			if (selection.TryGetAsPropertyOrFilteredEvalMeasureProperty(out conceptualMeasure))
			{
				measure.Value.DynamicFormatString = SemanticQueryProjectionGenerator.AddDynamicFormatProjection(conceptualMeasure.DynamicFormatString);
				measure.Value.DynamicFormatCulture = SemanticQueryProjectionGenerator.AddDynamicFormatProjection(conceptualMeasure.DynamicFormatCulture);
				return;
			}
			IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn;
			if (expressionGenerator.TryGetAsTransformColumn(selection, out intermediateQueryTransformTableColumn) && intermediateQueryTransformTableColumn.UnderlyingExpression.TryGetAsProperty(out conceptualMeasure))
			{
				measure.Value.DynamicFormatString = SemanticQueryProjectionGenerator.AddDynamicFormatProjection(conceptualMeasure.DynamicFormatString, intermediateQueryTransformTableColumn, expressionGenerator);
				measure.Value.DynamicFormatCulture = SemanticQueryProjectionGenerator.AddDynamicFormatProjection(conceptualMeasure.DynamicFormatCulture, intermediateQueryTransformTableColumn, expressionGenerator);
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001BA58 File Offset: 0x00019C58
		private static ProjectedDsqExpression AddDynamicFormatProjection(IConceptualMeasure dynamicFormatMeasure, IntermediateQueryTransformTableColumn existingColumn, DsqExpressionGenerator expressionGenerator)
		{
			if (dynamicFormatMeasure == null)
			{
				return null;
			}
			IntermediateQueryTransformTableColumn orCreateColumn = IntermediateQueryTransformTable.GetOrCreateColumn(dynamicFormatMeasure, TransformTableColumnActAs.Measure, existingColumn, expressionGenerator, false);
			bool? flag = null;
			if (orCreateColumn.UnderlyingConceptualColumn != null)
			{
				flag = new bool?(orCreateColumn.UnderlyingConceptualColumn.ConceptualDataType.IsScalar());
			}
			return new ProjectedDsqExpression(null, new ProjectedDsqExpressionValue(orCreateColumn.DsqExpression(), orCreateColumn.FormatString, orCreateColumn.UnderlyingConceptualColumn), true, flag, null, false);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001BAC8 File Offset: 0x00019CC8
		private static ProjectedDsqExpression AddDynamicFormatProjection(IConceptualMeasure dynamicFormatMeasure)
		{
			if (dynamicFormatMeasure == null)
			{
				return null;
			}
			bool flag = dynamicFormatMeasure.ConceptualDataType.IsScalar();
			return new ProjectedDsqExpression(null, new ProjectedDsqExpressionValue(dynamicFormatMeasure.DsqExpression(), dynamicFormatMeasure.FormatString, dynamicFormatMeasure), true, new bool?(flag), null, false);
		}
	}
}
