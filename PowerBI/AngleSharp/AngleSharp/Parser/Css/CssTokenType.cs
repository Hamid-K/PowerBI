using System;

namespace AngleSharp.Parser.Css
{
	// Token: 0x0200007F RID: 127
	internal enum CssTokenType : byte
	{
		// Token: 0x0400030E RID: 782
		String,
		// Token: 0x0400030F RID: 783
		Url,
		// Token: 0x04000310 RID: 784
		Color,
		// Token: 0x04000311 RID: 785
		Hash,
		// Token: 0x04000312 RID: 786
		Comment,
		// Token: 0x04000313 RID: 787
		AtKeyword,
		// Token: 0x04000314 RID: 788
		Ident,
		// Token: 0x04000315 RID: 789
		Function,
		// Token: 0x04000316 RID: 790
		Number,
		// Token: 0x04000317 RID: 791
		Percentage,
		// Token: 0x04000318 RID: 792
		Dimension,
		// Token: 0x04000319 RID: 793
		Range,
		// Token: 0x0400031A RID: 794
		Cdo,
		// Token: 0x0400031B RID: 795
		Cdc,
		// Token: 0x0400031C RID: 796
		Column,
		// Token: 0x0400031D RID: 797
		Delim,
		// Token: 0x0400031E RID: 798
		Match,
		// Token: 0x0400031F RID: 799
		RoundBracketOpen,
		// Token: 0x04000320 RID: 800
		RoundBracketClose,
		// Token: 0x04000321 RID: 801
		CurlyBracketOpen,
		// Token: 0x04000322 RID: 802
		CurlyBracketClose,
		// Token: 0x04000323 RID: 803
		SquareBracketOpen,
		// Token: 0x04000324 RID: 804
		SquareBracketClose,
		// Token: 0x04000325 RID: 805
		Colon,
		// Token: 0x04000326 RID: 806
		Comma,
		// Token: 0x04000327 RID: 807
		Semicolon,
		// Token: 0x04000328 RID: 808
		Whitespace,
		// Token: 0x04000329 RID: 809
		EndOfFile
	}
}
