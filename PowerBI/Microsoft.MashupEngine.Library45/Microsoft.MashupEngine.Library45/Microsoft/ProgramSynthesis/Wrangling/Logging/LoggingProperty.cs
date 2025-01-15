using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Logging
{
	// Token: 0x02000179 RID: 377
	public static class LoggingProperty
	{
		// Token: 0x040003CA RID: 970
		public const string Success = "Success";

		// Token: 0x040003CB RID: 971
		public const string TopProgram = "TopProgram";

		// Token: 0x040003CC RID: 972
		public const string TopProgramAnonymized = "TopProgramAnonymized";

		// Token: 0x040003CD RID: 973
		public const string SessionType = "SessionType";

		// Token: 0x040003CE RID: 974
		public const string Constraints = "Constraints";

		// Token: 0x040003CF RID: 975
		public const string NoInputConstraints = "NoInputConstraints";

		// Token: 0x040003D0 RID: 976
		public const string InputSample = "InputSample";

		// Token: 0x040003D1 RID: 977
		public const string Conflicts = "Conflicts";

		// Token: 0x040003D2 RID: 978
		public const string ProseVersion = "ProseVersion";

		// Token: 0x040003D3 RID: 979
		public const string CxPythonEncoding = "CxPythonEncoding";

		// Token: 0x040003D4 RID: 980
		public const string CxProseEncoding = "CxProseEncoding";

		// Token: 0x040003D5 RID: 981
		public const string CxEncodingAllAscii = "CxEncodingAllAscii";

		// Token: 0x040003D6 RID: 982
		public const string CxEncodingDidLearn = "CxEncodingDidLearn";

		// Token: 0x040003D7 RID: 983
		public const string CxEncodingIsBinary = "CxEncodingIsBinary";

		// Token: 0x040003D8 RID: 984
		public const string CxEncoding = "CxEncoding";

		// Token: 0x040003D9 RID: 985
		public const string CxFileType = "CxFileType";

		// Token: 0x040003DA RID: 986
		public const string CxTarget = "CxTarget";

		// Token: 0x040003DB RID: 987
		public const string CxDetectDataTypes = "CxDetectDataTypes";

		// Token: 0x040003DC RID: 988
		public const string CxDelimiter = "CxDelimiter";

		// Token: 0x040003DD RID: 989
		public const string CxFieldPositions = "CxFieldPositions";

		// Token: 0x040003DE RID: 990
		public const string CxSkip = "CxSkip";

		// Token: 0x040003DF RID: 991
		public const string ProgramSamplingStrategy = "ProgramSamplingStrategy";

		// Token: 0x040003E0 RID: 992
		public const string Id = "Id";

		// Token: 0x040003E1 RID: 993
		public const string TransformationsUsed = "TransformationsUsed";

		// Token: 0x040003E2 RID: 994
		public const string AutoCompleterEventId = "AutoCompleterEventId";

		// Token: 0x040003E3 RID: 995
		public const string AutoCompleterSuggestEventId = "AutoCompleterSuggestEventId";

		// Token: 0x040003E4 RID: 996
		public const string AutoCompleterRowData = "AutoCompleterRowData";

		// Token: 0x040003E5 RID: 997
		public const string AutoCompleterRowDataIsIncomplete = "AutoCompleterRowDataIsIncomplete";

		// Token: 0x040003E6 RID: 998
		public const string AutoCompleterUserInput = "AutoCompleterUserInput";

		// Token: 0x040003E7 RID: 999
		public const string AutoCompleterEntityCounts = "AutoCompleterEntityCounts";

		// Token: 0x040003E8 RID: 1000
		public const string AutoCompleterSuggestions = "AutoCompleterSuggestions";

		// Token: 0x040003E9 RID: 1001
		public const string AutoCompleterSuggestionsAreIncomplete = "AutoCompleterSuggestionsAreIncomplete";

		// Token: 0x040003EA RID: 1002
		public const string AutoCompleterConfirmedSuggestionIndex = "AutoCompleterConfirmedSuggestionIndex";

		// Token: 0x040003EB RID: 1003
		public const string SplitTextColumnCount = "SplitTextColumnCount";

		// Token: 0x040003EC RID: 1004
		public const string SplitTextSplitCount = "SplitTextSplitCount";

		// Token: 0x040003ED RID: 1005
		public const string CompoundSplitColumnDelimiter = "CompoundSplitColumnDelimiter";

		// Token: 0x040003EE RID: 1006
		public const string CompoundSplitCommentStr = "CompoundSplitCommentStr";

		// Token: 0x040003EF RID: 1007
		public const string CompoundSplitEscapeCharacter = "CompoundSplitEscapeCharacter";

		// Token: 0x040003F0 RID: 1008
		public const string CompoundSplitFilterEmptyLines = "CompoundSplitFilterEmptyLines";

		// Token: 0x040003F1 RID: 1009
		public const string CompoundSplitHasCommentHeader = "CompoundSplitHasCommentHeader";

		// Token: 0x040003F2 RID: 1010
		public const string CompoundSplitHasNewLineInQuotes = "CompoundSplitHasNewLineInQuotes";

		// Token: 0x040003F3 RID: 1011
		public const string CompoundSplitHeaderIndex = "CompoundSplitHeaderIndex";

		// Token: 0x040003F4 RID: 1012
		public const string CompoundSplitIsDelimitedProgram = "CompoundSplitIsDelimitedProgram";

		// Token: 0x040003F5 RID: 1013
		public const string CompoundSplitIsFixedWidthProgram = "CompoundSplitIsFixedWidthProgram";

		// Token: 0x040003F6 RID: 1014
		public const string CompoundSplitQuoteCharacter = "CompoundSplitQuoteCharacter";

		// Token: 0x040003F7 RID: 1015
		public const string CompoundSplitQuotingStyle = "CompoundSplitQuotingStyle";

		// Token: 0x040003F8 RID: 1016
		public const string CompoundSplitSkipLinesCount = "CompoundSplitSkipLinesCount";

		// Token: 0x040003F9 RID: 1017
		public const string CompoundSplitHasDataRegex = "CompoundSplitHasDataRegex";

		// Token: 0x040003FA RID: 1018
		public const string CompoundSplitHasHeaderRegex = "CompoundSplitHasHeaderRegex";

		// Token: 0x040003FB RID: 1019
		public const string ReadFlatFileProgramType = "ReadFlatFileProgramType";

		// Token: 0x040003FC RID: 1020
		public const string ReadFlatFileColumnCount = "ReadFlatFileColumnCount";

		// Token: 0x040003FD RID: 1021
		public const string ReadFlatFileSkipCount = "ReadFlatFileSkipCount";

		// Token: 0x040003FE RID: 1022
		public const string ReadFlatFileSkipFooterCount = "ReadFlatFileSkipFooterCount";

		// Token: 0x040003FF RID: 1023
		public const string ReadFlatFileFilterEmptyLines = "ReadFlatFileFilterEmptyLines";

		// Token: 0x04000400 RID: 1024
		public const string ReadFlatFileCommentStr = "ReadFlatFileCommentStr";

		// Token: 0x04000401 RID: 1025
		public const string ReadFlatFileDelimiter = "ReadFlatFileDelimiter";

		// Token: 0x04000402 RID: 1026
		public const string ReadFlatFileQuoteChar = "ReadFlatFileQuoteChar";

		// Token: 0x04000403 RID: 1027
		public const string ReadFlatFileEscapeChar = "ReadFlatFileEscapeChar";

		// Token: 0x04000404 RID: 1028
		public const string ReadFlatFileDoubleQuote = "ReadFlatFileDoubleQuote";

		// Token: 0x04000405 RID: 1029
		public const string ReadFlatFileNewLineStrings = "ReadFlatFileNewLineStrings";

		// Token: 0x04000406 RID: 1030
		public const string ReadFlatFileHasMultiLineRows = "ReadFlatFileHasMultiLineRows";

		// Token: 0x04000407 RID: 1031
		public const string ReadFlatFileHasEmptyLines = "ReadFlatFileHasEmptyLines";

		// Token: 0x04000408 RID: 1032
		public const string FilterValueCompleterOp = "FilterValueCompleterOp";

		// Token: 0x04000409 RID: 1033
		public const string FilterValueCompleterPrefix = "FilterValueCompleterPrefix";

		// Token: 0x0400040A RID: 1034
		public const string FilterValueCompleterSuggestions = "FilterValueCompleterSuggestions";

		// Token: 0x0400040B RID: 1035
		public const string DetectedType = "DetectedType";

		// Token: 0x0400040C RID: 1036
		public const string AllDetectedTypes = "AllDetectedTypes";

		// Token: 0x0400040D RID: 1037
		public const string Examples = "Examples";

		// Token: 0x0400040E RID: 1038
		public const string ExamplesAnonymized = "ExamplesAnonymized";

		// Token: 0x0400040F RID: 1039
		public const string TranslatedProgram = "TranslatedProgram";

		// Token: 0x04000410 RID: 1040
		public const string TranslatedProgramAnonymized = "TranslatedProgramAnonymized";

		// Token: 0x04000411 RID: 1041
		public const string TranslatedProgramFailureReason = "TranslatedProgramFailureReason";

		// Token: 0x04000412 RID: 1042
		public const string TranslatedProgramTarget = "TranslatedProgramTarget";

		// Token: 0x04000413 RID: 1043
		public const string TranslatedProgramSuppressReason = "TranslatedProgramSuppressReason";

		// Token: 0x04000414 RID: 1044
		public const string TopProgramString = "TopProgramString";

		// Token: 0x04000415 RID: 1045
		public const string Features = "Features";

		// Token: 0x04000416 RID: 1046
		public const string IntentName = "IntentName";

		// Token: 0x04000417 RID: 1047
		public const string IntentFlagNames = "IntentFlagNames";
	}
}
