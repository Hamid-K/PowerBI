using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000A2 RID: 162
	internal static class QueryFiltersGenerator
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x000173A8 File Offset: 0x000155A8
		internal static bool TryRun(QueryTranslationContext context, DsqTargetResolver dsqFilterTargetBuilder, IReadOnlyDictionary<int, HashSet<ResolvedQueryFilter>> projectionFilters, bool contextOnly, out QueryFilters filters)
		{
			QueryFiltersBuilder queryFiltersBuilder = new QueryFiltersBuilder();
			if (!QueryFiltersGenerator.TryApplyWhere(context, queryFiltersBuilder, dsqFilterTargetBuilder, context.QueryDefinition.Where, contextOnly))
			{
				filters = null;
				return false;
			}
			if (!QueryFiltersGenerator.TryApplyProjectionFilters(queryFiltersBuilder, projectionFilters))
			{
				filters = null;
				return false;
			}
			if (!contextOnly && !QueryFiltersGenerator.TryApplyHighlights(queryFiltersBuilder, context.DataShapeBinding))
			{
				filters = null;
				return false;
			}
			filters = queryFiltersBuilder.ToFilters();
			return true;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00017408 File Offset: 0x00015608
		private static bool TryApplyWhere(QueryTranslationContext context, QueryFiltersBuilder builder, DsqTargetResolver dsqFilterTargetBuilder, IReadOnlyList<ResolvedQueryFilter> where, bool contextOnly)
		{
			if (where == null)
			{
				return true;
			}
			DsqFilterConditionGenerationOptions dsqFilterConditionGenerationOptions = DsqFilterConditionGenerationOptions.Create(context.SharedContext.FederatedConceptualSchema.GetCapabilities(), contextOnly, contextOnly);
			IDsqFilterConditionGenerator dsqFilterConditionGenerator = new DsqFilterConditionGenerator(context.Expressions, context.SharedContext.FeatureSwitchProvider, context.SharedContext.ErrorContext, context.SharedContext.Telemetry, dsqFilterConditionGenerationOptions, context.SourceRefContext);
			DsqFilterGenerator dsqFilterGenerator = new DsqFilterGenerator(dsqFilterTargetBuilder, context.SharedContext.ErrorContext, dsqFilterConditionGenerator, contextOnly ? QueryFiltersGenerator.ContextOnlyDataShapeSuppressedFilters : null);
			int i = 0;
			while (i < where.Count)
			{
				global::System.ValueTuple<FilterUsageKind, List<GeneratedFilter>> valueTuple = QueryFiltersGenerator.GenerateFilter(context.SharedContext.DateTimeProvider, dsqFilterGenerator, where[i], new ExpressionContext(context.QueryDefinition.Name, SemanticQueryObjectType.Where, i));
				FilterUsageKind item = valueTuple.Item1;
				using (List<GeneratedFilter>.Enumerator enumerator = valueTuple.Item2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!QueryFiltersGenerator.TryApplyFilterCondition(enumerator.Current, builder, context, new ExpressionContext(context.QueryDefinition.Name, SemanticQueryObjectType.Where, i), item))
						{
							return false;
						}
					}
				}
				if (context.SharedContext.ErrorContext.HasError)
				{
					return false;
				}
				i++;
				continue;
			}
			return true;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00017554 File Offset: 0x00015754
		private static bool TryApplyFilterCondition(GeneratedFilter generatedCondition, QueryFiltersBuilder builder, QueryTranslationContext context, ExpressionContext expressionContext, FilterUsageKind filterUsageKind)
		{
			if (generatedCondition.ConversionStatus == FilterConversionStatus.Failed)
			{
				context.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedDataShapeFilterConditionExpressions(EngineMessageSeverity.Error, expressionContext.ObjectId, SemanticQueryObjectType.Where, expressionContext.OwningQueryName));
				return false;
			}
			if (generatedCondition.ConversionStatus == FilterConversionStatus.Succeeded)
			{
				builder.Add(generatedCondition, filterUsageKind);
			}
			return true;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000175A3 File Offset: 0x000157A3
		private static bool TryApplyProjectionFilters(QueryFiltersBuilder builder, IReadOnlyDictionary<int, HashSet<ResolvedQueryFilter>> projectionFilters)
		{
			if (projectionFilters.IsNullOrEmpty<KeyValuePair<int, HashSet<ResolvedQueryFilter>>>())
			{
				return true;
			}
			builder.SetProjectionFilters(projectionFilters);
			return true;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000175B8 File Offset: 0x000157B8
		private static bool TryApplyHighlights(QueryFiltersBuilder builder, DataShapeBinding dataShapeBinding)
		{
			if (dataShapeBinding == null)
			{
				return true;
			}
			IList<FilterDefinition> highlights = dataShapeBinding.Highlights;
			if (highlights.IsNullOrEmptyCollection<FilterDefinition>())
			{
				return true;
			}
			builder.SetHighlightFilters(highlights);
			return true;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000175EC File Offset: 0x000157EC
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "usageKind", "filterConditions" })]
		internal static global::System.ValueTuple<FilterUsageKind, List<GeneratedFilter>> GenerateFilter(IDateTimeProvider dateTimeProvider, DsqFilterGenerator filterGenerator, ResolvedQueryFilter resolvedQueryFilter, ExpressionContext expressionContext)
		{
			ResolvedQueryExpression resolvedQueryExpression = QueryFiltersGenerator.PrepareExpression(dateTimeProvider, resolvedQueryFilter);
			FilterUsageKind filterUsageKind;
			List<GeneratedFilter> list = filterGenerator.Generate(resolvedQueryExpression, expressionContext, resolvedQueryFilter.Target, out filterUsageKind);
			return new global::System.ValueTuple<FilterUsageKind, List<GeneratedFilter>>(filterUsageKind, list);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001761C File Offset: 0x0001581C
		internal static List<GeneratedFilterCondition> GenerateFilterCondition(IDateTimeProvider dateTimeProvider, IDsqFilterConditionGenerator filterConditionGenerator, ResolvedQueryFilter resolvedQueryFilter, ExpressionContext expressionContext)
		{
			ResolvedQueryExpression resolvedQueryExpression = QueryFiltersGenerator.PrepareExpression(dateTimeProvider, resolvedQueryFilter);
			FilterUsageKind filterUsageKind;
			return filterConditionGenerator.Generate(resolvedQueryExpression, expressionContext, resolvedQueryFilter.Target, out filterUsageKind);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00017641 File Offset: 0x00015841
		private static ResolvedQueryExpression PrepareExpression(IDateTimeProvider dateTimeProvider, ResolvedQueryFilter resolvedQueryFilter)
		{
			return resolvedQueryFilter.Condition.Accept<ResolvedQueryExpression>(new RelativeDateExpressionRewriter(dateTimeProvider)).Accept<ResolvedQueryExpression>(new DatePartFilterExpressionRewriter(dateTimeProvider)).Accept<ResolvedQueryExpression>(new ResolvedQueryExpressionDsqRewriter());
		}

		// Token: 0x0400033D RID: 829
		internal static readonly ISet<DsqFilterType> ContextOnlyDataShapeSuppressedFilters = new HashSet<DsqFilterType>
		{
			DsqFilterType.DataShape,
			DsqFilterType.AnyValue,
			DsqFilterType.AnyValueDefaultValueOverridesAncestors,
			DsqFilterType.DefaultValue,
			DsqFilterType.Apply,
			DsqFilterType.Exist
		};
	}
}
