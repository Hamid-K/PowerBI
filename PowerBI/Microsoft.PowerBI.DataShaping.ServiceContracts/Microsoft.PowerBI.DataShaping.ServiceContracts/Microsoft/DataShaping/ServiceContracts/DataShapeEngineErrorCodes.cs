using System;
using Microsoft.PowerBI.DataExtension.Contracts;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x02000009 RID: 9
	internal class DataShapeEngineErrorCodes : DataExtensionErrorCodes
	{
		// Token: 0x0400005E RID: 94
		internal const string RawDataProcessingError = "RawDataProcessingError";

		// Token: 0x0400005F RID: 95
		internal const string DataShapeQueryGenerationError = "rsDataShapeQueryGenerationError";

		// Token: 0x04000060 RID: 96
		internal const string InternalError = "rsInternalError";

		// Token: 0x04000061 RID: 97
		internal const string DataShapeQueryTranslationError = "rsDataShapeQueryTranslationError";

		// Token: 0x04000062 RID: 98
		internal const string DataModelParsingError = "DataModelParsingError";

		// Token: 0x04000063 RID: 99
		internal const string QueryExtensionMeasureError = "QueryExtensionMeasureError";

		// Token: 0x04000064 RID: 100
		internal const string QueryExtensionColumnError = "QueryExtensionColumnError";

		// Token: 0x04000065 RID: 101
		internal const string QueryExtensionMeasureUnexpectedEndOfUserInput = "QueryExtensionMeasureUnexpectedEndOfUserInput";

		// Token: 0x04000066 RID: 102
		internal const string QueryExtensionColumnUnexpectedEndOfUserInput = "QueryExtensionColumnUnexpectedEndOfUserInput";

		// Token: 0x04000067 RID: 103
		internal const string DataTransformException = "DataTransformError";

		// Token: 0x04000068 RID: 104
		internal const string UnexpectedDataTransformException = "UnexpectedDataTransformError";

		// Token: 0x04000069 RID: 105
		internal const string DataTransformInvalidOutputException = "DataTransformInvalidOutputError";

		// Token: 0x0400006A RID: 106
		internal const string DataExtensionInvalidOutputError = "DataExtensionInvalidOutputError";

		// Token: 0x0400006B RID: 107
		internal const string MissingDataTransformException = "MissingDataTransformError";

		// Token: 0x0400006C RID: 108
		internal const string DataExtensionMissingResultSet = "DataExtensionMissingResultSet";

		// Token: 0x0400006D RID: 109
		internal const string ArgumentOutOfRangeExceptionInDataReader = "ArgumentOutOfRangeExceptionInDataReader";

		// Token: 0x02000020 RID: 32
		internal static class SemanticQueryToDaxTranslation
		{
			// Token: 0x040000A9 RID: 169
			internal const string UnknownError = "UnknownError";

			// Token: 0x040000AA RID: 170
			internal const string QueryUpgradeError = "QueryUpgradeError";

			// Token: 0x040000AB RID: 171
			internal const string InvalidSemanticQueryError = "InvalidSemanticQueryError";

			// Token: 0x040000AC RID: 172
			internal const string UnknownQueryUpgradeError = "UnknownQueryUpgradeError";

			// Token: 0x040000AD RID: 173
			internal const string QueryResolutionError = "QueryResolutionError";

			// Token: 0x040000AE RID: 174
			internal const string UnknownQueryResolutionError = "UnknownQueryResolutionError";

			// Token: 0x040000AF RID: 175
			internal const string DaxTransformError = "DaxTransformError";

			// Token: 0x040000B0 RID: 176
			internal const string InvalidQueryForExpressionFilters = "InvalidQueryForExpressionFilters";

			// Token: 0x040000B1 RID: 177
			internal const string InvalidQueryForExpressionSorting = "InvalidQueryForExpressionSorting";

			// Token: 0x040000B2 RID: 178
			internal const string InvalidQueryForExpressionMultipleProjections = "InvalidQueryForExpressionMultipleProjections";

			// Token: 0x040000B3 RID: 179
			internal const string QdmTranslationError = "QdmTranslationError";

			// Token: 0x040000B4 RID: 180
			internal const string InvalidComparisonKind = "InvalidComparisonKind";

			// Token: 0x040000B5 RID: 181
			internal const string InvalidExpressionType = "InvalidExpressionType";

			// Token: 0x040000B6 RID: 182
			internal const string InvalidColumnExpression = "InvalidColumnExpression";

			// Token: 0x040000B7 RID: 183
			internal const string InvalidMeasureExpression = "InvalidMeasureExpression";

			// Token: 0x040000B8 RID: 184
			internal const string InvalidNullValueComparison = "InvalidNullValueComparison";

			// Token: 0x040000B9 RID: 185
			internal const string InvalidGroupingError = "InvalidGroupingError";

			// Token: 0x040000BA RID: 186
			internal const string GroupingResolutionError = "GroupingResolutionError";

			// Token: 0x040000BB RID: 187
			internal const string BinningTranslationErrorExpectedField = "BinningTranslationErrorExpectedField";

			// Token: 0x040000BC RID: 188
			internal const string BinningTranslationErrorInvalidDataType = "BinningTranslationErrorInvalidDataType";

			// Token: 0x040000BD RID: 189
			internal const string BinningTranslationErrorInvalidTimeUnit = "BinningTranslationErrorInvalidTimeUnit";

			// Token: 0x040000BE RID: 190
			internal const string BinningTranslationErrorMissingTimeUnit = "BinningTranslationErrorMissingTimeUnit";

			// Token: 0x040000BF RID: 191
			internal const string GroupingTranslationError = "GroupingTranslationError";

			// Token: 0x040000C0 RID: 192
			internal const string GroupingTranslationErrorNoMeaningfulGroups = "GroupingTranslationErrorNoMeaningfulGroups";

			// Token: 0x040000C1 RID: 193
			internal const string GroupingTranslationErrorDefaultCaseDataType = "GroupingTranslationErrorDefaultCaseDataType";

			// Token: 0x040000C2 RID: 194
			internal const string GroupingTranslationErrorBinaryColumn = "GroupingTranslationErrorBinaryColumn";

			// Token: 0x040000C3 RID: 195
			internal const string ClusteringTranslationError = "ClusteringTranslationError";

			// Token: 0x040000C4 RID: 196
			internal const string ClusteringColumnGenerationError = "ClusteringColumnGenerationError";

			// Token: 0x040000C5 RID: 197
			internal const string InvalidPartitionTableError = "InvalidPartitionTableError";

			// Token: 0x040000C6 RID: 198
			internal const string InvalidPartitionTableColumnsError = "InvalidPartitionTableColumnsError";

			// Token: 0x040000C7 RID: 199
			internal const string MissingPartitionTableContentError = "MissingPartitionTableContentError";

			// Token: 0x040000C8 RID: 200
			internal const string MissingPartitionTableMappingsError = "MissingPartitionTableMappingsError";

			// Token: 0x040000C9 RID: 201
			internal const string UnknownBinningTranslationError = "UnknownBinningTranslationError";

			// Token: 0x040000CA RID: 202
			internal const string UnknownBinningDaxTranslationError = "UnknownBinningDaxTranslationError";

			// Token: 0x040000CB RID: 203
			internal const string UnknownBinningValidationError = "UnknownBinningValidationError";

			// Token: 0x040000CC RID: 204
			internal const string UnknownSparklineDataValidationError = "UnknownSparklineDataValidationError";

			// Token: 0x040000CD RID: 205
			internal const string UnsupportedSparklineDataExpression = "UnsupportedSparklineDataExpression";

			// Token: 0x040000CE RID: 206
			internal const string SparklineDataTranslationErrorUnexpectedMeasure = "SparklineDataTranslationErrorUnexpectedMeasure";

			// Token: 0x040000CF RID: 207
			internal const string SparklineDataTranslationErrorUnsupportedNumberOfGroupings = "SparklineDataTranslationErrorUnsupportedNumberOfGroupings";

			// Token: 0x040000D0 RID: 208
			internal const string SparklineDataTranslationErrorUnexpectedGrouping = "SparklineDataTranslationErrorUnexpectedGrouping";

			// Token: 0x040000D1 RID: 209
			internal const string SparklineDataTranslationErrorUnexpectedScalarKey = "SparklineDataTranslationErrorUnexpectedScalarKey";

			// Token: 0x040000D2 RID: 210
			internal const string UnsupportedFeatureInTranslateQueryCommand = "UnsupportedFeatureInTranslateQueryCommand";

			// Token: 0x040000D3 RID: 211
			internal const string UnsupportedFeatureInTranslateQueryCommandForModel = "UnsupportedFeatureInTranslateQueryCommandForModel";
		}
	}
}
