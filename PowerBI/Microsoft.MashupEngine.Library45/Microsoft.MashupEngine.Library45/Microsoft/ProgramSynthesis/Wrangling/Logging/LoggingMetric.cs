using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Logging
{
	// Token: 0x02000178 RID: 376
	public static class LoggingMetric
	{
		// Token: 0x04000399 RID: 921
		public const string LearnTime = "LearnTime";

		// Token: 0x0400039A RID: 922
		public const string TotalTime = "TotalTime";

		// Token: 0x0400039B RID: 923
		public const string SignificantInputsTime = "SignificantInputsTime";

		// Token: 0x0400039C RID: 924
		public const string ProgramSampleTime = "ProgramSampleTime";

		// Token: 0x0400039D RID: 925
		public const string NumProgramsRequested = "NumProgramsRequested";

		// Token: 0x0400039E RID: 926
		public const string NumProgramsLearned = "NumProgramsLearned";

		// Token: 0x0400039F RID: 927
		public const string NumInputs = "NumInputs";

		// Token: 0x040003A0 RID: 928
		public const string NumStringInputs = "NumStringInputs";

		// Token: 0x040003A1 RID: 929
		public const string NumIntInputs = "NumIntInputs";

		// Token: 0x040003A2 RID: 930
		public const string NumDatetimeInputs = "NumDatetimeInputs";

		// Token: 0x040003A3 RID: 931
		public const string NumDoubleInputs = "NumDoubleInputs";

		// Token: 0x040003A4 RID: 932
		public const string NumNullInputs = "NumNullInputs";

		// Token: 0x040003A5 RID: 933
		public const string NumEmptyInputs = "NumEmptyInputs";

		// Token: 0x040003A6 RID: 934
		public const string NumConstraints = "NumConstraints";

		// Token: 0x040003A7 RID: 935
		public const string NumConflicts = "NumConflicts";

		// Token: 0x040003A8 RID: 936
		public const string NumExamples = "NumExamples";

		// Token: 0x040003A9 RID: 937
		public const string NumSignificantInputs = "NumSignificantInputs";

		// Token: 0x040003AA RID: 938
		public const string NumInputColumns = "NumInputColumns";

		// Token: 0x040003AB RID: 939
		public const string NumColumnsUsed = "NumColumnsUsed";

		// Token: 0x040003AC RID: 940
		public const string NumBranches = "NumBranches";

		// Token: 0x040003AD RID: 941
		public const string NumDistinctTokens = "NumDistinctTokens";

		// Token: 0x040003AE RID: 942
		public const string MaxNumAtoms = "MaxNumAtoms";

		// Token: 0x040003AF RID: 943
		public const string MinInputLength = "MinInputLength";

		// Token: 0x040003B0 RID: 944
		public const string MaxInputLength = "MaxInputLength";

		// Token: 0x040003B1 RID: 945
		public const string MeanInputLength = "MeanInputLength";

		// Token: 0x040003B2 RID: 946
		public const string MedianInputLength = "MedianInputLength";

		// Token: 0x040003B3 RID: 947
		public const string StdDevInputLength = "StdDevInputLength";

		// Token: 0x040003B4 RID: 948
		public const string MinOutputLength = "MinOutputLength";

		// Token: 0x040003B5 RID: 949
		public const string MaxOutputLength = "MaxOutputLength";

		// Token: 0x040003B6 RID: 950
		public const string MeanOutputLength = "MeanOutputLength";

		// Token: 0x040003B7 RID: 951
		public const string MedianOutputLength = "MedianOutputLength";

		// Token: 0x040003B8 RID: 952
		public const string StdDevOutputLength = "StdDevOutputLength";

		// Token: 0x040003B9 RID: 953
		public const string AutoCompleterEntityExtractionTimeInMs = "AutoCompleterEntityExtractionTimeInMs";

		// Token: 0x040003BA RID: 954
		public const string AutoCompleterTreeBuildTimeInMs = "AutoCompleterTreeBuildTimeInMs";

		// Token: 0x040003BB RID: 955
		public const string AutoCompleterTotalCreateTimeInMs = "AutoCompleterTotalCreateTimeInMs";

		// Token: 0x040003BC RID: 956
		public const string AutoCompleterSuggestTimeInMs = "AutoCompleterSuggestTimeInMs";

		// Token: 0x040003BD RID: 957
		public const string FilterValueCompleterTimeInMs = "FilterValueCompleterTimeInMs";

		// Token: 0x040003BE RID: 958
		public const string FilterValueCompleterSuggestionsCount = "FilterValueCompleterSuggestionsCount";

		// Token: 0x040003BF RID: 959
		public const string CxCodeLineCount = "CxCodeLineCount";

		// Token: 0x040003C0 RID: 960
		public const string CxCodeTotalLineCount = "CxCodeTotalLineCount";

		// Token: 0x040003C1 RID: 961
		public const string CxCodeLength = "CxCodeLength";

		// Token: 0x040003C2 RID: 962
		public const string CxLinesToLearn = "CxLinesToLearn";

		// Token: 0x040003C3 RID: 963
		public const string CxInputLength = "CxInputLength";

		// Token: 0x040003C4 RID: 964
		public const string CxInputNameLength = "CxInputNameLength";

		// Token: 0x040003C5 RID: 965
		public const string CxDataRows = "CxDataRows";

		// Token: 0x040003C6 RID: 966
		public const string CxColumnCount = "CxColumnCount";

		// Token: 0x040003C7 RID: 967
		public const string CxDelimiterLength = "CxDelimiterLength";

		// Token: 0x040003C8 RID: 968
		public const string TranslateTime = "TranslateTime";

		// Token: 0x040003C9 RID: 969
		public const string Success = "Success";
	}
}
