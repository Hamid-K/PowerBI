using System;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Utils;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x0200000C RID: 12
	internal static class SemanticQueryTranslationMessages
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002679 File Offset: 0x00000879
		internal static SemanticQueryTranslationMessage InvalidPartitionTableError(EngineMessageSeverity severity, string columnType, string columnName)
		{
			return SemanticQueryTranslationMessages.Create("InvalidPartitionTableError", "The PartitionTableDefinition is semantically invalid. {0} '{1}' cannot be found in the TableDefinition select.", severity, ErrorSourceCategory.MalformedInternalInput, new object[]
			{
				columnType,
				columnName.MarkAsModelInfo()
			});
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026A3 File Offset: 0x000008A3
		internal static SemanticQueryTranslationMessage InvalidPartitionTableColumnsError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("InvalidPartitionTableColumnsError", "The partition table identity mapping should be betwen two valid columns", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026BF File Offset: 0x000008BF
		internal static SemanticQueryTranslationMessage GroupingTranslationError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("GroupingTranslationError", "An unknown error occurred during DAX generation for the grouping column.", severity, ErrorSourceCategory.UnexpectedError, Array.Empty<object>());
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026DB File Offset: 0x000008DB
		internal static SemanticQueryTranslationMessage GroupingTranslationErrorBinaryColumn(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("GroupingTranslationErrorBinaryColumn", "The grouped column has invalid data type.  Grouping is not allowed on binary columns.", severity, ErrorSourceCategory.MalformedInternalInput, Array.Empty<object>());
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026F7 File Offset: 0x000008F7
		internal static SemanticQueryTranslationMessage GroupingTranslationErrorDefaultCaseDataType(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("GroupingTranslationErrorDefaultCaseDataType", "The grouped column has invalid data type for default case of grouping.", severity, ErrorSourceCategory.MalformedInternalInput, Array.Empty<object>());
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002713 File Offset: 0x00000913
		internal static SemanticQueryTranslationMessage GroupingTranslationErrorNoMeaningfulGroups(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("GroupingTranslationErrorNoMeaningfulGroups", "The grouped column has no meaningful groups.", severity, ErrorSourceCategory.MalformedInternalInput, Array.Empty<object>());
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000272F File Offset: 0x0000092F
		internal static SemanticQueryTranslationMessage QdmTranslationError(EngineMessageSeverity severity, string qdmErrorMessage)
		{
			return SemanticQueryTranslationMessages.Create("QdmTranslationError", "An error occurred translating the specified expression: {0}", severity, ErrorSourceCategory.UnexpectedError, new object[] { qdmErrorMessage.MarkAsCustomerContent() });
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002755 File Offset: 0x00000955
		internal static SemanticQueryTranslationMessage BinningTranslationErrorExpectedField(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("BinningTranslationErrorExpectedField", "Binned expression should translate to a field expression.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002771 File Offset: 0x00000971
		internal static SemanticQueryTranslationMessage BinningTranslationErrorInvalidDataType(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("BinningTranslationErrorInvalidDataType", "The binned field has invalid data type.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000278D File Offset: 0x0000098D
		internal static SemanticQueryTranslationMessage BinningTranslationErrorInvalidTimeUnit(EngineMessageSeverity severity, TimeUnit timeUnit)
		{
			return SemanticQueryTranslationMessages.Create("BinningTranslationErrorInvalidTimeUnit", "TimeUnit {0} is not supported for binning.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { timeUnit });
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027B3 File Offset: 0x000009B3
		internal static SemanticQueryTranslationMessage BinningTranslationErrorMissingTimeUnit(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("BinningTranslationErrorMissingTimeUnit", "DateTime input columns require a TimeUnit to be specified.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027CF File Offset: 0x000009CF
		internal static SemanticQueryTranslationMessage InvalidComparisonKind(EngineMessageSeverity severity, QueryComparisonKind comparisonKind)
		{
			return SemanticQueryTranslationMessages.Create("InvalidComparisonKind", "ResolvedSemanticQueryGenerator Comparison processing failed: Unknown type {0}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { comparisonKind });
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027F5 File Offset: 0x000009F5
		internal static SemanticQueryTranslationMessage InvalidNullValueComparison(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("InvalidNullValueComparison", "ResolvedSemanticQueryGenerator Comparison processing failed: Cannot compare two null expressions", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002811 File Offset: 0x00000A11
		internal static SemanticQueryTranslationMessage InvalidExpressionType(EngineMessageSeverity severity, string expressionTypeName)
		{
			return SemanticQueryTranslationMessages.Create("InvalidExpressionType", "Unknown expression type in SemanticQueryTranslation: {0}", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { expressionTypeName });
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002832 File Offset: 0x00000A32
		internal static SemanticQueryTranslationMessage InvalidColumnExpression(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("InvalidColumnExpression", "Invalid Column expression in SemanticQueryTranslation. Only model based columns are allowed", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000284E File Offset: 0x00000A4E
		internal static SemanticQueryTranslationMessage InvalidMeasureExpression(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("InvalidMeasureExpression", "Invalid Measure expression in SemanticQueryTranslation. The measure needs to be a model measure or an extension measure.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000286A File Offset: 0x00000A6A
		internal static SemanticQueryTranslationMessage InvalidQueryForExpressionFilters(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("InvalidQueryForExpressionFilters", "SemanticQueryTranslation does not support filters.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002886 File Offset: 0x00000A86
		internal static SemanticQueryTranslationMessage InvalidQueryForExpressionSorting(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("InvalidQueryForExpressionSorting", "SemanticQueryTranslation does not support sorting.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000028A2 File Offset: 0x00000AA2
		internal static SemanticQueryTranslationMessage InvalidQueryForExpressionMultipleProjections(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("InvalidQueryForExpressionMultipleProjections", "SemanticQueryTranslation does not support multiple projections.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028BE File Offset: 0x00000ABE
		internal static SemanticQueryTranslationMessage DaxTransformError(EngineMessageSeverity severity, ErrorSource source, string message = null)
		{
			if (message == null)
			{
				message = "DaxTransform failed to generate a Dax expression from Qdm";
			}
			return SemanticQueryTranslationMessages.Create("DaxTransformError", message, severity, source, Array.Empty<object>());
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028DC File Offset: 0x00000ADC
		internal static SemanticQueryTranslationMessage UnknownBinningTranslationError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("UnknownBinningTranslationError", "An unknown error occurred during the translation of the binning semantic query.", severity, ErrorSourceCategory.UnexpectedError, Array.Empty<object>());
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000028F8 File Offset: 0x00000AF8
		internal static SemanticQueryTranslationMessage UnknownBinningDaxTranslationError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("UnknownBinningDaxTranslationError", "An unknown error occurred during DAX generation for the binning column.", severity, ErrorSourceCategory.UnexpectedError, Array.Empty<object>());
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002914 File Offset: 0x00000B14
		internal static SemanticQueryTranslationMessage UnknownBinningValidationError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("UnknownBinningValidationError", "Invalid BinItem.", severity, ErrorSourceCategory.UnexpectedError, Array.Empty<object>());
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002930 File Offset: 0x00000B30
		internal static SemanticQueryTranslationMessage UnknownSparklineDataValidationError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("UnknownSparklineDataValidationError", "SparklineData Expression Unknown Error.", severity, ErrorSourceCategory.UnexpectedError, Array.Empty<object>());
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000294C File Offset: 0x00000B4C
		public static SemanticQueryTranslationMessage UnsupportedSparklineDataExpression(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("UnsupportedSparklineDataExpression", "The query contains a SparklineData expression. The feature switch is off for this feature.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002968 File Offset: 0x00000B68
		internal static SemanticQueryTranslationMessage SparklineDataTranslationUnexpectedMeasureError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("SparklineDataTranslationErrorUnexpectedMeasure", "SparklineData measure expression should translate to a measure or aggregate expression.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002984 File Offset: 0x00000B84
		internal static SemanticQueryTranslationMessage SparklineDataTranslationUnsupportedNumberOfGroupingError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("SparklineDataTranslationErrorUnsupportedNumberOfGroupings", "Unsupported number of groupings in the SparklineData expression, only one grouping is supported.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029A0 File Offset: 0x00000BA0
		internal static SemanticQueryTranslationMessage SparklineDataTranslationUnexpectedGroupingError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("SparklineDataTranslationErrorUnexpectedGrouping", "SparklineData grouping expression should translate to a field expression.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029BC File Offset: 0x00000BBC
		internal static SemanticQueryTranslationMessage SparklineDataTranslationUnexpectedScalarKeyError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("SparklineDataTranslationErrorUnexpectedScalarKey", "SparklineData scalar key expression should translate to an expression of scalar primitive result type.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029D8 File Offset: 0x00000BD8
		internal static SemanticQueryTranslationMessage ClusteringColumnGenerationError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("ClusteringColumnGenerationError", "An unknown error occurred during generation of the clustering column", severity, ErrorSourceCategory.UnexpectedError, Array.Empty<object>());
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000029F4 File Offset: 0x00000BF4
		internal static SemanticQueryTranslationMessage MissingPartitionTableContentError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("MissingPartitionTableContentError", "The partiton table is missing/incomplete", severity, ErrorSourceCategory.UnexpectedError, Array.Empty<object>());
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A10 File Offset: 0x00000C10
		internal static SemanticQueryTranslationMessage MissingPartitionTableMappingsError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("MissingPartitionTableMappingsError", "Only exactly one non-empty collection of partition table identity mappings is expected and supported", severity, ErrorSourceCategory.UnexpectedError, Array.Empty<object>());
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A2C File Offset: 0x00000C2C
		internal static SemanticQueryTranslationMessage InvalidSemanticQueryError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("InvalidSemanticQueryError", "Semantic Query is invalid.", severity, ErrorSourceCategory.UnexpectedError, Array.Empty<object>());
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A48 File Offset: 0x00000C48
		internal static SemanticQueryTranslationMessage GroupingResolutionError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("GroupingResolutionError", "An unknown error occurred during GroupingDefinition resolution", severity, ErrorSourceCategory.UnexpectedError, Array.Empty<object>());
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002A64 File Offset: 0x00000C64
		internal static SemanticQueryTranslationMessage InvalidGroupingError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("InvalidGroupingError", "The GroupingDefinition is invalid.", severity, ErrorSourceCategory.MalformedExternalInput, Array.Empty<object>());
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A80 File Offset: 0x00000C80
		internal static SemanticQueryTranslationMessage UnknownQueryUpgradeError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("UnknownQueryUpgradeError", "An unknown error occurred during query upgrade", severity, ErrorSourceCategory.UnexpectedError, Array.Empty<object>());
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A9C File Offset: 0x00000C9C
		internal static SemanticQueryTranslationMessage UnknownQueryResolutionError(EngineMessageSeverity severity)
		{
			return SemanticQueryTranslationMessages.Create("UnknownQueryResolutionError", "An unknown error occurred during query resolution", severity, ErrorSourceCategory.UnexpectedError, Array.Empty<object>());
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public static SemanticQueryTranslationMessage UnsupportedFeatureInTranslateQueryCommand(EngineMessageSeverity severity, string feature)
		{
			return SemanticQueryTranslationMessages.Create("UnsupportedFeatureInTranslateQueryCommand", "'{0}' is not supported on a TranslateQueryCommand.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { feature });
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002AD9 File Offset: 0x00000CD9
		public static SemanticQueryTranslationMessage UnsupportedFeatureInTranslateQueryCommandForModel(EngineMessageSeverity severity, string feature)
		{
			return SemanticQueryTranslationMessages.Create("UnsupportedFeatureInTranslateQueryCommandForModel", "'{0}' is not supported on a TranslateQueryCommand against the current data model. Remove the feature from the command or try the command against a newer version of the data model or model host.", severity, ErrorSourceCategory.MalformedExternalInput, new object[] { feature });
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002AFA File Offset: 0x00000CFA
		private static SemanticQueryTranslationMessage Create(string errorCode, string messageTemplate, EngineMessageSeverity severity, ErrorSource source, params object[] args)
		{
			return SemanticQueryTranslationMessages.Create(errorCode, messageTemplate, null, severity, source, args);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B08 File Offset: 0x00000D08
		private static SemanticQueryTranslationMessage Create(string errorCode, string messageTemplate, string[] affectedItems, EngineMessageSeverity severity, ErrorSource source, params object[] args)
		{
			return SemanticQueryTranslationMessage.Create(errorCode, messageTemplate, affectedItems, severity, source, args);
		}
	}
}
