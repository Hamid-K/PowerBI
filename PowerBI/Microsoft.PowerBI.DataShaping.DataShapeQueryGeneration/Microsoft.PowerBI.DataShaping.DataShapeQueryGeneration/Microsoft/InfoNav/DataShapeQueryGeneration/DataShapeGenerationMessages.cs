using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Utils;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200004E RID: 78
	internal static class DataShapeGenerationMessages
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0000B099 File Offset: 0x00009299
		public static DataShapeGenerationMessage CannotProcessSelectExpression(EngineMessageSeverity severity, int expressionIndex)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CannotProcessSelectExpression, "Expression {0} could not be translated into a DataShapeGroup or Measure expression.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { expressionIndex });
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000B0BB File Offset: 0x000092BB
		public static DataShapeGenerationMessage CouldNotApplyAdditionalODataFilter(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotApplyAdditionalODataFilter, "Could not apply additional OData filter due to an unknown error.", severity, ErrorSourceCategory.UserInput, Array.Empty<object>());
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000B0D3 File Offset: 0x000092D3
		public static DataShapeGenerationMessage CouldNotConvertLiteral(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotConvertLiteralExpression, "Could not convert literal to value when constructing MParameter.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000B0EB File Offset: 0x000092EB
		public static DataShapeGenerationMessage CouldNotCreateLimit(EngineMessageSeverity severity, string dataReductionHierarchy)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotCreateLimit, "Could not create limit for {0} data reduction.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { dataReductionHierarchy });
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000B108 File Offset: 0x00009308
		public static DataShapeGenerationMessage CouldNotInterpretAlgorithm(EngineMessageSeverity severity, string dataReductionSlot)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotInterpretAlgorithm, "Could not interpret algorithm for {0} data reduction.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { dataReductionSlot });
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000B125 File Offset: 0x00009325
		public static DataShapeGenerationMessage CouldNotNormalizeDataShapeBinding(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotNormalizeDataShapeBinding, "Could not normalize DataShapeBinding due to an unknown error.", severity, ErrorSourceCategory.FallbackCondition, Array.Empty<object>());
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000B13D File Offset: 0x0000933D
		public static DataShapeGenerationMessage CouldNotResolveDataShapeBinding(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotResolveDataShapeBinding, "Could not normalize DataShapeBinding due to an unknown error.", severity, ErrorSourceCategory.FallbackCondition, Array.Empty<object>());
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000B155 File Offset: 0x00009355
		internal static DataShapeGenerationMessage CouldNotResolveLetReference(EngineMessageSeverity severity, string letName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotResolveLetReference, "The query contains an invalid reference to let name '{0}'.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { letName.MarkAsModelInfo() });
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000B178 File Offset: 0x00009378
		public static DataShapeGenerationMessage CouldNotResolveModelReferencesInSemanticQuery(EngineMessageSeverity severity, IContainsTelemetryMarkup[] unresolvedModelReferences)
		{
			string localizedTemplate = DataShapeGenerationMessages.GetLocalizedTemplate(DataShapeGenerationErrorCode.CouldNotResolveModelReferencesInSemanticQuery);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotResolveModelReferencesInSemanticQuery, localizedTemplate, unresolvedModelReferences, severity, ErrorSourceCategory.InputDoesNotMatchModel, Array.Empty<object>());
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000B1A4 File Offset: 0x000093A4
		public static DataShapeGenerationMessage CouldNotResolveModelReferencesInQueryExtensionSchema(EngineMessageSeverity severity, IContainsTelemetryMarkup[] unresolvedModelReferences)
		{
			string localizedTemplate = DataShapeGenerationMessages.GetLocalizedTemplate(DataShapeGenerationErrorCode.CouldNotResolveModelReferencesInQueryExtensionSchema);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotResolveModelReferencesInQueryExtensionSchema, localizedTemplate, unresolvedModelReferences, severity, ErrorSourceCategory.InputDoesNotMatchModel, Array.Empty<object>());
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000B1CD File Offset: 0x000093CD
		internal static DataShapeGenerationMessage CouldNotResolveQueryExtensionSchema(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotResolveQueryExtensionSchema, "Could not resolve QueryExtensionSchema due to an unknown error.", severity, ErrorSourceCategory.FallbackCondition, Array.Empty<object>());
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000B1E6 File Offset: 0x000093E6
		internal static DataShapeGenerationMessage ExpressionToExtensionSchemaItemRewriterFailed(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ExpressionToExtensionSchemaItemRewriterFailed, "Could not rewrite expression to extension schema due to an unknown error.", severity, ErrorSourceCategory.UnexpectedError, Array.Empty<object>());
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000B200 File Offset: 0x00009400
		internal static DataShapeGenerationMessage CouldNotRewriteExpressionToExtensionSchemaItemCommand(string message, EngineMessageSeverity severity, ErrorSource source, string[] affectedItems)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotRewriteExpressionToExtensionSchemaItemCommand, message, severity, source, affectedItems);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000B21A File Offset: 0x0000941A
		internal static DataShapeGenerationMessage CouldNotResolveQueryParameterReference(EngineMessageSeverity severity, string parameterName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotResolveQueryParameterReference, "The query contains an invalid reference to query parameter name '{0}'.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { parameterName.MarkAsModelInfo() });
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000B23D File Offset: 0x0000943D
		internal static DataShapeGenerationMessage CouldNotResolveQuerySelectName(EngineMessageSeverity severity, string selectName, string sourceName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotResolveQuerySelectName, "Could not resolve SelectName '{0}' from query '{1}'.", severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				selectName.MarkAsModelInfo(),
				sourceName.MarkAsModelInfo()
			});
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000B269 File Offset: 0x00009469
		internal static DataShapeGenerationMessage CouldNotResolveSourceReference(EngineMessageSeverity severity, string sourceName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotResolveSourceReference, "The query contains an invalid reference to source name '{0}'.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { sourceName.MarkAsModelInfo() });
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000B28C File Offset: 0x0000948C
		internal static DataShapeGenerationMessage CouldNotResolveSourceNameOnSuppressedJoinPredicateByName(EngineMessageSeverity severity, string sourceName, string expressionReference)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotResolveSuppressedJoinPredicateSourceName, "The query contains a Source Name on Suppressed Join Predicate By Name that could not be resolved. Source Name '{0}' Expression Reference '{1}'.", severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				sourceName.MarkAsModelInfo(),
				expressionReference.MarkAsModelInfo()
			});
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000B2B8 File Offset: 0x000094B8
		internal static DataShapeGenerationMessage CouldNotResolveExpressionReferenceOnSuppressedJoinPredicateByName(EngineMessageSeverity severity, string sourceName, string expressionReference)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotResolveSuppressedJoinPredicateExpressionName, "The query contains an Expression Name on Suppressed Join Predicate By Name that could not be resolved. Source Name '{0}' Expression Reference '{1}'.", severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				sourceName.MarkAsModelInfo(),
				expressionReference.MarkAsModelInfo()
			});
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000B2E4 File Offset: 0x000094E4
		internal static DataShapeGenerationMessage CouldNotResolveExpressionReferenceOnHiddenProjection(EngineMessageSeverity severity, string sourceName, string expressionReference)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotResolveHiddenProjectionExpressionName, "The query contains an Expression Name on a Hidden Projection that could not be resolved. Source Name '{0}' Expression Reference '{1}'.", severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				sourceName.MarkAsModelInfo(),
				expressionReference.MarkAsModelInfo()
			});
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000B30F File Offset: 0x0000950F
		internal static DataShapeGenerationMessage HiddenProjectionSourceNameMustBeTheRootQuery(EngineMessageSeverity severity, string sourceName, string expressionReference)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.HiddenProjectionSourceNameMustBeNull, "The query contains a Source Name on a Hidden Projection that is not the root query. Hidden projections can only target the root query. Source Name '{0}' Expression Reference '{1}'.", severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				sourceName.MarkAsModelInfo(),
				expressionReference.MarkAsModelInfo()
			});
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000B33B File Offset: 0x0000953B
		internal static DataShapeGenerationMessage InvalidAggregateOfHiddenProjection(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidAggregateOfHiddenProjection, "The query contains an aggregate of a Hidden Projection, which is not allowed.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000B354 File Offset: 0x00009554
		internal static DataShapeGenerationMessage CouldNotResolveTargetScopeForVisualAxis(EngineMessageSeverity severity, string visualAxisName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotResolveTargetScopeForVisualAxis, "Could not resolve target scope for visual axis {0}.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { visualAxisName });
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000B372 File Offset: 0x00009572
		public static DataShapeGenerationMessage CouldNotUpgradeDataShapeBinding(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotUpgradeDataShapeBinding, "Could not upgrade DataShapeBinding to the target version due to an unknown error.", severity, ErrorSourceCategory.FallbackCondition, Array.Empty<object>());
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000B38B File Offset: 0x0000958B
		public static DataShapeGenerationMessage CouldNotUpgradeSemanticQueryDefinition(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.CouldNotUpgradeSemanticQueryDefinition, "Could not upgrade QueryDefinition to the target version due to an unknown error.", severity, ErrorSourceCategory.FallbackCondition, Array.Empty<object>());
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000B3A4 File Offset: 0x000095A4
		public static DataShapeGenerationMessage DifferentContextDataShapeFilterTargets(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.DifferentContextDataShapeFilterTargets, "Could not upgrade QueryDefinition to the target version due to an unknown error.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000B3BD File Offset: 0x000095BD
		public static DataShapeGenerationMessage TooManyScopedDataReductions(EngineMessageSeverity severity, int actualCount, int scopedDataReductionMax)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.TooManyScopedDataReductions, "The query contains {0} ScopedDataReductions. The maximum number of ScopedDataReduction in a query is {1}.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { actualCount, scopedDataReductionMax });
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000B3EC File Offset: 0x000095EC
		public static DataShapeGenerationMessage ExtensionEntityNameDoesNotMatchExtends(EngineMessageSeverity severity, string name, string extends)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ExtensionEntityNameDoesNotMatchExtends, "The extension entity Name '{0}' does not match the Extends '{1}'. An extension entity must use the same name as the table it is extending in the underlying model.", severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				name.MarkAsModelInfo(),
				extends.MarkAsModelInfo()
			});
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000B418 File Offset: 0x00009618
		public static DataShapeGenerationMessage ExtensionEntityNameNotUnique(EngineMessageSeverity severity, string schemaName, string entityName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ExtensionEntityNameNotUnique, "The extension entity '{0}' is already defined. Extension entity names must not be the same as any other extension entity. Please rename the extension entity to have a unique name.", severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				schemaName.MarkAsModelInfo(),
				entityName.MarkAsModelInfo()
			});
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000B444 File Offset: 0x00009644
		public static DataShapeGenerationMessage ExtensionMeasureEmptyExpression(EngineMessageSeverity severity, string schemaName, string measureName, string entityName)
		{
			string localizedTemplate = DataShapeGenerationMessages.GetLocalizedTemplate(DataShapeGenerationErrorCode.ExtensionMeasureEmptyExpression);
			IContainsTelemetryMarkup[] array = DataShapeGenerationMessages.CreateAffectedItems(schemaName, entityName, measureName);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ExtensionMeasureEmptyExpression, localizedTemplate, array, severity, ErrorSourceCategory.UserInput, new object[]
			{
				measureName.MarkAsModelInfo(),
				entityName.MarkAsModelInfo()
			});
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000B48C File Offset: 0x0000968C
		public static DataShapeGenerationMessage ExtensionColumnEmptyExpression(EngineMessageSeverity severity, string schemaName, string columnName, string entityName)
		{
			string localizedTemplate = DataShapeGenerationMessages.GetLocalizedTemplate(DataShapeGenerationErrorCode.ExtensionColumnEmptyExpression);
			IContainsTelemetryMarkup[] array = DataShapeGenerationMessages.CreateAffectedItems(schemaName, entityName, columnName);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ExtensionColumnEmptyExpression, localizedTemplate, array, severity, ErrorSourceCategory.UserInput, new object[]
			{
				columnName.MarkAsModelInfo(),
				entityName.MarkAsModelInfo()
			});
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000B4D4 File Offset: 0x000096D4
		public static DataShapeGenerationMessage ExtensionMeasureNameNotUniqueModel(EngineMessageSeverity severity, string extensionSchemaName, string extensionMeasureName, string extensionMeasureEntity, string otherMeasureName, string otherMeasureEntity)
		{
			string localizedTemplate = DataShapeGenerationMessages.GetLocalizedTemplate(DataShapeGenerationErrorCode.ExtensionMeasureNameNotUniqueModel);
			IContainsTelemetryMarkup[] array = DataShapeGenerationMessages.CreateAffectedItems(extensionSchemaName, extensionMeasureEntity, extensionMeasureName);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ExtensionMeasureNameNotUniqueModel, localizedTemplate, array, severity, ErrorSourceCategory.InputDoesNotMatchModel, new object[]
			{
				extensionMeasureName.MarkAsModelInfo(),
				extensionMeasureEntity.MarkAsModelInfo(),
				otherMeasureName.MarkAsModelInfo(),
				otherMeasureEntity.MarkAsModelInfo()
			});
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000B530 File Offset: 0x00009730
		public static DataShapeGenerationMessage ExtensionColumnNameNotUniqueModel(EngineMessageSeverity severity, string extensionSchemaName, string extensionColumnName, string extensionColumnEntity, string otherColumnName, string otherColumnEntity)
		{
			string localizedTemplate = DataShapeGenerationMessages.GetLocalizedTemplate(DataShapeGenerationErrorCode.ExtensionColumnNameNotUniqueModel);
			IContainsTelemetryMarkup[] array = DataShapeGenerationMessages.CreateAffectedItems(extensionSchemaName, extensionColumnEntity, extensionColumnName);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ExtensionColumnNameNotUniqueModel, localizedTemplate, array, severity, ErrorSourceCategory.InputDoesNotMatchModel, new object[]
			{
				extensionColumnName.MarkAsModelInfo(),
				extensionColumnEntity.MarkAsModelInfo(),
				otherColumnName.MarkAsModelInfo(),
				otherColumnEntity.MarkAsModelInfo()
			});
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000B58C File Offset: 0x0000978C
		public static DataShapeGenerationMessage ExtensionMeasureNameNotUnique(EngineMessageSeverity severity, string extensionSchemaName, string extensionMeasureName, string extensionMeasureEntity, string otherMeasureName, string otherMeasureEntity)
		{
			string localizedTemplate = DataShapeGenerationMessages.GetLocalizedTemplate(DataShapeGenerationErrorCode.ExtensionMeasureNameNotUnique);
			IContainsTelemetryMarkup[] array = DataShapeGenerationMessages.CreateAffectedItems(extensionSchemaName, extensionMeasureEntity, extensionMeasureName);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ExtensionMeasureNameNotUnique, localizedTemplate, array, severity, ErrorSourceCategory.InputDoesNotMatchModel, new object[]
			{
				extensionMeasureName.MarkAsModelInfo(),
				extensionMeasureEntity.MarkAsModelInfo(),
				otherMeasureName.MarkAsModelInfo(),
				otherMeasureEntity.MarkAsModelInfo()
			});
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000B5E8 File Offset: 0x000097E8
		public static DataShapeGenerationMessage ExtensionMeasureNameNotUniqueFlexible(EngineMessageSeverity severity, string extensionSchemaName, string extensionMeasureName, string extensionMeasureEntity)
		{
			IContainsTelemetryMarkup[] array = DataShapeGenerationMessages.CreateAffectedItems(extensionSchemaName, extensionMeasureEntity, extensionMeasureName);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ExtensionMeasureNameNotUniqueFlexible, "Multiple extension measures on entity '{1}' have the same name '{0}'.Extension measure names must not be the same as any other extension measure in the same entity.Please rename the extension measure to have a unique name.", array, severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				extensionMeasureName.MarkAsModelInfo(),
				extensionMeasureEntity.MarkAsModelInfo()
			});
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000B62C File Offset: 0x0000982C
		public static DataShapeGenerationMessage ExtensionColumnNameNotUnique(EngineMessageSeverity severity, string extensionSchemaName, string extensionColumnName, string extensionColumnEntity, string otherColumnName, string otherColumnEntity)
		{
			string localizedTemplate = DataShapeGenerationMessages.GetLocalizedTemplate(DataShapeGenerationErrorCode.ExtensionColumnNameNotUnique);
			IContainsTelemetryMarkup[] array = DataShapeGenerationMessages.CreateAffectedItems(extensionSchemaName, extensionColumnEntity, extensionColumnName);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ExtensionColumnNameNotUnique, localizedTemplate, array, severity, ErrorSourceCategory.InputDoesNotMatchModel, new object[]
			{
				extensionColumnName.MarkAsModelInfo(),
				extensionColumnEntity.MarkAsModelInfo(),
				otherColumnName.MarkAsModelInfo(),
				otherColumnEntity.MarkAsModelInfo()
			});
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000B688 File Offset: 0x00009888
		public static DataShapeGenerationMessage ExtensionColumnNameNotUniqueFlexible(EngineMessageSeverity severity, string extensionSchemaName, string extensionColumnName, string extensionColumnEntity)
		{
			IContainsTelemetryMarkup[] array = DataShapeGenerationMessages.CreateAffectedItems(extensionSchemaName, extensionColumnEntity, extensionColumnName);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ExtensionColumnNameNotUniqueFlexible, "Multiple extension columns on entity '{1}' have the same name '{0}'. Extension column names must not be the same as any other extension column in the same entity. Please rename the extension column to have a unique name.", array, severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				extensionColumnName.MarkAsModelInfo(),
				extensionColumnEntity.MarkAsModelInfo()
			});
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000B6C9 File Offset: 0x000098C9
		public static DataShapeGenerationMessage ExtensionSchemaNameMatchesBaseEntityContainer(EngineMessageSeverity severity, string name)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ExtensionSchemaNameMatchesBaseEntityContainer, "The QueryExtensionSchema Name '{0}' is the same as the base schema EntityContainer Name. The extension schema name must not be the same as the base entity container name.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { name });
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000B6E8 File Offset: 0x000098E8
		public static DataShapeGenerationMessage ExtensionColumnAndMeasureNamesNotUnique(EngineMessageSeverity severity, string extensionSchemaName, string extensionPropertyName, string extensionPropertyEntity, string propertyType, string otherPropertyType)
		{
			string localizedTemplate = DataShapeGenerationMessages.GetLocalizedTemplate(DataShapeGenerationErrorCode.ExtensionColumnAndMeasureNamesNotUnique);
			IContainsTelemetryMarkup[] array = DataShapeGenerationMessages.CreateAffectedItems(extensionSchemaName, extensionPropertyName, extensionPropertyEntity);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ExtensionColumnAndMeasureNamesNotUnique, localizedTemplate, array, severity, ErrorSourceCategory.InputDoesNotMatchModel, new object[]
			{
				extensionPropertyName.MarkAsModelInfo(),
				extensionPropertyEntity.MarkAsModelInfo(),
				propertyType,
				otherPropertyType
			});
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000B737 File Offset: 0x00009937
		public static DataShapeGenerationMessage FlexibleExtensionSchemaWithEmptyModel(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.FlexibleExtensionSchemaWithEmptyModel, "The query contains a QueryExtensionSchema with a flexibly named property against an empty base model. The base model should contain at least one entity to which the flexibly named property can be attached.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000B750 File Offset: 0x00009950
		public static DataShapeGenerationMessage FlexibleExtensionEntityShouldNotHaveExtendsProperty(EngineMessageSeverity severity, string extensionEntity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.FlexibleExtensionEntityShouldNotHaveExtendsProperty, "The query contains a QueryExtensionSchema with a flexibly named entity that has extends property set. The flexibly named entity should not have the extends property set.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { extensionEntity.MarkAsModelInfo() });
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000B773 File Offset: 0x00009973
		public static DataShapeGenerationMessage FlexibleExtensionEntityCanNotHaveColumns(EngineMessageSeverity severity, string extensionEntity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.FlexibleExtensionEntityCanNotHaveColumns, "The query contains a QueryExtensionSchema with a flexibly named entity that has columns. The flexibly named entity can not have columns.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { extensionEntity.MarkAsModelInfo() });
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000B798 File Offset: 0x00009998
		public static DataShapeGenerationMessage FlexibleExtensionColumnWithNoExtensionEntityExtendsProperty(EngineMessageSeverity severity, string extensionSchemaName, string extensionColumnName, string extensionColumnEntity)
		{
			IContainsTelemetryMarkup[] array = DataShapeGenerationMessages.CreateAffectedItems(extensionSchemaName, extensionColumnEntity, extensionColumnName);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.FlexibleExtensionColumnWithNoExtensionEntityExtendsProperty, "The query contains a QueryExtensionSchema with a flexibly named column on an extension entity that does not specify the extends property. The extension entity should extend an entity.", array, severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				extensionColumnName.MarkAsModelInfo(),
				extensionColumnEntity.MarkAsModelInfo()
			});
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000B7D9 File Offset: 0x000099D9
		public static DataShapeGenerationMessage GroupByAndSubtotal(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.GroupByAndSubtotal, "The query uses both a subtotal and a GroupBy. These features may not be combined in the same query.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000B7F2 File Offset: 0x000099F2
		public static DataShapeGenerationMessage GroupByAndSubqueryReferenceInSelect(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.GroupByAndSubqueryReferenceInSelect, "The query contains a GroupBy and refers to a subquery in a Select item. These features may not be combined in the same query.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000B80B File Offset: 0x00009A0B
		public static DataShapeGenerationMessage GroupByNotSupportedWithScopedDataReduction(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.GroupByNotSupportedWithScopedDataReduction, "GroupBy is not supported with scoped data reduction.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000B824 File Offset: 0x00009A24
		public static DataShapeGenerationMessage GroupByWithoutColumnSelect(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.GroupByWithoutColumnSelect, "The query contains a GroupBy but no Select refers to a valid column or hierarchy level. GroupBy requires at least one selected column.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000B83D File Offset: 0x00009A3D
		public static DataShapeGenerationMessage UnsupportedSubqueryReferenceByTransform(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedSubqueryReferenceByTransform, "The query contains a transform input table that refers to a subquery. The feature switch is off for this feature.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000B859 File Offset: 0x00009A59
		public static DataShapeGenerationMessage TransformRefersMultipleSubqueries(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.TransformRefersMultipleSubqueries, "The query contains a transform input table that refers to more than one subquery. This is not supported.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000B878 File Offset: 0x00009A78
		public static DataShapeGenerationMessage ParameterMappingFilterConflict(EngineMessageSeverity severity, IContainsTelemetryMarkup[] parameterName)
		{
			string localizedTemplate = DataShapeGenerationMessages.GetLocalizedTemplate(DataShapeGenerationErrorCode.ParameterMappingFilterConflict);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ParameterMappingFilterConflict, localizedTemplate, parameterName, severity, ErrorSourceCategory.UserInput, Array.Empty<object>());
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000B8A1 File Offset: 0x00009AA1
		public static DataShapeGenerationMessage InvalidFromSourceItemWithSubqueryRegrouping(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidFromSourceItemWithSubqueryRegrouping, "The query refers to a subquery in a Select item and has invalid From item. Only subquery source expressions are allowed in From clause.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000B8BA File Offset: 0x00009ABA
		public static DataShapeGenerationMessage InvalidFromSourceExpressionWithSubqueryRegrouping(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidFromSourceExpressionWithSubqueryRegrouping, "The query refers to a subquery in a Select item and has invalid From Expression. Only subquery source expressions are allowed in From clause. Any other expressions are not supported with subquery regrouping.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B8D3 File Offset: 0x00009AD3
		public static DataShapeGenerationMessage InvalidWhereItemWithSubqueryAggregation(EngineMessageSeverity severity, DataShapeGenerationMessagePhrase reason)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidWhereItemWithSubqueryAggregation, "The query refers to multi-subqueries in Select clause and has an invalid Where filter. {0}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { reason });
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B8F1 File Offset: 0x00009AF1
		public static DataShapeGenerationMessage InvalidDataReductionAlgorithmCount(EngineMessageSeverity severity, int count)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidDataReductionAlgorithmCount, "The query contains a DataReductionAlgorithm with a Count of {0}. When specified Count must be greater than zero. The Count will be ignored.", severity, ErrorSourceCategory.UserInput, new object[] { count });
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B914 File Offset: 0x00009B14
		public static DataShapeGenerationMessage InvalidDataShapeBindingWithSubqueryRegrouping(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidDataShapeBindingWithSubqueryRegrouping, "The query contains Highlights in the DataShapeBinding and refers to a subquery in a Select item. These features may not be combined in the same query.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B92D File Offset: 0x00009B2D
		public static DataShapeGenerationMessage InvalidDataShapeBindingAxisWithSubqueryRegrouping(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidDataShapeBindingAxisWithSubqueryRegrouping, "The query contains either Expansion or invalid Groupings in the DataShapeBinding axis and refers to a subquery in a Select item. These features may not be combined in the same query.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B946 File Offset: 0x00009B46
		public static DataShapeGenerationMessage InvalidDataShapeBindingAggregate(EngineMessageSeverity severity, DataShapeGenerationMessagePhrase reason)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidDataShapeBindingAggregate, "The query contains invalid aggregate in the DataShapeBinding. {0}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { reason });
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B964 File Offset: 0x00009B64
		public static DataShapeGenerationMessage InvalidTopNPerLevelDataReduction(EngineMessageSeverity severity, DataShapeGenerationMessagePhrase reason)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidTopNPerLevelDataReduction, "The query contains invalid TopNPerLevel Data Reduction. {0}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { reason });
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B982 File Offset: 0x00009B82
		public static DataShapeGenerationMessage InvalidFilteredEval(EngineMessageSeverity severity, DataShapeGenerationMessagePhrase reason)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidFilteredEval, "The query contains an invalid FilteredEval Expression. {0}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { reason });
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B9A0 File Offset: 0x00009BA0
		public static DataShapeGenerationMessage InvalidExpression(EngineMessageSeverity severity, string owningQueryName, SemanticQueryObjectType objectType, object objectId, DataShapeGenerationMessagePhrase reason)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidExpression, "The expression on {1} {2} of query definition '{0}' contains an error. {3}", severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				new ScrubbedString(owningQueryName),
				objectType,
				objectId,
				reason
			});
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B9D5 File Offset: 0x00009BD5
		public static DataShapeGenerationMessage SingleValueParameterWithMultipleValues(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.SingleValueParameterWithMultipleValues, "The query contains filters that specify multiple values for a column mapped to a single value parameter. Columns mapped to single value parameters can be assigned at most one value.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B9F1 File Offset: 0x00009BF1
		public static DataShapeGenerationMessage InvalidSingleValueAggregation(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidSingleValueAggregation, "The query contains an unsupported SingleValue Aggregation Expression.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000BA0A File Offset: 0x00009C0A
		public static DataShapeGenerationMessage UnsupportedSubqueryRegrouping(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedSubqueryRegrouping, "The query contains an unsuported Subquery Regrouping. Either the model capabilities do not support subquery regrouping or the feature switch is off.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000BA26 File Offset: 0x00009C26
		public static DataShapeGenerationMessage UnsupportedSubqueryUsage(EngineMessageSeverity severity, string parentObjectType, string parentObjectName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedSubqueryUsage, "The query {0} '{1}' is used for both regrouping and filtering.  The Data Shape Engine does not allow a single {0} to be used for both filtering and regrouping in the same query.", severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				parentObjectType,
				parentObjectName.MarkAsModelInfo()
			});
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000BA50 File Offset: 0x00009C50
		public static DataShapeGenerationMessage InvalidNumberOfSubqueries(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidNumberOfSubqueries, "The query contains an invalid number of Subqueries. Having more than one subquery is not supported with subquery regrouping.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000BA69 File Offset: 0x00009C69
		public static DataShapeGenerationMessage UnsupportedSparklineNumberOfPoints(EngineMessageSeverity severity, int maxPointsForSparklineQuery)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedSparklineNumberOfPoints, "The query requested too much data using SparklineData expressions and query grouping. The maximum number of sparkline data points in a single query result is {0}.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { maxPointsForSparklineQuery });
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000BA8F File Offset: 0x00009C8F
		public static DataShapeGenerationMessage IgnoredDataReductionAlgorithm(EngineMessageSeverity severity, string dataReductionHierarchyIgnored)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.IgnoredDataReductionAlgorithm, "A data reduction algorithm was specified but the corresponding grouping does not exist. The data reduction will be ignored for {0}.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { dataReductionHierarchyIgnored });
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000BAB0 File Offset: 0x00009CB0
		public static DataShapeGenerationMessage InvalidBetweenFilterConditionExpression(EngineMessageSeverity severity, IConceptualProperty comparedProperty, ResolvedQueryExpression comparisonValue)
		{
			ScrubbedEntityPropertyReference propertyNameForError = comparedProperty.GetPropertyNameForError();
			DataShapeGenerationErrorCode dataShapeGenerationErrorCode = DataShapeGenerationErrorCode.InvalidBetweenFilterConditionExpression;
			string text = "The query contains a between filter on {0} against an invalid expression '{1}'.";
			IContainsTelemetryMarkup[] array = propertyNameForError.ArrayWrap<ScrubbedEntityPropertyReference>();
			return DataShapeGenerationMessages.CreateMessage(dataShapeGenerationErrorCode, text, array, severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				propertyNameForError.ToCustomerContentString(),
				comparisonValue.ToTraceString()
			});
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		public static DataShapeGenerationMessage InvalidBinaryFilterConditionExpression(EngineMessageSeverity severity, IConceptualProperty comparedProperty, ResolvedQueryExpression comparisonValue)
		{
			ScrubbedEntityPropertyReference propertyNameForError = comparedProperty.GetPropertyNameForError();
			DataShapeGenerationErrorCode dataShapeGenerationErrorCode = DataShapeGenerationErrorCode.InvalidBinaryFilterConditionExpression;
			string text = "The query contains a binary filter on {0} against an invalid expression '{1}'.";
			IContainsTelemetryMarkup[] array = propertyNameForError.ArrayWrap<ScrubbedEntityPropertyReference>();
			return DataShapeGenerationMessages.CreateMessage(dataShapeGenerationErrorCode, text, array, severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				propertyNameForError.ToCustomerContentString(),
				comparisonValue.ToTraceString()
			});
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000BB40 File Offset: 0x00009D40
		public static DataShapeGenerationMessage InvalidFilterComparisonIncompatibleExpressions(EngineMessageSeverity severity, IConceptualProperty comparedProperty, ResolvedQueryExpression comparisonValue)
		{
			string localizedTemplate = DataShapeGenerationMessages.GetLocalizedTemplate(DataShapeGenerationErrorCode.InvalidFilterComparisonIncompatibleExpressions);
			ScrubbedEntityPropertyReference propertyNameForError = comparedProperty.GetPropertyNameForError();
			DataShapeGenerationErrorCode dataShapeGenerationErrorCode = DataShapeGenerationErrorCode.InvalidFilterComparisonIncompatibleExpressions;
			string text = localizedTemplate;
			IContainsTelemetryMarkup[] array = propertyNameForError.ArrayWrap<ScrubbedEntityPropertyReference>();
			return DataShapeGenerationMessages.CreateMessage(dataShapeGenerationErrorCode, text, array, severity, ErrorSourceCategory.UserInput, new object[]
			{
				propertyNameForError.ToCustomerContentString(),
				comparisonValue.ToTraceString()
			});
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000BB8A File Offset: 0x00009D8A
		public static DataShapeGenerationMessage InvalidInTableTypeFilterConditionMultipleEntities(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidInTableTypeFilterConditionMultipleEntities, "The query contains an In filter where Expressions contains columns from multiple entities and the Table refers to a table typed expression. In table filters that do not refer to subqueries may only reference columns from a single entity.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		public static DataShapeGenerationMessage InvalidOrMalformedDataShapeBinding(EngineMessageSeverity severity, DataShapeGenerationMessagePhrase reason = null)
		{
			if (reason == null)
			{
				return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidOrMalformedDataShapeBinding, "Skipping invalid/malformed DataShapeBinding.{0}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { string.Empty });
			}
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidOrMalformedDataShapeBinding, "Skipping invalid/malformed DataShapeBinding.{0}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { reason });
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000BBF1 File Offset: 0x00009DF1
		public static DataShapeGenerationMessage InvalidOrMalformedDataShapeFilterConditionExpressions(EngineMessageSeverity severity, object objectId, SemanticQueryObjectType owningObjectType, string owningQueryName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidOrMalformedDataShapeFilterConditionExpressions, "Expression {1} {0} on '{2}' could not be translated into DataShape FilterCondition.", severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				objectId,
				owningObjectType,
				new ScrubbedString(owningQueryName)
			});
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000BC21 File Offset: 0x00009E21
		public static DataShapeGenerationMessage InvalidOrMalformedAnchorTime(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidOrMalformedAnchorTime, "The anchor time specified is not in the expected format.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000BC3A File Offset: 0x00009E3A
		public static DataShapeGenerationMessage InvalidOrMalformedExpression(EngineMessageSeverity severity, ResolvedQueryExpression expression)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidOrMalformedExpression, "Skipping invalid/malformed Expression: {0}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { expression.ToTraceString() });
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000BC5D File Offset: 0x00009E5D
		public static DataShapeGenerationMessage InvalidOrMalformedExtensionSchema(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidOrMalformedExtensionSchema, "Skipping invalid/malformed ExtensionSchema.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000BC76 File Offset: 0x00009E76
		public static DataShapeGenerationMessage InvalidOrMalformedOrderByExpression(EngineMessageSeverity severity, int index)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidOrMalformedOrderByExpression, "OrderBy expression {0} could not be translated into a DataShape expression.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { index });
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000BC99 File Offset: 0x00009E99
		public static DataShapeGenerationMessage InvalidOrMalformedSemanticQueryDataShape(EngineMessageSeverity severity, DataShapeGenerationMessagePhrase reason)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidOrMalformedSemanticQueryDataShape, "Skipping invalid/malformed semantic SemanticQueryDataShape. {0}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { reason });
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000BCB7 File Offset: 0x00009EB7
		public static DataShapeGenerationMessage InvalidOrMalformedSemanticQueryDataShapeCommand(EngineMessageSeverity severity, DataShapeGenerationMessagePhrase reason)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidOrMalformedSemanticQueryDataShapeCommand, "Skipping invalid/malformed semantic SemanticQueryDataShapeCommand. {0}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { reason });
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000BCD5 File Offset: 0x00009ED5
		public static DataShapeGenerationMessage InvalidOrMalformedSemanticQueryDefinition(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidOrMalformedSemanticQueryDefinition, "Skipping invalid/malformed semantic QueryDefinition.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000BCEE File Offset: 0x00009EEE
		public static DataShapeGenerationMessage InvalidPlotAxisBindingIndex(EngineMessageSeverity severity, int index)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidPlotAxisBindingIndex, "The query contains an invalid OverlappingPointsSample PlotAxisBinding index value of {0}. PlotAxisBinding index must contain a select index that matches axis projections.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { index });
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000BD11 File Offset: 0x00009F11
		public static DataShapeGenerationMessage InvalidQueryParameterReferenceType(EngineMessageSeverity severity, string parameterName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidQueryParameterReferenceType, "The query contains a reference to query parameter {0} in an expression that does not support the parameter's declared type. Verify that the parameter is declared with the correct type or remove the parameter reference.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { parameterName.MarkAsCustomerContent() });
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000BD34 File Offset: 0x00009F34
		public static DataShapeGenerationMessage InvalidQueryParameterTypeExpression(EngineMessageSeverity severity, string parameterName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidQueryParameterTypeExpression, "The query parameter declaration {0} contains an invalid type expression.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { parameterName.MarkAsCustomerContent() });
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000BD57 File Offset: 0x00009F57
		public static DataShapeGenerationMessage InvalidPrimaryScalarKeyIndex(EngineMessageSeverity severity, int index)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidPrimaryScalarKeyIndex, "The query contains an invalid BinnedLineSample PrimaryScalarKey value of {0}. PrimaryScalarKey must contain a select index that matches a primary axis projection.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { index });
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000BD7A File Offset: 0x00009F7A
		public static DataShapeGenerationMessage InvalidScopedDataReductionAlgorithm(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidScopedDataReductionAlgorithm, "Invalid reduction algorithm for scoped data reduction. TopNPerLevel, OverlappingPointsSample or BinnedLineSample may only be used if the Scope covers all grouping axes.", severity, ErrorSourceCategory.FallbackCondition, Array.Empty<object>());
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000BD93 File Offset: 0x00009F93
		public static DataShapeGenerationMessage InvalidSortByTransformOutputRoleRef(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidSortByTransformOutputRoleRef, "The query sorts by a column created by a transform. A query may only sort by columns originating from the conceptual schema.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000BDAC File Offset: 0x00009FAC
		public static DataShapeGenerationMessage InvalidTableArgumentToInOperatorInFilter(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidTableArgumentToInOperatorInFilter, "The Table argument to the In operator in a filter condition must be a reference to a subquery.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000BDC5 File Offset: 0x00009FC5
		public static DataShapeGenerationMessage InvalidTransformColumnExpression(EngineMessageSeverity severity, string tableName, string columnName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidTransformColumnExpression, "The transform column {1} in table {0} contains an invalid expression.", severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				tableName,
				columnName.MarkAsCustomerContent()
			});
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000BDEC File Offset: 0x00009FEC
		public static DataShapeGenerationMessage InvalidTransformColumnExpressionSchemaReference(EngineMessageSeverity severity, string tableName, string columnName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidTransformColumnExpressionSchemaReference, "The transform column {1} in table {0} contains an invalid reference to the data model. Only columns in the first transform input table may refer to the data model.", severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				tableName,
				columnName.MarkAsCustomerContent()
			});
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000BE13 File Offset: 0x0000A013
		public static DataShapeGenerationMessage InvalidTransformColumnReference(EngineMessageSeverity severity, string columnName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidTransformColumnReference, "The query contains an invalid reference to transform column {0}. Expressions may only refer to columns in earlier transform tables.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { columnName.MarkAsCustomerContent() });
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000BE36 File Offset: 0x0000A036
		public static DataShapeGenerationMessage InvalidTransformOutputRoleRefExpression(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidTransformOutputRoleRefExpression, "The query contains an invalid use of QueryTransformOutputRoleRef. QueryTransformOutputRoleRef is only allowed in the column expression of a transform output table, and it may not be combined with other expression elements.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000BE4F File Offset: 0x0000A04F
		public static DataShapeGenerationMessage InvalidTransformParameterExpression(EngineMessageSeverity severity, string parameterName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidTransformParameterExpression, "The transform parameter {0} contains an invalid expression.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { parameterName });
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000BE6D File Offset: 0x0000A06D
		public static DataShapeGenerationMessage MismatchedArgumentCountsToInOperator(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.MismatchedArgumentCountsToInOperator, "The count of expressions in the Expressions argument to the In operator must be <= count of selects in the Table argument.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000BE86 File Offset: 0x0000A086
		public static DataShapeGenerationMessage MismatchedArgumentCountsToInOperatorTableType(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.MismatchedArgumentCountsToInOperatorTableType, "The count of expressions in the Expressions argument to the In operator must match the number of columns in the type of the Table argument.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000BE9F File Offset: 0x0000A09F
		public static DataShapeGenerationMessage MismatchedColumnArgumentsToInOperatorInFilter(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.MismatchedColumnArgumentsToInOperatorInFilter, "When a filter condition uses the In operator with a Table argument, each Expression must refer to the same base column as the corresponding projection in the Table argument.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		public static DataShapeGenerationMessage NaNLiteralNotSupported(EngineMessageSeverity severity)
		{
			string localizedTemplate = DataShapeGenerationMessages.GetLocalizedTemplate(DataShapeGenerationErrorCode.NaNLiteralNotSupported);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.NaNLiteralNotSupported, localizedTemplate, severity, ErrorSourceCategory.UserInput, Array.Empty<object>());
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000BEE0 File Offset: 0x0000A0E0
		public static DataShapeGenerationMessage NoInnermostDynamicDataMemberFound(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.NoInnermostDynamicDataMemberFound, "Unable to find innermost dynamic data member.", severity, ErrorSourceCategory.FallbackCondition, Array.Empty<object>());
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000BEF9 File Offset: 0x0000A0F9
		public static DataShapeGenerationMessage NoInnermostDynamicDataMemberFoundForIntersectionLimit(EngineMessageSeverity severity, string hierarchy)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.NoInnermostDynamicDataMemberFoundForIntersectionLimit, "Unable to find innermost dynamic data member on {0} hierarchy for intersection limit.", severity, ErrorSourceCategory.FallbackCondition, new object[] { hierarchy });
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000BF17 File Offset: 0x0000A117
		public static DataShapeGenerationMessage NonColumnArgumentToInOperatorInFilter(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.NonColumnArgumentToInOperatorInFilter, "When a filter condition uses the In operator with a Table argument, all of the Expressions and all of the projections in the Table must be column references.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000BF30 File Offset: 0x0000A130
		public static DataShapeGenerationMessage NonexistentDataVolumeLevel(EngineMessageSeverity severity, int dataVolumeLevel)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.NonexistentDataVolumeLevel, "Specified DataVolume level {0} does not exist. Falling back to default.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { dataVolumeLevel });
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000BF53 File Offset: 0x0000A153
		public static DataShapeGenerationMessage NoStableKeyAndNoVisibleFieldsForSelectedEntity(EngineMessageSeverity severity, string entityName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.NoStableKeyAndNoVisibleFieldsForSelectedEntity, "Entity {0} must have a stable key to be selected, and no visible fields to use.", severity, ErrorSourceCategory.UserInput, new object[] { entityName.MarkAsCustomerContent() });
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000BF76 File Offset: 0x0000A176
		public static DataShapeGenerationMessage NoStableKeyForSelectedEntity(EngineMessageSeverity severity, string fieldName, string entityName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.NoStableKeyForSelectedEntity, "Entity {1} does not have a display key or stable key, selecting field: {0}.", severity, ErrorSourceCategory.UserInput, new object[]
			{
				fieldName.MarkAsCustomerContent(),
				entityName.MarkAsCustomerContent()
			});
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000BFA2 File Offset: 0x0000A1A2
		public static DataShapeGenerationMessage DuplicateGroupingKeysDetected(EngineMessageSeverity severity, string axis, int groupingNumber)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.DuplicateGroupingKeysDetected, "The query has a duplicate grouping key on {0} axis on grouping number {1}. This can happen when the same expression is projected twice or when the model group by and order by overlaps with another projection. This has an impact on the query subtotals, expand/collapse, sort by measure, and scoped aggregates.", severity, ErrorSourceCategory.UserInput, new object[] { axis, groupingNumber });
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000BFCC File Offset: 0x0000A1CC
		public static DataShapeGenerationMessage IgnoredOrderBy(EngineMessageSeverity severity, DsqSortKey dsqSortKey)
		{
			ScrubbedString scrubbedString = new ScrubbedString(dsqSortKey.Expression.ToString());
			DataShapeGenerationErrorCode dataShapeGenerationErrorCode = DataShapeGenerationErrorCode.IgnoredOrderBy;
			string text = "The order by {0} is ignored. An order by needs to be a projected column or a measure and groups need to be present in the query.";
			IContainsTelemetryMarkup[] array = scrubbedString.ArrayWrap<ScrubbedString>();
			return DataShapeGenerationMessages.CreateMessage(dataShapeGenerationErrorCode, text, array, severity, ErrorSourceCategory.UserInput, new object[] { scrubbedString.ToCustomerContentString() });
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000C013 File Offset: 0x0000A213
		public static DataShapeGenerationMessage OverlappingDataReductionScopes(EngineMessageSeverity severity, string groupingAxis, int groupIndex)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.OverlappingDataReductionScopes, "The {0} group at index {1} is covered by multiple data reduction algorithms. Only the first algorithm will be applied.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { groupingAxis, groupIndex });
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000C03C File Offset: 0x0000A23C
		public static DataShapeGenerationMessage OverlappingKeysOnOppositeHierarchies(EngineMessageSeverity severity)
		{
			string localizedTemplate = DataShapeGenerationMessages.GetLocalizedTemplate(DataShapeGenerationErrorCode.OverlappingKeysOnOppositeHierarchies);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.OverlappingKeysOnOppositeHierarchies, localizedTemplate, severity, ErrorSourceCategory.UserInput, Array.Empty<object>());
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000C064 File Offset: 0x0000A264
		public static DataShapeGenerationMessage QueryTopWithDataReduction(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.QueryTopWithDataReduction, "The query uses a top clause and the binding contains data reduction. Data reduction is forbidden if the top clause is used.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000C07D File Offset: 0x0000A27D
		public static DataShapeGenerationMessage QueryTopWithoutPrimary(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.QueryTopWithoutPrimary, "The query uses a top clause without a primary axis. A primary axis is required if the top clause is used.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000C096 File Offset: 0x0000A296
		public static DataShapeGenerationMessage QueryTopWithSecondary(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.QueryTopWithSecondary, "The query uses a top clause with a secondary axis. The secondary axis is forbidden if the top clause is used.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000C0AF File Offset: 0x0000A2AF
		public static DataShapeGenerationMessage ScopedDataReductionIntersectionScope(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ScopedDataReductionIntersectionScope, "The query uses a scoped data reduction that targets a combination of primary and secondary groups. Targeting both primary and secondary groups with the same reduction is not allowed.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000C0CB File Offset: 0x0000A2CB
		public static DataShapeGenerationMessage ScopedReductionWithIntersectionReduction(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.ScopedReductionWithIntersectionReduction, "The query uses a scoped data reduction and an intersection data reduction. Intersection and scoped data reductions may not be combined.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000C0E7 File Offset: 0x0000A2E7
		public static DataShapeGenerationMessage SecondaryGroupsWithoutPrimary(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.SecondaryGroupsWithoutPrimary, "DataShape has secondary groups but no primary.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000C103 File Offset: 0x0000A303
		public static DataShapeGenerationMessage SpecifiedDataWindowSizeExceedsMaxIntersections(EngineMessageSeverity severity, int originalSpec, DataShapeGenerationMessagePhrase reductionTarget)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.SpecifiedDataWindowSizeExceedsMaxIntersections, "Specified data window size ({0}) exceeds maximum allowed number of intersections. {1}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { originalSpec, reductionTarget });
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000C12D File Offset: 0x0000A32D
		public static DataShapeGenerationMessage SpecifiedIntersectionReductionAlgorithmExceedsMaxIntersections(EngineMessageSeverity severity, long originalReductionAlgorithms)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.SpecifiedIntersectionReductionAlgorithmExceedsMaxIntersections, "Specified intersection reduction algorithm exceeds the maximum allowed number of data points ({0}). The counts will be ignored.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { originalReductionAlgorithms });
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000C153 File Offset: 0x0000A353
		public static DataShapeGenerationMessage SpecifiedLimitExceedsMaxIntersections(EngineMessageSeverity severity, int originalSpec, DataShapeGenerationMessagePhrase reductionTarget)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.SpecifiedLimitExceedsMaxIntersections, "Specified limit ({0}) exceeds maximum allowed number of intersections.  {1}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { originalSpec, reductionTarget });
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000C17D File Offset: 0x0000A37D
		public static DataShapeGenerationMessage SpecifiedMaxDynamicSeriesExceedsMaxAllowedDynamicSeries(EngineMessageSeverity severity, int originalSpec, DataShapeGenerationMessagePhrase reductionTarget)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.SpecifiedMaxDynamicSeriesExceedsMaxAllowedDynamicSeries, "Specified maximum dynamic series count ({0}) exceeds maximum allowed number of dynamic series. The specified value will be ignored. {1}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { originalSpec, reductionTarget });
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000C1A7 File Offset: 0x0000A3A7
		public static DataShapeGenerationMessage SpecifiedMinPointsPerSeriesExceedsMaxIntersections(EngineMessageSeverity severity, int originalSpec, DataShapeGenerationMessagePhrase reductionTarget)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.SpecifiedMinPointsPerSeriesExceedsMaxIntersections, "Specified minimum points per series ({0}) exceeds maximum allowed number of intersections. The specified value will be ignored. {1}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { originalSpec, reductionTarget });
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000C1D1 File Offset: 0x0000A3D1
		public static DataShapeGenerationMessage SpecifiedPerLevelLimitExceedsMaxIntersections(EngineMessageSeverity severity, int originalSpec, DataShapeGenerationMessagePhrase reductionTarget)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.SpecifiedPerLevelLimitExceedsMaxIntersections, "Specified per-level limit ({0}) exceeds maximum allowed number of intersections. {1}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { originalSpec, reductionTarget });
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000C1FB File Offset: 0x0000A3FB
		public static DataShapeGenerationMessage SpecifiedReductionAlgorithmsExceedsMaxIntersections(EngineMessageSeverity severity, long originalReductionAlgorithms)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.SpecifiedReductionAlgorithmsExceedsMaxIntersections, "Specified reduction algorithms exceed the maximum allowed number of intersections ({0}). The counts will be ignored.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { originalReductionAlgorithms });
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000C224 File Offset: 0x0000A424
		public static DataShapeGenerationMessage TooManyExtensionProperties(EngineMessageSeverity severity, int actualCount, int allowedCount)
		{
			string localizedTemplate = DataShapeGenerationMessages.GetLocalizedTemplate(DataShapeGenerationErrorCode.TooManyExtensionProperties);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.TooManyExtensionProperties, localizedTemplate, severity, ErrorSource.User, new object[] { actualCount, allowedCount });
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000C261 File Offset: 0x0000A461
		public static DataShapeGenerationMessage TransformAndGroupBy(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.TransformAndGroupBy, "The query uses both a transform and a GroupBy. These features may not be combined in the same query.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000C27D File Offset: 0x0000A47D
		public static DataShapeGenerationMessage TransformAndHighlight(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.TransformAndHighlight, "The query uses both a transform and a highlight. These features may not be combined in the same query.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000C299 File Offset: 0x0000A499
		public static DataShapeGenerationMessage TransformAndInstanceFilter(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.TransformAndInstanceFilter, "The query uses both a transform and an instance filter. These features may not be combined in the same query.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000C2B5 File Offset: 0x0000A4B5
		public static DataShapeGenerationMessage TransformWithSchemaOrSubqueryReferenceInSameScope(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.TransformWithSchemaOrSubqueryReferenceInSameScope, "In the same scope, the query combines references to transforms, subqueries and the data model. These kinds of references may not be combined in the same scope.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000C2D1 File Offset: 0x0000A4D1
		public static DataShapeGenerationMessage TransformAndScopedAggregates(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.TransformAndScopedAggregates, "The query uses both a transform and scoped aggregates. These features may not be combined in the same query.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000C2ED File Offset: 0x0000A4ED
		public static DataShapeGenerationMessage TransformAndSecondaryGroup(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.TransformAndSecondaryGroup, "The query uses both a transform and a secondary group. These features may not be combined in the same query.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000C309 File Offset: 0x0000A509
		public static DataShapeGenerationMessage TransformAndSubtotal(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.TransformAndSubtotal, "The query uses both a transform and a subtotal. These features may not be combined in the same query.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000C325 File Offset: 0x0000A525
		public static DataShapeGenerationMessage TransformAndSuppressedProjections(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.TransformAndSuppressedProjections, "The query uses both a transform and suppressed projections. These features may not be combined in the same query.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000C341 File Offset: 0x0000A541
		public static DataShapeGenerationMessage TransformInsideSubquery(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.TransformInsideSubquery, "Transforms are not allowed within subqueries.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000C35D File Offset: 0x0000A55D
		public static DataShapeGenerationMessage TransformReferencedFromMultipleScopes(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.TransformReferencedFromMultipleScopes, "The query references a transform in multiple scopes. A query may only reference transforms in a single scope.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000C379 File Offset: 0x0000A579
		public static DataShapeGenerationMessage TransformWithoutDataShapeBinding(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.TransformWithoutDataShapeBinding, "The query contains a transform without specifying a DataShapeBinding. A DataShapeBinding is required to use a transform.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000C395 File Offset: 0x0000A595
		public static DataShapeGenerationMessage InFilterReferencingSubqueryWithTransform(EngineMessageSeverity severity, string subqueryName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InFilterReferencingSubqueryWithTransform, "The query contains an 'In' filter that refers to a subquery with transform. This is not allowed. Subquery name: {0}", severity, ErrorSourceCategory.UnsupportedFeature, new object[]
			{
				new ScrubbedString(subqueryName)
			});
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		public static DataShapeGenerationMessage InvalidAnyValueOrDefaultValue(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidAnyValueOrDefaultValue, "AnyValue or DefaultValue can only be nested in a Comparison or AND.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000C3D1 File Offset: 0x0000A5D1
		public static DataShapeGenerationMessage InvalidBinaryOperatorForAnyValueOrDefaultValue(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidBinaryOperatorForAnyValueOrDefaultValue, "Comparisons against AnyValue or DefaultValue must use the 'Equal' operator.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000C3EC File Offset: 0x0000A5EC
		public static DataShapeGenerationMessage InvalidFilterExceedsMaxNumberOfValuesForInFilter(EngineMessageSeverity severity, int actualCount, int maxCount)
		{
			string localizedTemplate = DataShapeGenerationMessages.GetLocalizedTemplate(DataShapeGenerationErrorCode.InvalidFilterExceedsMaxNumberOfValuesForInFilter);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidFilterExceedsMaxNumberOfValuesForInFilter, localizedTemplate, severity, ErrorSourceCategory.UserInput, new object[] { actualCount, maxCount });
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000C427 File Offset: 0x0000A627
		public static DataShapeGenerationMessage InvalidFilterTargetExpression(EngineMessageSeverity severity, ResolvedQueryExpression expression)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidFilterTargetExpression, "Invalid filter target column expression {0}.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { expression.ToTraceString() });
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000C44A File Offset: 0x0000A64A
		public static DataShapeGenerationMessage InvalidFilterTargetForAnyValueOrDefaultValue(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidFilterTargetForAnyValueOrDefaultValue, "AnyValue or DefaultValue require a target of type QueryPropertyExpression which refers to an IConceptualColumn.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000C463 File Offset: 0x0000A663
		public static DataShapeGenerationMessage InvalidFilterTargetScope(EngineMessageSeverity severity, string axis)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidFilterTargetScope, "{0} projections must have set equality with the corresponding subset in the filter target.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { axis });
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000C481 File Offset: 0x0000A681
		public static DataShapeGenerationMessage InvalidFilterType(EngineMessageSeverity severity, int index, string filterType, SemanticQueryObjectType owningObjectType, string owningQueryName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidFilterType, "The filter {2} {0} of type {1} is not allowed on '{3}'.", severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				index,
				filterType,
				owningObjectType,
				new ScrubbedString(owningQueryName)
			});
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000C4BB File Offset: 0x0000A6BB
		public static DataShapeGenerationMessage InvalidFilterSourceReference(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidFilterSourceReference, "A single filter can not reference model and subquery source at the same time.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000C4D4 File Offset: 0x0000A6D4
		public static DataShapeGenerationMessage InvalidScopedDataReductionIndices(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidScopedDataReductionIndices, "ScopedDataReduction indices are not valid. Scope indices need to be within bounds of the collection of groupings for the respective axis.", severity, ErrorSourceCategory.FallbackCondition, Array.Empty<object>());
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000C4ED File Offset: 0x0000A6ED
		public static DataShapeGenerationMessage UnexpectedLimitType(EngineMessageSeverity severity, DataShapeBindingLimitType limitType)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnexpectedLimitType, "Unexpected limit type: {0}.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { limitType });
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000C513 File Offset: 0x0000A713
		public static DataShapeGenerationMessage UnexpectedReductionAlgorithmType(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnexpectedReductionAlgorithmType, "Unexpected reduction algorithm.", severity, ErrorSourceCategory.FallbackCondition, Array.Empty<object>());
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000C52F File Offset: 0x0000A72F
		public static DataShapeGenerationMessage UnhandledExpression(EngineMessageSeverity severity, ResolvedQueryExpression expression)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnhandledExpression, "Unhandled expression: {0}.", severity, ErrorSourceCategory.UnexpectedError, new object[] { expression.ToTraceString() });
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000C555 File Offset: 0x0000A755
		public static DataShapeGenerationMessage UnsupportedScopedDataReduction(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedScopedDataReduction, "Unsupported ScopedDataReduction.", severity, ErrorSourceCategory.FallbackCondition, Array.Empty<object>());
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000C571 File Offset: 0x0000A771
		public static DataShapeGenerationMessage UnsupportedTableExpression(EngineMessageSeverity severity, string parentObjectType, string parentObjectName, ResolvedQueryExpression expression)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedTableExpression, "The query {0} '{1}' uses an unsupported expression type '{2}'.", severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				parentObjectType,
				parentObjectName.MarkAsModelInfo(),
				(expression != null) ? expression.GetType().Name : null
			});
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		public static DataShapeGenerationMessage UnsupportedExtensionMeasureDataType(EngineMessageSeverity severity, string schemaName, string measureName, string entityName, ConceptualPrimitiveType dataType)
		{
			IContainsTelemetryMarkup[] array = DataShapeGenerationMessages.CreateAffectedItems(schemaName, entityName, measureName);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedExtensionMeasureDataType, "The extension measure '{0}' on table '{1}' uses an unsupported data type '{2}'.", array, severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				measureName.MarkAsModelInfo(),
				entityName.MarkAsModelInfo(),
				dataType.ToString()
			});
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000C604 File Offset: 0x0000A804
		public static DataShapeGenerationMessage UnsupportedExtensionColumnDataType(EngineMessageSeverity severity, string schemaName, string columnName, string entityName, ConceptualPrimitiveType dataType)
		{
			IContainsTelemetryMarkup[] array = DataShapeGenerationMessages.CreateAffectedItems(schemaName, entityName, columnName);
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedExtensionColumnDataType, "The extension column '{0}' on table '{1}' uses an unsupported data type '{2}'.", array, severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				columnName.MarkAsModelInfo(),
				entityName.MarkAsModelInfo(),
				dataType.ToString()
			});
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000C658 File Offset: 0x0000A858
		public static DataShapeGenerationMessage UnsupportedFeatureInSemanticQueryDataShapeCommand(EngineMessageSeverity severity, string feature)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedFeatureInSemanticQueryDataShapeCommand, "'{0}' is not supported on a SemanticQueryDataShapeCommand.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { feature });
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000C679 File Offset: 0x0000A879
		public static DataShapeGenerationMessage UnsupportedFeatureOnContainer(EngineMessageSeverity severity, string feature, string container)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedFeatureOnContainer, "'{0}' is not supported on {1}.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { feature, container });
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000C69E File Offset: 0x0000A89E
		public static DataShapeGenerationMessage UnsupportedGroupSynchronization(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedGroupSynchronization, "'GroupSynchronization' is not supported on the SemanticQueryDataShapeCommand because the corresponding capability SupportsGroupSynchronization is not enabled.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000C6BA File Offset: 0x0000A8BA
		public static DataShapeGenerationMessage InvalidGroupSynchronization(EngineMessageSeverity severity, DataShapeGenerationMessagePhrase reason)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidGroupSynchronization, "The query contains an invalid group synchronization. '{0}'.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { reason });
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		public static DataShapeGenerationMessage UnsupportedExpansionWithoutTotalsInSemanticQueryDataShapeCommand(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedExpansionWithoutTotalsInSemanticQueryDataShapeCommand, "Expansion without totals is not supported on a SemanticQueryDataShapeCommand.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000C6F4 File Offset: 0x0000A8F4
		public static DataShapeGenerationMessage UnsupportedFeatureWithGenerationOptionInQueryDefinition(EngineMessageSeverity severity, string feature, string option)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedFeatureWithGenerationOptionInQueryDefinition, "'{0}' is not supported when translating only the QueryDefinition with the GenerationOption '{1}'", severity, ErrorSourceCategory.UnsupportedFeature, new object[] { feature, option });
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000C719 File Offset: 0x0000A919
		public static DataShapeGenerationMessage UnsupportedGroupByInSemanticQuery(EngineMessageSeverity severity, int index)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedGroupByInSemanticQuery, "The GroupBy in Semantic Query at index {0} is invalid. Only GroupBy referencing an Entity is supported.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { index });
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000C73F File Offset: 0x0000A93F
		public static DataShapeGenerationMessage UnsupportedMultipleGroupByEntitiesInSemanticQuery(EngineMessageSeverity severity, int index)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedMultipleGroupByEntitiesInSemanticQuery, "The GroupBy in Semantic Query at index {0} is invalid. Only a single GroupBy referencing an Entity is supported.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { index });
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000C765 File Offset: 0x0000A965
		public static DataShapeGenerationMessage UnsupportedMultipleSubqueryPostFilters(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedMultipleSubqueryPostFilters, "The query contains more than one filter that refers to a subquery. In a single where clause, filters may only refer to a single subquery.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000C781 File Offset: 0x0000A981
		public static DataShapeGenerationMessage UnsupportedProjectionIndexInSemanticQuery(EngineMessageSeverity severity, int index)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedProjectionIndexInSemanticQuery, "The Projection at index {0} is invalid.  The top level Projections must be a valid measure or aggregation expression in Semantic Query.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { index });
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000C7A7 File Offset: 0x0000A9A7
		public static DataShapeGenerationMessage UnsupportedDataSourceVariables(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedDataSourceVariables, "The data model does not allow data source variables in queries.", severity, ErrorSourceCategory.UserInput, Array.Empty<object>());
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000C7C3 File Offset: 0x0000A9C3
		public static DataShapeGenerationMessage InvalidWindowScope(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidWindowScope, "The query uses a ScopedDataReduction with an Algorithm of Window and a Scope that is empty, refers to Secondary groupings, or a Primary grouping other than the first. Window may only be used in a ScopedDataReduction with a Scope that refers to the outermost Primary grouping.", severity, ErrorSourceCategory.UserInput, Array.Empty<object>());
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		public static DataShapeGenerationMessage IsAfterNotSupported(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.IsAfterNotSupported, "IsAfter is not supported.", severity, ErrorSourceCategory.FallbackCondition, Array.Empty<object>());
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000C7F5 File Offset: 0x0000A9F5
		public static DataShapeGenerationMessage MissingIsAfterWithPrimaySubsetCovered(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.MissingIsAfterWithPrimaySubsetCovered, "When DataWindow does not include all primary groupings, RestartMatchingBehavior should be IsAfter.", severity, ErrorSourceCategory.FallbackCondition, Array.Empty<object>());
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000C80E File Offset: 0x0000AA0E
		public static DataShapeGenerationMessage ParameterMappingsNotSupportedOnHighlights()
		{
			return DataShapeGenerationMessage.Create(DataShapeGenerationErrorCode.ParameterMappingsNotSupportedOnHighlights, "Highlight filters were found with parameter mappings. Columns with parameter mappings are not supported in highlights.", EngineMessageSeverity.Error, ErrorSource.User, Array.Empty<object>());
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000C823 File Offset: 0x0000AA23
		public static DataShapeGenerationMessage UnsupportedSortByMeasure(string featureName, bool isPlural = false)
		{
			return DataShapeGenerationMessage.Create(DataShapeGenerationErrorCode.UnsupportedSortByMeasure, "The query sorts by a measure or aggregate and contains {0}. {0} {1} not supported on groups that sort on a measure or aggregate.", EngineMessageSeverity.Error, ErrorSource.User, new object[]
			{
				featureName,
				isPlural ? "are" : "is"
			});
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000C852 File Offset: 0x0000AA52
		public static DataShapeGenerationMessage UnsupportedVisualCalculation(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedVisualCalculation, "The query contains unsupported visual calculation expressions. Either the model capabilities do not support visual calculations or the visual calculation feature switch is off.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000C86E File Offset: 0x0000AA6E
		public static DataShapeGenerationMessage UnsupportedHiddenProjection(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedHiddenProjection, "The query contains unsupported Hidden Projections. The feature switch is off for this feature.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000C88A File Offset: 0x0000AA8A
		public static DataShapeGenerationMessage UnsupportedVisualShapeWithoutDataShapeBinding(EngineMessageSeverity severity, string queryName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedVisualShapeWithoutDataShapeBinding, "The query, '{0}', contains a Visual Shape, but no Data Shape Binding. A non-null Data Shape Binding must be present if a Visual Shape is included on the query.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { queryName.MarkAsCustomerContent() });
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
		public static DataShapeGenerationMessage UnsupportedSuppressedProjectionWithVisualCalculations(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedSuppressedProjectionWithVisualCalculations, "The query uses both suppressed projections and visual calculations. These features may not be combined in the same query.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000C8CC File Offset: 0x0000AACC
		public static DataShapeGenerationMessage UnsupportedVisualCalculationWithGroupBy(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedVisualCalculationWithGroupBy, "The query includes a visual calculation expression and group by. These features may not be combined in the same query.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		public static DataShapeGenerationMessage UnsupportedShowItemsWithNoDataWithVisualCalculations(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedVisualCalculationWithShowItemsWithNoData, "The query includes a visual calculation and show items with no data. These features may not be combined in the same query.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000C904 File Offset: 0x0000AB04
		public static DataShapeGenerationMessage UnsupportedSparklineWithVisualCalculation(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedSparklineWithVisualCalculation, "Sparkline expressions that refer to a visual calculation are unsupported.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000C920 File Offset: 0x0000AB20
		public static DataShapeGenerationMessage UnsupportedVisualCalculationWithTransforms(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedVisualCalculationWithTransforms, "The query includes a visual calculation and transforms. These features may not be combined in the same query.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000C93C File Offset: 0x0000AB3C
		public static DataShapeGenerationMessage UnsupportedUnprojectedFirstVisualShapeAxisGroup(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedUnprojectedFirstVisualShapeAxisGroup, "The first group from a VisualShape axis is not projected on the DataShapeBinding, which is not supported.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000C958 File Offset: 0x0000AB58
		public static DataShapeGenerationMessage UnsupportedInconsistentTotalsInVisualShapeAxis(EngineMessageSeverity severity, string axisName)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedInconsistentTotalsInVisualShapeAxis, "Visual Shape axis '{0}' has mixed totals state. All groups on an axis must have the same total setting.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { axisName });
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000C979 File Offset: 0x0000AB79
		public static DataShapeGenerationMessage UnsupportedIntermediateUnprojectedGroup(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedIntermediateUnprojectedGroup, "Unprojected groups followed by projected groups along the same hierarchy are not supported.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000C995 File Offset: 0x0000AB95
		public static DataShapeGenerationMessage UnsupportedVisualShapeAxisGroupKeyExpressionType(EngineMessageSeverity severity, string expressionType)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedVisualShapeAxisGroupKeyExpressionType, "The query contains a VisualShape axis group key of type '{0}' that is not supported.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { expressionType });
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000C9B6 File Offset: 0x0000ABB6
		public static DataShapeGenerationMessage UnsupportedVisualShapeAxisGroupTotals(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedVisualShapeAxisGroupTotals, "The VisualShape axis group subtotal state is incompatible with the corresponding binding group.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000C9D2 File Offset: 0x0000ABD2
		public static DataShapeGenerationMessage UnsupportedVisualShapeDataMemberMismatch(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedVisualShapeDataMemberMismatch, "The number of VisualShape axis groups does not match the number of data members in the data shape.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000C9EE File Offset: 0x0000ABEE
		public static DataShapeGenerationMessage UnsupportedVisualShapeAxisGroupKeyWithoutCorrespondingSelect(EngineMessageSeverity severity, string axisName, ResolvedQueryExpression expression)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedVisualShapeAxisGroupKeyWithoutCorrespondingSelect, "The Visual Shape axis, '{0}', contains a group key expression, '{1}', that does not have a corresponding Select item.", severity, ErrorSourceCategory.MalformedExternalInput, new object[]
			{
				axisName,
				expression.ToTraceString()
			});
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000CA18 File Offset: 0x0000AC18
		public static DataShapeGenerationMessage UnsupportedMissingVisualShape(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedMissingVisualShape, "The query contains a native visual calculation expression and groups, but there is no VisualShape to describe the axis structure, which is not supported.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000CA34 File Offset: 0x0000AC34
		public static DataShapeGenerationMessage UnsupportedVisualCalculationLanguage(EngineMessageSeverity severity, string unsupportedLanguage, string supportedLanguage)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedVisualCalculationLanguage, "The query includes a native visual calculation with unsupported language: '{0}'. Only '{1}' is supported.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { unsupportedLanguage, supportedLanguage });
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000CA59 File Offset: 0x0000AC59
		public static DataShapeGenerationMessage UnsupportedVisualShapeWithoutVisualCalcs(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedVisualShapeWithoutVisualCalcs, "The query includes a VisualShape but does not contain any native visual calculation expressions. The VisualShape will be ignored.", severity, ErrorSourceCategory.UnsupportedFeature, Array.Empty<object>());
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000CA75 File Offset: 0x0000AC75
		public static DataShapeGenerationMessage UnsupportedDuplicateVisualShapeKey(EngineMessageSeverity severity, ResolvedQueryExpression expression)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedDuplicateVisualShapeKey, "The query includes a VisualShape with a duplicate key, '{0}', which is not supported.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { expression.ToTraceString() });
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000CA9B File Offset: 0x0000AC9B
		public static DataShapeGenerationMessage InvalidVisualShapeAxisGroupProjection(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.InvalidVisualShapeAxisGroupProjection, "The query includes a VisualShape with a partially projected axis group or a projected group following an unprojected group, which is not supported.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		public static DataShapeGenerationMessage UnsupportedVisualShapeAxisGroupKeysProjectedAcrossDSBGroups(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedVisualShapeAxisGroupKeysProjectedAcrossDSBGroups, "The query includes a VisualShape with group keys projected across multiple DataShapeBinding groups which is not supported.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000CAD0 File Offset: 0x0000ACD0
		public static DataShapeGenerationMessage UnsupportedVisualShapeAxisGroupOutOfOrder(EngineMessageSeverity severity)
		{
			return DataShapeGenerationMessages.CreateMessage(DataShapeGenerationErrorCode.UnsupportedVisualShapeAxisGroupOutOfOrder, "The VisualShape axis groups are projected out of the expected order on the DataShapeBinding.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000CAEC File Offset: 0x0000ACEC
		public static DataShapeGenerationMessage InvalidExpansionState(DataShapeGenerationErrorCode invalidExpansionStateCode, DataShapeGenerationMessagePhrase reason)
		{
			return DataShapeGenerationMessages.CreateMessage(invalidExpansionStateCode, "The query contains invalid ExpansionState. {0}", EngineMessageSeverity.Warning, ErrorSourceCategory.MalformedExternalInput, new object[] { reason });
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000CB09 File Offset: 0x0000AD09
		public static string InternalDataShapeGenerationError(string errorCode)
		{
			return StringUtil.FormatInvariant("Data Shape Query generation failed with error code: '{0}'.Check the report server logs for more information.", DataShapeGenerationErrorCode.InternalDataShapeGenerationError);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		internal static DataShapeGenerationMessage CreateMessage(DataShapeGenerationErrorCode errorCode, string messageTemplate, EngineMessageSeverity severity, ErrorSource source, params object[] args)
		{
			return DataShapeGenerationMessage.Create(errorCode, messageTemplate, severity, source, args);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000CB29 File Offset: 0x0000AD29
		private static DataShapeGenerationMessage CreateMessage(DataShapeGenerationErrorCode errorCode, string messageTemplate, IContainsTelemetryMarkup[] affectedItems, EngineMessageSeverity severity, ErrorSource source, params object[] args)
		{
			return DataShapeGenerationMessage.Create(errorCode, messageTemplate, affectedItems, severity, source, args);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000CB38 File Offset: 0x0000AD38
		internal static string GetLocalizedTemplate(DataShapeGenerationErrorCode errorCode)
		{
			string text;
			if (DataShapeGenerationMessages.TryGetLocalizationCandidateTemplate(errorCode, out text))
			{
				return text;
			}
			return DataShapeGenerationMessages.InternalDataShapeGenerationError(errorCode.ToString());
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000CB63 File Offset: 0x0000AD63
		private static bool TryGetLocalizationCandidateTemplate(DataShapeGenerationErrorCode errorCode, out string errorString)
		{
			return DataShapeGenerationMessages._localizationCandidateStrings.TryGetValue(errorCode.ToString(), out errorString);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000CB7D File Offset: 0x0000AD7D
		private static IContainsTelemetryMarkup[] CreateAffectedItems(string schemaName, string entityName, string propertyName)
		{
			return new IContainsTelemetryMarkup[]
			{
				new ScrubbedEntityPropertyReference(new ScrubbedString(entityName), new ScrubbedString(propertyName), schemaName)
			};
		}

		// Token: 0x040001FF RID: 511
		private static Dictionary<string, string> _localizationCandidateStrings = new Dictionary<string, string>
		{
			{ "CouldNotResolveModelReferencesInSemanticQuery", "Could not resolve model references in the Semantic Query." },
			{ "CouldNotResolveModelReferencesInQueryExtensionSchema", "Could not resolve model references in the Query Extension Schema." },
			{ "ExtensionColumnEmptyExpression", "The extension column '{0}' on entity '{1}' has an empty DAX expression. Please provide a valid DAX expression." },
			{ "ExtensionColumnNameNotUniqueModel", "The extension column '{0}' on entity '{1}' has the same name as measure or column '{2}' on entity '{3}' in the base schema. Extension column names must not be the same as the name of any measure or column in the base schema. Please rename the extension column to have a unique name." },
			{ "ExtensionColumnNameNotUnique", "The extension column '{0}' on entity '{1}' has the same name as extension column '{2}' on entity '{3}'. Extension column names must not be the same as any other extension column. Please rename the extension column to have a unique name." },
			{ "ExtensionColumnAndMeasureNamesNotUnique", "The extension '{2}' '{0}' on entity '{1}' is already previously defined as an extension '{3}'. Extension columns and measures should be defined with unique names. Please rename either the extension column or measure to have a unique name." },
			{ "ExtensionMeasureEmptyExpression", "The extension measure '{0}' on entity '{1}' has an empty DAX expression. Please provide a valid DAX expression." },
			{ "ExtensionMeasureNameNotUniqueModel", "The extension measure '{0}' on entity '{1}' has the same name as measure or column '{2}' on entity '{3}' in the base schema. Extension measure names must not be the same as the name of any measure or column in the base schema. Please rename the extension measure to have a unique name." },
			{ "ExtensionMeasureNameNotUnique", "The extension measure '{0}' on entity '{1}' has the same name as extension measure '{2}' on entity '{3}'. Extension measure names must not be the same as any other extension measure. Please rename the extension measure to have a unique name." },
			{ "InvalidFilterComparisonIncompatibleExpressions", "The query contains a filter with an invalid comparison between {0} and '{1}'. This often happens when the data types do not match. Please update the data type or value." },
			{ "InvalidFilterExceedsMaxNumberOfValuesForInFilter", "The query contains a filter with {0} values.  The maximum number of values in an In filter operator is {1}." },
			{ "NaNLiteralNotSupported", "The 'NaN' (NotANumber) literal is not supported" },
			{ "OverlappingKeysOnOppositeHierarchies", "The query uses the same projection multiple times in the data shape binding." },
			{ "ParameterMappingFilterConflict", "Found multiple filters with different values on a parameter. To fix this, ensure that any columns mapped to a parameter are only filtered once." },
			{ "TooManyExtensionProperties", "The query contains too many ({0}) extension measures or columns. A query may contain up to a total of {1} extension measures and columns. Please reduce the number of extension measures or columns in this query." }
		};
	}
}
