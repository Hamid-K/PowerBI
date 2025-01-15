using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000137 RID: 311
	internal enum DaxInvalidExternalContentErrorCode
	{
		// Token: 0x04000AB0 RID: 2736
		None,
		// Token: 0x04000AB1 RID: 2737
		UnclosedBracketIdentifier,
		// Token: 0x04000AB2 RID: 2738
		UnclosedMultiLineComment,
		// Token: 0x04000AB3 RID: 2739
		UnclosedParenthesis,
		// Token: 0x04000AB4 RID: 2740
		UnclosedQuoteIdentifier,
		// Token: 0x04000AB5 RID: 2741
		UnclosedStringLiteral,
		// Token: 0x04000AB6 RID: 2742
		UnexpectedCloseParenthesis
	}
}
