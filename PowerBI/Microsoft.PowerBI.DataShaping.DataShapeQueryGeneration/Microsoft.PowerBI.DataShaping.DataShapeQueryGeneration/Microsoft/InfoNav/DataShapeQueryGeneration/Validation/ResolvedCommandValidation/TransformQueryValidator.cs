using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Validation.ResolvedCommandValidation
{
	// Token: 0x020000F1 RID: 241
	internal sealed class TransformQueryValidator : ITransformQueryValidator
	{
		// Token: 0x0600081E RID: 2078 RVA: 0x0001FEFC File Offset: 0x0001E0FC
		public TransformQueryValidator(DataShapeGenerationErrorContext dataShapeGenerationErrorContext, IFeatureSwitchProvider featureSwitchProvider)
		{
			this._errorContext = dataShapeGenerationErrorContext;
			this._featureSwitchProvider = featureSwitchProvider;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001FF14 File Offset: 0x0001E114
		public void Validate(ResolvedSemanticQueryDataShapeCommand command, SemanticQueryDataShapeAnnotations annotations)
		{
			ResolvedQueryDefinition query = command.QueryDataShape.Query;
			DataShapeBinding binding = command.QueryDataShape.Binding;
			TransformQueryValidator.ValidateQueryAndSubqueries(this._errorContext, query, this._featureSwitchProvider, annotations, false);
			TransformQueryValidator.ValidateQueryBinding(query, binding, this._errorContext);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001FF5A File Offset: 0x0001E15A
		public static void ValidateQuery(DataShapeGenerationErrorContext errorContext, ResolvedQueryDefinition topLevelQuery, IFeatureSwitchProvider featureSwitchProvider, SemanticQueryDataShapeAnnotations annotations)
		{
			TransformQueryValidator.ValidateQueryAndSubqueries(errorContext, topLevelQuery, featureSwitchProvider, annotations, true);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001FF68 File Offset: 0x0001E168
		private static void ValidateQueryAndSubqueries(DataShapeGenerationErrorContext errorContext, ResolvedQueryDefinition topLevelQuery, IFeatureSwitchProvider featureSwitchProvider, SemanticQueryDataShapeAnnotations annotations, bool validateHasMixedReferences)
		{
			TransformQueryValidator.ValidateQuery(errorContext, topLevelQuery, featureSwitchProvider, annotations, false, validateHasMixedReferences);
			IEnumerable<ResolvedQueryDefinition> values = annotations.QueryDefinitionByName.Values;
			Func<ResolvedQueryDefinition, bool> <>9__0;
			Func<ResolvedQueryDefinition, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (ResolvedQueryDefinition query) => query != topLevelQuery);
			}
			foreach (ResolvedQueryDefinition resolvedQueryDefinition in values.Where(func))
			{
				TransformQueryValidator.ValidateQuery(errorContext, resolvedQueryDefinition, featureSwitchProvider, annotations, true, true);
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00020000 File Offset: 0x0001E200
		private static void ValidateQuery(DataShapeGenerationErrorContext errorContext, ResolvedQueryDefinition query, IFeatureSwitchProvider featureSwitchProvider, SemanticQueryDataShapeAnnotations annotations, bool isSubquery, bool validateHasMixedReferences)
		{
			TransformQueryValidator.ValidateFiltersReferencingQueriesWithTransforms(query, errorContext, annotations);
			if (query.Transform.IsNullOrEmpty<ResolvedQueryTransform>())
			{
				return;
			}
			if (isSubquery && !featureSwitchProvider.IsEnabled(FeatureSwitchKind.SubqueryTransform))
			{
				errorContext.Register(DataShapeGenerationMessages.TransformInsideSubquery(EngineMessageSeverity.Error));
				return;
			}
			if (!query.GroupBy.IsNullOrEmpty<ResolvedQueryExpression>())
			{
				errorContext.Register(DataShapeGenerationMessages.TransformAndGroupBy(EngineMessageSeverity.Error));
				return;
			}
			if (annotations.QueryHasVisualCalculationsExpressions(query.Name))
			{
				errorContext.Register(DataShapeGenerationMessages.UnsupportedVisualCalculationWithTransforms(EngineMessageSeverity.Error));
			}
			if (validateHasMixedReferences && TransformQueryValidator.HasMixedReferences(query))
			{
				errorContext.Register(DataShapeGenerationMessages.TransformWithSchemaOrSubqueryReferenceInSameScope(EngineMessageSeverity.Error));
			}
			TransformQueryValidator.ValidateTransformsWithSubqueries(query, errorContext, featureSwitchProvider, annotations);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00020094 File Offset: 0x0001E294
		private static void ValidateQueryBinding(ResolvedQueryDefinition query, DataShapeBinding binding, DataShapeGenerationErrorContext errorContext)
		{
			if (query.Transform.IsNullOrEmpty<ResolvedQueryTransform>())
			{
				return;
			}
			if (binding == null)
			{
				errorContext.Register(DataShapeGenerationMessages.TransformWithoutDataShapeBinding(EngineMessageSeverity.Error));
				return;
			}
			if (binding.Secondary != null)
			{
				errorContext.Register(DataShapeGenerationMessages.TransformAndSecondaryGroup(EngineMessageSeverity.Error));
			}
			if (binding.Primary.HasSubtotals() || binding.Secondary.HasSubtotals())
			{
				errorContext.Register(DataShapeGenerationMessages.TransformAndSubtotal(EngineMessageSeverity.Error));
			}
			if (binding.Primary.HasSuppressedProjections() || binding.Secondary.HasSuppressedProjections())
			{
				errorContext.Register(DataShapeGenerationMessages.TransformAndSuppressedProjections(EngineMessageSeverity.Error));
			}
			bool? flag = null;
			bool? flag2 = null;
			bool? flag3 = null;
			if (TransformQueryValidator.HasMixedReferences(query, binding.Primary, ref flag) || TransformQueryValidator.HasMixedReferences(query, binding.Secondary, ref flag2) || TransformQueryValidator.HasMixedReferences(query, binding.Projections, ref flag3))
			{
				errorContext.Register(DataShapeGenerationMessages.TransformWithSchemaOrSubqueryReferenceInSameScope(EngineMessageSeverity.Error));
			}
			if (TransformQueryValidator.HasMultipleTrue(new bool?[] { flag, flag2, flag3 }))
			{
				errorContext.Register(DataShapeGenerationMessages.TransformReferencedFromMultipleScopes(EngineMessageSeverity.Error));
			}
			if (!binding.Highlights.IsNullOrEmptyCollection<FilterDefinition>())
			{
				errorContext.Register(DataShapeGenerationMessages.TransformAndHighlight(EngineMessageSeverity.Error));
			}
			if (binding.Primary.HasInstanceFilters() || binding.Secondary.HasInstanceFilters())
			{
				errorContext.Register(DataShapeGenerationMessages.TransformAndInstanceFilter(EngineMessageSeverity.Error));
			}
			if (binding.Primary.HasScopedAggregates() || binding.Secondary.HasScopedAggregates())
			{
				errorContext.Register(DataShapeGenerationMessages.TransformAndScopedAggregates(EngineMessageSeverity.Error));
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00020218 File Offset: 0x0001E418
		private static void ValidateTransformsWithSubqueries(ResolvedQueryDefinition query, DataShapeGenerationErrorContext errorContext, IFeatureSwitchProvider featureSwitchProvider, SemanticQueryDataShapeAnnotations annotations)
		{
			int expressionSourceForRegroupingCount = annotations.GetExpressionSourceForRegroupingCount(query);
			if (expressionSourceForRegroupingCount == 0)
			{
				return;
			}
			if (!featureSwitchProvider.IsEnabled(FeatureSwitchKind.TransformRefersSubquery))
			{
				errorContext.Register(DataShapeGenerationMessages.UnsupportedSubqueryReferenceByTransform(EngineMessageSeverity.Error));
				return;
			}
			if (expressionSourceForRegroupingCount > 1)
			{
				errorContext.Register(DataShapeGenerationMessages.TransformRefersMultipleSubqueries(EngineMessageSeverity.Error));
			}
			foreach (ResolvedQueryTransform resolvedQueryTransform in query.Transform)
			{
				foreach (ResolvedQueryTransformTableColumn resolvedQueryTransformTableColumn in resolvedQueryTransform.Input.Table.Columns)
				{
					ExpressionContext expressionContext = new ExpressionContext(query.Name, SemanticQueryObjectType.TransformColumn, resolvedQueryTransformTableColumn.Name);
					ResolvedQueryExpressionValidator.Validate(resolvedQueryTransformTableColumn.Expression, errorContext, AllowedExpressionContent.TransformSubqueryReferences, expressionContext);
				}
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000202F8 File Offset: 0x0001E4F8
		private static void ValidateFiltersReferencingQueriesWithTransforms(ResolvedQueryDefinition query, DataShapeGenerationErrorContext errorContext, SemanticQueryDataShapeAnnotations annotations)
		{
			if (query.Where.IsNullOrEmpty<ResolvedQueryFilter>())
			{
				return;
			}
			HashSet<ResolvedQueryDefinition> subqueriesUsedInFiltering = annotations.GetSubqueriesUsedInFiltering(query);
			if (subqueriesUsedInFiltering.IsNullOrEmpty<ResolvedQueryDefinition>())
			{
				return;
			}
			foreach (ResolvedQueryDefinition resolvedQueryDefinition in subqueriesUsedInFiltering)
			{
				if (!resolvedQueryDefinition.Transform.IsNullOrEmpty<ResolvedQueryTransform>())
				{
					errorContext.Register(DataShapeGenerationMessages.InFilterReferencingSubqueryWithTransform(EngineMessageSeverity.Error, resolvedQueryDefinition.Name));
					break;
				}
			}
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00020380 File Offset: 0x0001E580
		private static bool HasMultipleTrue(params bool?[] values)
		{
			int num = 0;
			foreach (bool flag in values)
			{
				if (flag.GetValueOrDefault())
				{
					num++;
				}
			}
			return num > 1;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000203B8 File Offset: 0x0001E5B8
		private static bool HasMixedReferences(ResolvedQueryDefinition query, DataShapeBindingAxis axis, ref bool? hasTransformReference)
		{
			if (axis == null || axis.Groupings.IsNullOrEmptyCollection<DataShapeBindingAxisGrouping>())
			{
				return false;
			}
			foreach (DataShapeBindingAxisGrouping dataShapeBindingAxisGrouping in axis.Groupings)
			{
				if (TransformQueryValidator.HasMixedReferences(query, dataShapeBindingAxisGrouping.Projections, ref hasTransformReference))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0002042C File Offset: 0x0001E62C
		private static bool HasMixedReferences(ResolvedQueryDefinition query)
		{
			bool? flag = null;
			return TransformQueryValidator.HasMixedReferences(query, Enumerable.Range(0, query.Select.Count).Evaluate<int>(), ref flag);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00020460 File Offset: 0x0001E660
		private static bool HasMixedReferences(ResolvedQueryDefinition query, IList<int> projections, ref bool? hasTransformReference)
		{
			if (projections == null)
			{
				return false;
			}
			foreach (int num in projections)
			{
				bool flag = TransformQueryValidator.HasTransformReference(query.Select[num].Expression);
				if (hasTransformReference == null)
				{
					hasTransformReference = new bool?(flag);
				}
				else if (hasTransformReference.Value != flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000204E4 File Offset: 0x0001E6E4
		private static bool HasTransformReference(ResolvedQueryExpression expr)
		{
			return expr is ResolvedQueryTransformTableColumnExpression;
		}

		// Token: 0x04000432 RID: 1074
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x04000433 RID: 1075
		private readonly IFeatureSwitchProvider _featureSwitchProvider;
	}
}
