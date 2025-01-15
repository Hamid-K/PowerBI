using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000098 RID: 152
	internal sealed class QueryAutomaticProjectionGenerator
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x00015138 File Offset: 0x00013338
		internal static bool TryProcessSelect(QueryProjectionsBuilder builder, QueryTranslationContext context, QuerySortGenerator sort)
		{
			IReadOnlyList<ResolvedQuerySelect> select = context.QueryDefinition.Select;
			BitArray bitArray = new BitArray(select.Count);
			bool flag = false;
			for (int i = 0; i < select.Count; i++)
			{
				ResolvedQuerySelect resolvedQuerySelect = select[i];
				ProjectedDsqExpression projectedDsqExpression;
				IReadOnlyList<ResolvedQueryFilter> readOnlyList;
				if (SemanticQueryProjectionGenerator.TryExtractProjectionMeasure(context, new int?(i), resolvedQuerySelect.NativeReferenceName, resolvedQuerySelect.Expression, false, false, AllowedExpressionContent.TopLevelQuerySelect, out projectedDsqExpression, out readOnlyList))
				{
					if (!readOnlyList.IsNullOrEmpty<ResolvedQueryFilter>())
					{
						builder.AddFilters(readOnlyList, i);
					}
					SemanticQueryProjectionGenerator.PrepareMeasureForUseInQuery(resolvedQuerySelect.Expression, projectedDsqExpression, context.Expressions);
					builder.AddMeasure(projectedDsqExpression);
					bitArray[i] = true;
					sort.Rebind(projectedDsqExpression);
				}
			}
			for (int j = 0; j < select.Count; j++)
			{
				if (!bitArray[j])
				{
					ResolvedQuerySelect resolvedQuerySelect2 = select[j];
					QueryMemberBuilder queryMemberBuilder;
					if (QueryAutomaticProjectionGenerator.TryExtractMember(context, j, resolvedQuerySelect2.NativeReferenceName, resolvedQuerySelect2.Expression, sort, out queryMemberBuilder))
					{
						builder.AddPrimaryMember(queryMemberBuilder);
						bitArray[j] = true;
						flag |= queryMemberBuilder.Group.Subtotal > SubtotalType.None;
					}
				}
			}
			bool flag2 = false;
			for (int k = 0; k < select.Count; k++)
			{
				if (!bitArray[k])
				{
					context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.CannotProcessSelectExpression(EngineMessageSeverity.Error, k));
					flag2 = true;
				}
			}
			if (flag2)
			{
				return false;
			}
			QueryAutomaticProjectionGenerator.TryAddInferredAggregates(builder, flag);
			return true;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001529C File Offset: 0x0001349C
		internal static bool TryExtractMember(QueryTranslationContext context, int i, string nativeReferenceName, ResolvedQueryExpression expression, QuerySortGenerator sort, out QueryMemberBuilder builder)
		{
			builder = new QueryMemberBuilder(context.Expressions, context.SharedContext.ErrorContext, sort, null, true, SubtotalType.Before, context.SourceRefContext, QueryGroupBuilderOptions.AllDisabledOptions, false);
			return builder.TryAddProjection(expression, i, nativeReferenceName, false);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000152E4 File Offset: 0x000134E4
		private static bool TryAddInferredAggregates(QueryProjectionsBuilder builder, bool hasSubtotal)
		{
			int primaryMemberCount = builder.PrimaryMemberCount;
			int secondaryMemberCount = builder.SecondaryMemberCount;
			int measureCount = builder.MeasureCount;
			if (primaryMemberCount == 0)
			{
				if (secondaryMemberCount > 0)
				{
					return false;
				}
				if (secondaryMemberCount == 0)
				{
					return true;
				}
			}
			foreach (QueryMemberBuilder queryMemberBuilder in builder.PrimaryMembers)
			{
				foreach (ProjectedDsqExpression projectedDsqExpression in queryMemberBuilder.MeasureCalculations)
				{
					DsqExpressionAggregates measureAggregates = QueryAutomaticProjectionGenerator.GetMeasureAggregates(primaryMemberCount, secondaryMemberCount, hasSubtotal, projectedDsqExpression.SemanticQuerySelectIndex);
					projectedDsqExpression.Aggregates.AddRange(measureAggregates.Aggregates);
				}
			}
			if (measureCount > 0)
			{
				IList<ProjectedDsqExpression> measures = builder.Measures;
				for (int i = 0; i < measureCount; i++)
				{
					DsqExpressionAggregates measureAggregates2 = QueryAutomaticProjectionGenerator.GetMeasureAggregates(primaryMemberCount, secondaryMemberCount, hasSubtotal, measures[i].SemanticQuerySelectIndex);
					measures[i].Aggregates.AddRange(measureAggregates2.Aggregates);
				}
				return true;
			}
			if (primaryMemberCount == 1 && secondaryMemberCount == 0)
			{
				foreach (QueryGroupValueBuilder queryGroupValueBuilder in builder.PrimaryMembers[0].ValueBuilders)
				{
					if (!queryGroupValueBuilder.IsIdentityOnly)
					{
						ProjectedDsqExpression projectedDsqExpression2 = queryGroupValueBuilder.GetProjectedDsqExpression();
						projectedDsqExpression2.Aggregates.Add(new DsqExpressionCountAggregate(projectedDsqExpression2.SemanticQuerySelectIndex));
					}
				}
			}
			return true;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00015474 File Offset: 0x00013674
		private static DsqExpressionAggregates GetMeasureAggregates(int primaryGroupCount, int secondaryGroupCount, bool hasSubtotal, int? selectIndex)
		{
			DsqExpressionAggregates dsqExpressionAggregates = new DsqExpressionAggregates();
			if (secondaryGroupCount != 0)
			{
				return dsqExpressionAggregates;
			}
			if (hasSubtotal)
			{
				dsqExpressionAggregates.Add(DsqExpressionSubtotalAggregate.Instance);
			}
			if (primaryGroupCount == 1)
			{
				dsqExpressionAggregates.Add(new DsqExpressionMaxAggregate(DataShapeBindingMaxAggregate.CreateContainer(IncludeAllTypes.Default), selectIndex, null));
				dsqExpressionAggregates.Add(new DsqExpressionMinAggregate(DataShapeBindingMinAggregate.CreateContainer(IncludeAllTypes.Default), selectIndex, null));
			}
			return dsqExpressionAggregates;
		}
	}
}
