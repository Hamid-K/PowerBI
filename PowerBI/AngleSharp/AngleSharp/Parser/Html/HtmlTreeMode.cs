using System;

namespace AngleSharp.Parser.Html
{
	// Token: 0x02000073 RID: 115
	internal enum HtmlTreeMode : byte
	{
		// Token: 0x040002B4 RID: 692
		Initial,
		// Token: 0x040002B5 RID: 693
		BeforeHtml,
		// Token: 0x040002B6 RID: 694
		BeforeHead,
		// Token: 0x040002B7 RID: 695
		InHead,
		// Token: 0x040002B8 RID: 696
		InHeadNoScript,
		// Token: 0x040002B9 RID: 697
		AfterHead,
		// Token: 0x040002BA RID: 698
		InBody,
		// Token: 0x040002BB RID: 699
		Text,
		// Token: 0x040002BC RID: 700
		InTable,
		// Token: 0x040002BD RID: 701
		InCaption,
		// Token: 0x040002BE RID: 702
		InColumnGroup,
		// Token: 0x040002BF RID: 703
		InTableBody,
		// Token: 0x040002C0 RID: 704
		InRow,
		// Token: 0x040002C1 RID: 705
		InCell,
		// Token: 0x040002C2 RID: 706
		InSelect,
		// Token: 0x040002C3 RID: 707
		InSelectInTable,
		// Token: 0x040002C4 RID: 708
		InTemplate,
		// Token: 0x040002C5 RID: 709
		AfterBody,
		// Token: 0x040002C6 RID: 710
		InFrameset,
		// Token: 0x040002C7 RID: 711
		AfterFrameset,
		// Token: 0x040002C8 RID: 712
		AfterAfterBody,
		// Token: 0x040002C9 RID: 713
		AfterAfterFrameset
	}
}
