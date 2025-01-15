using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics
{
	// Token: 0x020015FC RID: 5628
	internal class RuleId
	{
		// Token: 0x040046AE RID: 18094
		public const string LetX = "LetX";

		// Token: 0x040046AF RID: 18095
		public const string AddRightNumber = "AddRightNumber";

		// Token: 0x040046B0 RID: 18096
		public const string SubtractRightNumber = "SubtractRightNumber";

		// Token: 0x040046B1 RID: 18097
		public const string MultiplyRightNumber = "MultiplyRightNumber";

		// Token: 0x040046B2 RID: 18098
		public const string DivideRightNumber = "DivideRightNumber";

		// Token: 0x040046B3 RID: 18099
		public const string Trim = "Trim";

		// Token: 0x040046B4 RID: 18100
		public const string TrimFull = "TrimFull";

		// Token: 0x040046B5 RID: 18101
		public const string TrimFullSlice = "TrimFullSlice";

		// Token: 0x040046B6 RID: 18102
		public const string TrimFullSplit = "TrimFullSplit";

		// Token: 0x040046B7 RID: 18103
		public const string TrimSlice = "TrimSlice";

		// Token: 0x040046B8 RID: 18104
		public const string TrimSplit = "TrimSplit";

		// Token: 0x040046B9 RID: 18105
		public const string LowerCase = "LowerCase";

		// Token: 0x040046BA RID: 18106
		public const string LowerCaseConcat = "LowerCaseConcat";

		// Token: 0x040046BB RID: 18107
		public const string UpperCase = "UpperCase";

		// Token: 0x040046BC RID: 18108
		public const string UpperCaseConcat = "UpperCaseConcat";

		// Token: 0x040046BD RID: 18109
		public const string ProperCase = "ProperCase";

		// Token: 0x040046BE RID: 18110
		public const string ProperCaseConcat = "ProperCaseConcat";

		// Token: 0x040046BF RID: 18111
		public static readonly string[] AllLowerCase = new string[] { "LowerCase", "LowerCaseConcat" };

		// Token: 0x040046C0 RID: 18112
		public static readonly string[] AllUpperCase = new string[] { "UpperCase", "UpperCaseConcat" };

		// Token: 0x040046C1 RID: 18113
		public static readonly string[] AllProperCase = new string[] { "ProperCase", "ProperCaseConcat" };

		// Token: 0x040046C2 RID: 18114
		public static readonly string[] AllCase = new string[] { "LowerCase", "LowerCaseConcat", "UpperCase", "UpperCaseConcat", "ProperCase", "ProperCaseConcat" };

		// Token: 0x040046C3 RID: 18115
		public static readonly string[] AllTrim = new string[] { "Trim", "TrimSplit", "TrimSlice", "TrimFull", "TrimFullSlice", "TrimFullSplit" };
	}
}
