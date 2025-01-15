using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x0200013A RID: 314
	[Flags]
	public enum TmdlExpressionTrimStyle
	{
		// Token: 0x0400036B RID: 875
		NoTrim = 0,
		// Token: 0x0400036C RID: 876
		TrimTrailingWhitespaces = 1,
		// Token: 0x0400036D RID: 877
		TrimLeadingCommonWhitespaces = 2
	}
}
