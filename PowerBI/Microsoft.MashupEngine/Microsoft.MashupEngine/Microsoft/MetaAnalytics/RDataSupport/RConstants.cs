using System;

namespace Microsoft.MetaAnalytics.RDataSupport
{
	// Token: 0x0200016B RID: 363
	internal static class RConstants
	{
		// Token: 0x04000408 RID: 1032
		internal const int FileVersion = 2;

		// Token: 0x04000409 RID: 1033
		internal const byte AsciiFormat = 65;

		// Token: 0x0400040A RID: 1034
		internal const byte NativeBinaryFormat = 66;

		// Token: 0x0400040B RID: 1035
		internal const byte XdrFormat = 88;

		// Token: 0x0400040C RID: 1036
		internal const int ReaderVersion = 131840;

		// Token: 0x0400040D RID: 1037
		internal const int WriterVersion = 134915;

		// Token: 0x0400040E RID: 1038
		internal const int PosixLtYearOffset = 1900;

		// Token: 0x0400040F RID: 1039
		internal const int PosixLtMonthOffset = 1;

		// Token: 0x04000410 RID: 1040
		internal const string Class = "class";

		// Token: 0x04000411 RID: 1041
		internal const string Names = "names";

		// Token: 0x04000412 RID: 1042
		internal const string RowNames = "row.names";

		// Token: 0x04000413 RID: 1043
		internal const string Levels = "levels";

		// Token: 0x04000414 RID: 1044
		internal const string Name = "name";

		// Token: 0x04000415 RID: 1045
		internal const string DataFrame = "data.frame";

		// Token: 0x04000416 RID: 1046
		internal const string DataTable = "data.table";

		// Token: 0x04000417 RID: 1047
		internal const string Factor = "factor";

		// Token: 0x04000418 RID: 1048
		internal const string PosixDateNumericClass = "POSIXct";

		// Token: 0x04000419 RID: 1049
		internal const string PosixDateListClass = "POSIXlt";

		// Token: 0x0400041A RID: 1050
		internal const string PosixDateClass = "POSIXt";

		// Token: 0x0400041B RID: 1051
		internal const string DateClass = "Date";

		// Token: 0x0400041C RID: 1052
		internal const string DiffTimeClass = "difftime";

		// Token: 0x0400041D RID: 1053
		internal const string DiffTimeClassUnits = "units";

		// Token: 0x0400041E RID: 1054
		internal const string DiffTimeClassSeconds = "secs";

		// Token: 0x0400041F RID: 1055
		internal const string Logical = "logical";

		// Token: 0x04000420 RID: 1056
		internal const string Complex = "complex";

		// Token: 0x04000421 RID: 1057
		internal const string Label = "label.type";

		// Token: 0x04000422 RID: 1058
		internal const string Score = "score.type";

		// Token: 0x04000423 RID: 1059
		internal const string FAttribute = "feature.type";

		// Token: 0x04000424 RID: 1060
		internal const string FChannel = "feature.channel";

		// Token: 0x04000425 RID: 1061
		internal static readonly byte[] WorkspaceHeader = new byte[] { 82, 68, 88, 50, 10 };

		// Token: 0x04000426 RID: 1062
		internal static readonly RObject RNull = new RObject<bool?>(new bool?[1], true, false);

		// Token: 0x04000427 RID: 1063
		internal static readonly RObject PosixClass = new RObject<string>(new string[] { "POSIXct", "POSIXt" }, true, false);
	}
}
