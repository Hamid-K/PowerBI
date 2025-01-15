using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200012E RID: 302
	internal enum DaxExternalContentTokenType
	{
		// Token: 0x04000A92 RID: 2706
		EndOfInput,
		// Token: 0x04000A93 RID: 2707
		OpenParen,
		// Token: 0x04000A94 RID: 2708
		CloseParen,
		// Token: 0x04000A95 RID: 2709
		OpenCurly,
		// Token: 0x04000A96 RID: 2710
		CloseCurly,
		// Token: 0x04000A97 RID: 2711
		String,
		// Token: 0x04000A98 RID: 2712
		StringStart,
		// Token: 0x04000A99 RID: 2713
		BracketedIdentifier,
		// Token: 0x04000A9A RID: 2714
		BracketedIdentifierStart,
		// Token: 0x04000A9B RID: 2715
		QuotedIdentifier,
		// Token: 0x04000A9C RID: 2716
		QuotedIdentifierStart,
		// Token: 0x04000A9D RID: 2717
		MultiLineComment,
		// Token: 0x04000A9E RID: 2718
		MultiLineCommentStart,
		// Token: 0x04000A9F RID: 2719
		SingleLineComment,
		// Token: 0x04000AA0 RID: 2720
		SQLComment
	}
}
