using System;
using AngleSharp.Attributes;

namespace AngleSharp.Parser.Css
{
	// Token: 0x02000079 RID: 121
	public enum CssParseError : byte
	{
		// Token: 0x040002DF RID: 735
		[DomDescription("Unexpected end of the given file.")]
		EOF,
		// Token: 0x040002E0 RID: 736
		[DomDescription("The provided character is not valid at the given position.")]
		InvalidCharacter = 16,
		// Token: 0x040002E1 RID: 737
		[DomDescription("No block can start at the current position.")]
		InvalidBlockStart,
		// Token: 0x040002E2 RID: 738
		[DomDescription("The given token is not valid at the current position.")]
		InvalidToken,
		// Token: 0x040002E3 RID: 739
		[DomDescription("An expected colon is missing.")]
		ColonMissing,
		// Token: 0x040002E4 RID: 740
		[DomDescription("An expected identifier could not be found.")]
		IdentExpected,
		// Token: 0x040002E5 RID: 741
		[DomDescription("An given input has not been expected.")]
		InputUnexpected,
		// Token: 0x040002E6 RID: 742
		[DomDescription("This position does not support a linebreak (LF, FF).")]
		LineBreakUnexpected,
		// Token: 0x040002E7 RID: 743
		[DomDescription("The name of the @-rule is unknown.")]
		UnknownAtRule = 32,
		// Token: 0x040002E8 RID: 744
		[DomDescription("The provided selector is invalid.")]
		InvalidSelector = 48,
		// Token: 0x040002E9 RID: 745
		[DomDescription("The provided keyframe selector is invalid.")]
		InvalidKeyframe,
		// Token: 0x040002EA RID: 746
		[DomDescription("The value of the declaration could not be found.")]
		ValueMissing = 64,
		// Token: 0x040002EB RID: 747
		[DomDescription("The value is invalid and cannot be used.")]
		InvalidValue,
		// Token: 0x040002EC RID: 748
		[DomDescription("The name of the declaration is unknown.")]
		UnknownDeclarationName = 80
	}
}
