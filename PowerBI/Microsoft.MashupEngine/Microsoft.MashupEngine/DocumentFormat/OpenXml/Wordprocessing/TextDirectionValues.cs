using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x0200306A RID: 12394
	[GeneratedCode("DomGen", "2.0")]
	internal enum TextDirectionValues
	{
		// Token: 0x0400B118 RID: 45336
		[EnumString("lrTb")]
		LefToRightTopToBottom,
		// Token: 0x0400B119 RID: 45337
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[EnumString("tb")]
		LeftToRightTopToBottom2010,
		// Token: 0x0400B11A RID: 45338
		[EnumString("tbRl")]
		TopToBottomRightToLeft,
		// Token: 0x0400B11B RID: 45339
		[EnumString("rl")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		TopToBottomRightToLeft2010,
		// Token: 0x0400B11C RID: 45340
		[EnumString("btLr")]
		BottomToTopLeftToRight,
		// Token: 0x0400B11D RID: 45341
		[EnumString("lr")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		BottomToTopLeftToRight2010,
		// Token: 0x0400B11E RID: 45342
		[EnumString("lrTbV")]
		LefttoRightTopToBottomRotated,
		// Token: 0x0400B11F RID: 45343
		[EnumString("tbV")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		LeftToRightTopToBottomRotated2010,
		// Token: 0x0400B120 RID: 45344
		[EnumString("tbRlV")]
		TopToBottomRightToLeftRotated,
		// Token: 0x0400B121 RID: 45345
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[EnumString("rlV")]
		TopToBottomRightToLeftRotated2010,
		// Token: 0x0400B122 RID: 45346
		[EnumString("tbLrV")]
		TopToBottomLeftToRightRotated,
		// Token: 0x0400B123 RID: 45347
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[EnumString("lrV")]
		TopToBottomLeftToRightRotated2010
	}
}
