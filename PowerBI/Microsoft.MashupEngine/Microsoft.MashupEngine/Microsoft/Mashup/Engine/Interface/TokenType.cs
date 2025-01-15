using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000127 RID: 295
	public enum TokenType : byte
	{
		// Token: 0x040002D8 RID: 728
		Bof,
		// Token: 0x040002D9 RID: 729
		Eof,
		// Token: 0x040002DA RID: 730
		TokenError,
		// Token: 0x040002DB RID: 731
		Identifier,
		// Token: 0x040002DC RID: 732
		Literal,
		// Token: 0x040002DD RID: 733
		Ampersand,
		// Token: 0x040002DE RID: 734
		At,
		// Token: 0x040002DF RID: 735
		Bang,
		// Token: 0x040002E0 RID: 736
		Comma,
		// Token: 0x040002E1 RID: 737
		Divide,
		// Token: 0x040002E2 RID: 738
		DotDot,
		// Token: 0x040002E3 RID: 739
		Ellipsis,
		// Token: 0x040002E4 RID: 740
		Equal,
		// Token: 0x040002E5 RID: 741
		GoesTo,
		// Token: 0x040002E6 RID: 742
		GreaterThan,
		// Token: 0x040002E7 RID: 743
		GreaterThanOrEqual,
		// Token: 0x040002E8 RID: 744
		LeftBrace,
		// Token: 0x040002E9 RID: 745
		LeftBracket,
		// Token: 0x040002EA RID: 746
		LeftParen,
		// Token: 0x040002EB RID: 747
		LessThan,
		// Token: 0x040002EC RID: 748
		LessThanOrEqual,
		// Token: 0x040002ED RID: 749
		Minus,
		// Token: 0x040002EE RID: 750
		Multiply,
		// Token: 0x040002EF RID: 751
		NotEqual,
		// Token: 0x040002F0 RID: 752
		Plus,
		// Token: 0x040002F1 RID: 753
		QuestionMark,
		// Token: 0x040002F2 RID: 754
		RightBrace,
		// Token: 0x040002F3 RID: 755
		RightBracket,
		// Token: 0x040002F4 RID: 756
		RightParen,
		// Token: 0x040002F5 RID: 757
		Semicolon,
		// Token: 0x040002F6 RID: 758
		As,
		// Token: 0x040002F7 RID: 759
		Else,
		// Token: 0x040002F8 RID: 760
		Error,
		// Token: 0x040002F9 RID: 761
		HashShared,
		// Token: 0x040002FA RID: 762
		HashSections,
		// Token: 0x040002FB RID: 763
		If,
		// Token: 0x040002FC RID: 764
		In,
		// Token: 0x040002FD RID: 765
		Is,
		// Token: 0x040002FE RID: 766
		Let,
		// Token: 0x040002FF RID: 767
		LogicalAnd,
		// Token: 0x04000300 RID: 768
		LogicalOr,
		// Token: 0x04000301 RID: 769
		Meta,
		// Token: 0x04000302 RID: 770
		Not,
		// Token: 0x04000303 RID: 771
		Otherwise,
		// Token: 0x04000304 RID: 772
		Section,
		// Token: 0x04000305 RID: 773
		Shared,
		// Token: 0x04000306 RID: 774
		Then,
		// Token: 0x04000307 RID: 775
		Try,
		// Token: 0x04000308 RID: 776
		Type,
		// Token: 0x04000309 RID: 777
		Each,
		// Token: 0x0400030A RID: 778
		Whitespace,
		// Token: 0x0400030B RID: 779
		Verbatim,
		// Token: 0x0400030C RID: 780
		Coalesce
	}
}
