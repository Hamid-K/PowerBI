using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryTranslation;

namespace Microsoft.DataShaping.SemanticQueryTranslation.TranslateQuery
{
	// Token: 0x0200001B RID: 27
	internal static class TranslateQueryCommandValidator
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x0000504B File Offset: 0x0000324B
		internal static bool TryValidateCommand(TranslateQueryCommand command, SemanticQueryTranslatorContext context)
		{
			return (!(command.Binding != null) || TranslateQueryCommandValidator.TryValidateBinding(command.Binding, context)) && TranslateQueryCommandValidator.TryValidateQuery(command.Query, context);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005078 File Offset: 0x00003278
		private static bool TryValidateQuery(QueryDefinition query, SemanticQueryTranslatorContext context)
		{
			if (query == null)
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.InvalidSemanticQueryError(EngineMessageSeverity.Error));
				return false;
			}
			if (!query.Parameters.IsNullOrEmpty<QueryExpressionContainer>() && !context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.DSESemanticQueryParameters))
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity.Error, "query parameters"));
				return false;
			}
			return SemanticQueryTranslationUtils.TryValidateQuery(query, context);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000050DC File Offset: 0x000032DC
		private static bool TryValidateBinding(DataShapeBinding binding, SemanticQueryTranslatorContext context)
		{
			if (binding.Secondary != null)
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity.Error, "Binding.Secondary"));
				return false;
			}
			if (binding.Aggregates != null)
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity.Error, "Binding.Aggregates"));
				return false;
			}
			if (binding.Projections != null)
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity.Error, "Binding.Projections"));
				return false;
			}
			if (binding.OrderBy != null)
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity.Error, "Binding.OrderBy"));
				return false;
			}
			if (binding.Limits != null)
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity.Error, "Binding.Limits"));
				return false;
			}
			if (binding.Highlights != null)
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity.Error, "Binding.Highlights"));
				return false;
			}
			if (binding.DataReduction != null)
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity.Error, "Binding.DataReduction"));
				return false;
			}
			if (binding.SuppressedJoinPredicates != null)
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity.Error, "Binding.SuppressedJoinPredicates"));
				return false;
			}
			return TranslateQueryCommandValidator.TryValidateBindingAxis(binding.Primary, context);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005208 File Offset: 0x00003408
		private static bool TryValidateBindingAxis(DataShapeBindingAxis axis, SemanticQueryTranslatorContext context)
		{
			if (axis == null)
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.InvalidSemanticQueryError(EngineMessageSeverity.Error));
				return false;
			}
			if (axis.Expansion != null)
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity.Error, "Binding.Axis.Expansion"));
				return false;
			}
			if (axis.Synchronization != null)
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity.Error, "Binding.Axis.Synchronization"));
				return false;
			}
			return TranslateQueryCommandValidator.TryValidateBindingGroupings(axis.Groupings, context);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005284 File Offset: 0x00003484
		private static bool TryValidateBindingGroupings(IEnumerable<DataShapeBindingAxisGrouping> groupings, SemanticQueryTranslatorContext context)
		{
			if (groupings == null || !groupings.Any<DataShapeBindingAxisGrouping>())
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.InvalidSemanticQueryError(EngineMessageSeverity.Error));
				return false;
			}
			foreach (DataShapeBindingAxisGrouping dataShapeBindingAxisGrouping in groupings)
			{
				if (dataShapeBindingAxisGrouping.SuppressedProjections != null)
				{
					context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity.Error, "Binding.Axis.Grouping.SuppressedProjections"));
					return false;
				}
				if (dataShapeBindingAxisGrouping.InstanceFilters != null)
				{
					context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity.Error, "Binding.Axis.Grouping.InstanceFilters"));
					return false;
				}
				if (dataShapeBindingAxisGrouping.Aggregates != null)
				{
					context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity.Error, "Binding.Axis.Grouping.Aggregates"));
					return false;
				}
				if (dataShapeBindingAxisGrouping.ShowItemsWithNoData != null)
				{
					context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity.Error, "Binding.Axis.Grouping.ShowItemsWithNoData"));
					return false;
				}
				if (dataShapeBindingAxisGrouping.Subtotal != null)
				{
					SubtotalType? subtotal = dataShapeBindingAxisGrouping.Subtotal;
					SubtotalType subtotalType = SubtotalType.None;
					if (!((subtotal.GetValueOrDefault() == subtotalType) & (subtotal != null)) && !context.Schema.GetDaxCapabilitiesAnnotation().DaxFunctions.SupportsSummarizeColumns)
					{
						context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedFeatureInTranslateQueryCommandForModel(EngineMessageSeverity.Error, "subtotals"));
						return false;
					}
				}
			}
			return true;
		}
	}
}
