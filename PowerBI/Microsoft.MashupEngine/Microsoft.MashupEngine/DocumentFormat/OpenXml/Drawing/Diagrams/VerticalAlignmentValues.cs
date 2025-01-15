using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x020026CA RID: 9930
	[GeneratedCode("DomGen", "2.0")]
	internal enum VerticalAlignmentValues
	{
		// Token: 0x040083B2 RID: 33714
		[EnumString("t")]
		Top,
		// Token: 0x040083B3 RID: 33715
		[EnumString("mid")]
		Middle,
		// Token: 0x040083B4 RID: 33716
		[EnumString("b")]
		Bottom,
		// Token: 0x040083B5 RID: 33717
		[EnumString("none")]
		None,
		// Token: 0x040083B6 RID: 33718
		[EnumString("top")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		Top2010,
		// Token: 0x040083B7 RID: 33719
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[EnumString("center")]
		Middle2010,
		// Token: 0x040083B8 RID: 33720
		[EnumString("bottom")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		Bottom2010
	}
}
