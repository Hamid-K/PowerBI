using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200011F RID: 287
	internal enum CommandTreeTranslationErrorCode
	{
		// Token: 0x04000A68 RID: 2664
		Unexpected,
		// Token: 0x04000A69 RID: 2665
		UnsupportedStringMinMaxColumn,
		// Token: 0x04000A6A RID: 2666
		UnsupportedStringMinMaxExpression,
		// Token: 0x04000A6B RID: 2667
		InvalidFilterExceedsMaxNumberOfValuesForInFilter,
		// Token: 0x04000A6C RID: 2668
		InvalidFilterExceedsMaxNumberOfValuesForInFilterTreeRewrite,
		// Token: 0x04000A6D RID: 2669
		InvalidDaxExternalContent,
		// Token: 0x04000A6E RID: 2670
		InvalidInFilterWithDuplicateColumns,
		// Token: 0x04000A6F RID: 2671
		DuplicateUnqualifiedColumnName
	}
}
