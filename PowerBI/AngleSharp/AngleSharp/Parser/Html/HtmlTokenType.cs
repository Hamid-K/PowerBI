using System;

namespace AngleSharp.Parser.Html
{
	// Token: 0x02000072 RID: 114
	public enum HtmlTokenType : byte
	{
		// Token: 0x040002AD RID: 685
		Doctype,
		// Token: 0x040002AE RID: 686
		StartTag,
		// Token: 0x040002AF RID: 687
		EndTag,
		// Token: 0x040002B0 RID: 688
		Comment,
		// Token: 0x040002B1 RID: 689
		Character,
		// Token: 0x040002B2 RID: 690
		EndOfFile
	}
}
