using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ModelParameters;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000A0 RID: 160
	internal sealed class QueryFilters
	{
		// Token: 0x060005E4 RID: 1508 RVA: 0x00016C37 File Offset: 0x00014E37
		internal QueryFilters(IList<Filter> filters, ReadOnlyCollection<FilterDefinition> highlightFilter, IReadOnlyDictionary<int, HashSet<ResolvedQueryFilter>> projectionFilters)
		{
			this._highlightFilter = highlightFilter;
			this._filters = filters;
			this._projectionFilters = projectionFilters;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00016C54 File Offset: 0x00014E54
		internal IList<Filter> Filters
		{
			get
			{
				return this._filters;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00016C5C File Offset: 0x00014E5C
		internal bool HasHighlightFilter
		{
			get
			{
				return !this._highlightFilter.IsNullOrEmpty<FilterDefinition>();
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00016C6C File Offset: 0x00014E6C
		internal IReadOnlyDictionary<int, HashSet<ResolvedQueryFilter>> ProjectionFilters
		{
			get
			{
				return this._projectionFilters;
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00016C74 File Offset: 0x00014E74
		internal bool TryCreateHighlightFilters(QueryTranslationContext queryTranslationContext, FilterCondition highlightProjectionFilter, out FilterCondition filterCondition)
		{
			HashSet<FilterCondition> hashSet = new HashSet<FilterCondition>(new FilterConditionComparer(ExpressionComparerByOriginalNode.Instance));
			if (highlightProjectionFilter != null)
			{
				hashSet.Add(highlightProjectionFilter);
			}
			ConceptualCapabilities capabilities = queryTranslationContext.SharedContext.FederatedConceptualSchema.GetCapabilities();
			IFeatureSwitchProvider featureSwitchProvider = queryTranslationContext.SharedContext.FeatureSwitchProvider;
			IDsqFilterConditionGenerator dsqFilterConditionGenerator = QueryFilters.CreateFilterConditionGenerator(queryTranslationContext, DsqFilterConditionGenerationOptions.Create(capabilities));
			for (int i = 0; i < this._highlightFilter.Count; i++)
			{
				FilterDefinition filterDefinition = this._highlightFilter[i];
				List<ResolvedQueryFilter> list;
				if (!QueryFilters.TryResolveFilterDefinition(queryTranslationContext.SharedContext, filterDefinition, DataShapeGenerationErrorCode.CouldNotResolveHighlightFilter, out list))
				{
					filterCondition = null;
					return false;
				}
				DataShapeGenerationErrorContextAdapter dataShapeGenerationErrorContextAdapter = new DataShapeGenerationErrorContextAdapter(queryTranslationContext.SharedContext.ErrorContext, DataShapeGenerationErrorCode.FilterIncompatibleWithParameter, ErrorSource.User);
				if (featureSwitchProvider.IsEnabled(FeatureSwitchKind.MParameterColumnMapping) && ModelParametersExtractor.AnyFilterHasParameterMapping(list, dataShapeGenerationErrorContextAdapter))
				{
					queryTranslationContext.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.ParameterMappingsNotSupportedOnHighlights());
					filterCondition = null;
					return false;
				}
				List<FilterCondition> list2;
				if (!QueryFilters.TryCreateFilterConditionsImpl(dsqFilterConditionGenerator, queryTranslationContext.SharedContext, list, new ExpressionContext(queryTranslationContext.QueryDefinition.Name, SemanticQueryObjectType.Highlight, i), FilterRestrictions.Expressions, out list2))
				{
					filterCondition = null;
					return false;
				}
				hashSet.UnionWith(list2);
			}
			filterCondition = QueryFiltersBuilder.FlattenAndWrapAll(hashSet);
			return true;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00016D94 File Offset: 0x00014F94
		internal bool TryCreateFilterCondition(QueryTranslationContext queryTranslationContext, IReadOnlyList<ResolvedQueryFilter> resolvedFilters, ExpressionContext expressionContext, HashSet<DsqFilterType> allowedTypes, out FilterCondition filterCondition)
		{
			ConceptualCapabilities capabilities = queryTranslationContext.SharedContext.FederatedConceptualSchema.GetCapabilities();
			List<FilterCondition> list;
			if (!QueryFilters.TryCreateFilterConditionsImpl(QueryFilters.CreateFilterConditionGenerator(queryTranslationContext, DsqFilterConditionGenerationOptions.Create(capabilities)), queryTranslationContext.SharedContext, resolvedFilters, expressionContext, allowedTypes, out list))
			{
				filterCondition = null;
				return false;
			}
			filterCondition = QueryFiltersBuilder.FlattenAndWrapAll(list);
			return true;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00016DE4 File Offset: 0x00014FE4
		internal static bool TryResolveAndCreateFilterConditions(QueryTranslationContext queryTranslationContext, IReadOnlyList<FilterDefinition> filterDefinitions, DataShapeGenerationErrorCode failureErrorCode, SemanticQueryObjectType objectType, HashSet<DsqFilterType> allowedTypes, DsqFilterConditionGenerationOptions generationOptions, out List<FilterCondition> filterConditions)
		{
			IDsqFilterConditionGenerator dsqFilterConditionGenerator = QueryFilters.CreateFilterConditionGenerator(queryTranslationContext, generationOptions);
			filterConditions = new List<FilterCondition>(filterDefinitions.Count);
			for (int i = 0; i < filterDefinitions.Count; i++)
			{
				FilterDefinition filterDefinition = filterDefinitions[i];
				List<FilterCondition> list;
				if (!QueryFilters.TryResolveAndCreateFilterConditions(queryTranslationContext, dsqFilterConditionGenerator, filterDefinition, failureErrorCode, new ExpressionContext(queryTranslationContext.QueryDefinition.Name, objectType, i), allowedTypes, out list))
				{
					filterConditions = null;
					return false;
				}
				filterConditions.AddRange(list);
			}
			return true;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00016E58 File Offset: 0x00015058
		private static bool TryResolveAndCreateFilterConditions(QueryTranslationContext queryTranslationContext, IDsqFilterConditionGenerator filterConditionGenerator, FilterDefinition filterDefinition, DataShapeGenerationErrorCode failureErrorCode, ExpressionContext expressionContext, HashSet<DsqFilterType> allowedTypes, out List<FilterCondition> filterConditions)
		{
			filterConditions = null;
			List<ResolvedQueryFilter> list;
			if (!QueryFilters.TryResolveFilterDefinition(queryTranslationContext.SharedContext, filterDefinition, failureErrorCode, out list) || !QueryFilters.TryCreateFilterConditionsImpl(filterConditionGenerator, queryTranslationContext.SharedContext, list, expressionContext, allowedTypes, out filterConditions))
			{
				filterConditions = null;
				return false;
			}
			return true;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00016E98 File Offset: 0x00015098
		private static bool TryCreateFilterConditionsImpl(IDsqFilterConditionGenerator filterConditionGenerator, DataShapeGenerationInternalContext sharedContext, IReadOnlyList<ResolvedQueryFilter> resolvedFilters, ExpressionContext expressionContext, HashSet<DsqFilterType> allowedTypes, out List<FilterCondition> filterConditions)
		{
			filterConditions = new List<FilterCondition>(resolvedFilters.Count);
			for (int i = 0; i < resolvedFilters.Count; i++)
			{
				foreach (GeneratedFilterCondition generatedFilterCondition in QueryFiltersGenerator.GenerateFilterCondition(sharedContext.DateTimeProvider, filterConditionGenerator, resolvedFilters[i], expressionContext))
				{
					if (generatedFilterCondition.ConversionStatus == FilterConversionStatus.Failed)
					{
						sharedContext.ErrorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedDataShapeFilterConditionExpressions(EngineMessageSeverity.Error, i, expressionContext.ObjectType, expressionContext.OwningQueryName));
						filterConditions = null;
						return false;
					}
					if (generatedFilterCondition.ConversionStatus == FilterConversionStatus.Succeeded)
					{
						DsqFilterType value = generatedFilterCondition.FilterType.Value;
						if (!allowedTypes.Contains(value))
						{
							sharedContext.ErrorContext.Register(DataShapeGenerationMessages.InvalidFilterType(EngineMessageSeverity.Error, i, value.ToString(), expressionContext.ObjectType, expressionContext.OwningQueryName));
							filterConditions = null;
							return false;
						}
						filterConditions.Add(generatedFilterCondition.Condition);
					}
				}
			}
			return true;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00016FC0 File Offset: 0x000151C0
		private static IDsqFilterConditionGenerator CreateFilterConditionGenerator(QueryTranslationContext queryTranslationContext, DsqFilterConditionGenerationOptions generationOptions)
		{
			return new DsqFilterConditionGenerator(queryTranslationContext.Expressions, queryTranslationContext.SharedContext.FeatureSwitchProvider, queryTranslationContext.SharedContext.ErrorContext, queryTranslationContext.SharedContext.Telemetry, generationOptions, queryTranslationContext.SourceRefContext);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00016FF8 File Offset: 0x000151F8
		private static bool TryResolveFilterDefinition(DataShapeGenerationInternalContext sharedContext, FilterDefinition filter, DataShapeGenerationErrorCode failureErrorCode, out List<ResolvedQueryFilter> resolvedFilters)
		{
			QueryResolutionErrorContext queryResolutionErrorContext = new QueryResolutionErrorContext(new DataShapeGenerationErrorContextAdapter(sharedContext.ErrorContext, failureErrorCode, ErrorSourceCategory.InputDoesNotMatchModel));
			bool flag = QueryDefinitionResolver.TryResolveQueryFilters(filter.Where, sharedContext.FederatedConceptualSchema, filter.From, queryResolutionErrorContext, new HashSet<string>(), out resolvedFilters);
			if (queryResolutionErrorContext.UnresolvedModelReferences.Count > 0)
			{
				sharedContext.ErrorContext.Register(DataShapeGenerationMessages.CouldNotResolveModelReferencesInSemanticQuery(EngineMessageSeverity.Error, queryResolutionErrorContext.UnresolvedModelReferences.ToArray()));
			}
			return flag;
		}

		// Token: 0x04000335 RID: 821
		internal static readonly QueryFilters Empty = new QueryFilters(null, null, null);

		// Token: 0x04000336 RID: 822
		private readonly ReadOnlyCollection<FilterDefinition> _highlightFilter;

		// Token: 0x04000337 RID: 823
		private readonly IReadOnlyDictionary<int, HashSet<ResolvedQueryFilter>> _projectionFilters;

		// Token: 0x04000338 RID: 824
		private readonly IList<Filter> _filters;
	}
}
