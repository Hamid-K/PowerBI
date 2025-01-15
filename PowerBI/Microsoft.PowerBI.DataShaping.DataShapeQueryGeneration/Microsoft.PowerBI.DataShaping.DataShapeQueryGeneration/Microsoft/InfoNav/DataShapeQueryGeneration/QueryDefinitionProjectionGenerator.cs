using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200009E RID: 158
	internal sealed class QueryDefinitionProjectionGenerator
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x00016834 File Offset: 0x00014A34
		internal static bool TryRun(QueryTranslationContext context, DataShapeGenerationOptions generationOptions, bool trackGroupKeysAndSortKeysForReferencing, out QueryProjections projections)
		{
			if (QueryDefinitionProjectionGenerator.HasUnsupportedFeatures(generationOptions, context.QueryDefinition, context.SharedContext.ErrorContext))
			{
				projections = null;
				return false;
			}
			QuerySortGenerator querySortGenerator;
			if (!QuerySortGenerator.TryParse(context, generationOptions.OmitOrderBy, out querySortGenerator))
			{
				projections = null;
				return false;
			}
			QueryProjectionsBuilder queryProjectionsBuilder = new QueryProjectionsBuilder(querySortGenerator.HasMeasure, context.SharedContext.ErrorContext);
			if (!QueryDefinitionProjectionGenerator.TryProcessSelect(context.QueryDefinition.Select, context, queryProjectionsBuilder, querySortGenerator, generationOptions, trackGroupKeysAndSortKeysForReferencing))
			{
				projections = null;
				return false;
			}
			if (!QueryDefinitionProjectionGenerator.TryAddGroupBy(queryProjectionsBuilder, context.QueryDefinition.GroupBy, context.SharedContext.ErrorContext))
			{
				projections = null;
				return false;
			}
			projections = queryProjectionsBuilder.ToProjections(null);
			querySortGenerator.RegisterUnhandledOrderBy(context.SharedContext.ErrorContext);
			return true;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000168E8 File Offset: 0x00014AE8
		private static bool HasUnsupportedFeatures(DataShapeGenerationOptions generationOptions, ResolvedQueryDefinition resolvedQuery, DataShapeGenerationErrorContext errorContext)
		{
			if (generationOptions != null && resolvedQuery.Top != null)
			{
				if (generationOptions.OmitOrderBy)
				{
					errorContext.Register(DataShapeGenerationMessages.UnsupportedFeatureWithGenerationOptionInQueryDefinition(EngineMessageSeverity.Error, "Top", "OmitOrderBy"));
					return true;
				}
				if (generationOptions.ProjectIdentityOnly)
				{
					errorContext.Register(DataShapeGenerationMessages.UnsupportedFeatureWithGenerationOptionInQueryDefinition(EngineMessageSeverity.Error, "Top", "ProjectIdentityOnly"));
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001694C File Offset: 0x00014B4C
		private static bool TryProcessSelect(IReadOnlyList<ResolvedQuerySelect> selects, QueryTranslationContext context, QueryProjectionsBuilder builder, QuerySortGenerator sort, DataShapeGenerationOptions generationOptions, bool trackGroupKeysAndSortKeysForReferencing)
		{
			BitArray bitArray = new BitArray(selects.Count);
			for (int i = 0; i < selects.Count; i++)
			{
				ResolvedQuerySelect resolvedQuerySelect = selects[i];
				if (QueryDefinitionProjectionGenerator.ShouldNotProject(generationOptions, i))
				{
					bitArray[i] = true;
				}
				else
				{
					bool flag = context.DataShapeBinding.IsSuppressedJoinPredicate(context.QueryDefinition.Name, i, context.QueryDefinition.Select, context.SuppressedJoinPredicatesByName);
					bool flag2 = context.DataShapeBinding.IsHiddenProjection(context.QueryDefinition.Name, i, context.QueryDefinition.Select, context.HiddenProjections);
					ProjectedDsqExpression projectedDsqExpression;
					IReadOnlyList<ResolvedQueryFilter> readOnlyList;
					if (SemanticQueryProjectionGenerator.TryExtractMeasure(context.Expressions, context.SharedContext.ErrorContext, new int?(i), resolvedQuerySelect.NativeReferenceName, resolvedQuerySelect.Expression, flag, flag2, generationOptions.AllowedSelectExpressionContent, new ExpressionContext(context.QueryDefinition.Name, SemanticQueryObjectType.Select, i), out projectedDsqExpression, out readOnlyList))
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
			}
			QueryMemberBuilder queryMemberBuilder = null;
			for (int j = 0; j < selects.Count; j++)
			{
				if (!bitArray[j])
				{
					if (queryMemberBuilder == null)
					{
						queryMemberBuilder = new QueryMemberBuilder(context.Expressions, context.SharedContext.ErrorContext, sort, null, true, SubtotalType.None, context.SourceRefContext, new QueryGroupBuilderOptions(generationOptions.ProjectIdentityOnly, generationOptions.OmitOrderBy, generationOptions.SuppressAutomaticGroupSorts, generationOptions.SuppressModelGrouping, false, trackGroupKeysAndSortKeysForReferencing), false);
					}
					ResolvedQuerySelect resolvedQuerySelect2 = selects[j];
					if (queryMemberBuilder.TryAddProjection(resolvedQuerySelect2.Expression, j, resolvedQuerySelect2.NativeReferenceName, false))
					{
						bitArray[j] = true;
					}
				}
			}
			if (queryMemberBuilder != null)
			{
				builder.AddPrimaryMember(queryMemberBuilder);
			}
			bool flag3 = false;
			for (int k = 0; k < selects.Count; k++)
			{
				if (!bitArray[k])
				{
					context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.CannotProcessSelectExpression(EngineMessageSeverity.Error, k));
					flag3 = true;
				}
			}
			return !flag3;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00016B68 File Offset: 0x00014D68
		private static bool TryAddGroupBy(QueryProjectionsBuilder projectionsBuilder, IReadOnlyList<ResolvedQueryExpression> groupBy, DataShapeGenerationErrorContext errorContext)
		{
			if (groupBy.IsNullOrEmpty<ResolvedQueryExpression>())
			{
				return true;
			}
			if (projectionsBuilder.PrimaryMembers.IsNullOrEmptyCollection<QueryMemberBuilder>())
			{
				errorContext.Register(DataShapeGenerationMessages.GroupByWithoutColumnSelect(EngineMessageSeverity.Error));
				return false;
			}
			QueryMemberBuilder queryMemberBuilder = projectionsBuilder.PrimaryMembers[0];
			for (int i = 0; i < groupBy.Count; i++)
			{
				ResolvedQueryExpression resolvedQueryExpression = groupBy[i];
				if (!queryMemberBuilder.TryAddGroupBy(resolvedQueryExpression, i))
				{
					errorContext.Register(DataShapeGenerationMessages.UnsupportedGroupByInSemanticQuery(EngineMessageSeverity.Error, i));
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00016BDA File Offset: 0x00014DDA
		private static bool ShouldNotProject(DataShapeGenerationOptions generationOptions, int i)
		{
			return !generationOptions.SelectIndicesToPreserve.IsNullOrEmpty<int>() && !generationOptions.SelectIndicesToPreserve.Contains(i);
		}
	}
}
