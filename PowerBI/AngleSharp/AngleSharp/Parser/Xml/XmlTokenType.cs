using System;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x0200005E RID: 94
	internal enum XmlTokenType : byte
	{
		// Token: 0x0400020F RID: 527
		Doctype,
		// Token: 0x04000210 RID: 528
		Declaration,
		// Token: 0x04000211 RID: 529
		StartTag,
		// Token: 0x04000212 RID: 530
		EndTag,
		// Token: 0x04000213 RID: 531
		Comment,
		// Token: 0x04000214 RID: 532
		CData,
		// Token: 0x04000215 RID: 533
		Character,
		// Token: 0x04000216 RID: 534
		CharacterReference,
		// Token: 0x04000217 RID: 535
		ProcessingInstruction,
		// Token: 0x04000218 RID: 536
		EndOfFile
	}
}
