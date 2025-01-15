using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002D32 RID: 11570
	[GeneratedCode("DomGen", "2.0")]
	internal enum CellValues
	{
		// Token: 0x0400A3F9 RID: 41977
		[EnumString("b")]
		Boolean,
		// Token: 0x0400A3FA RID: 41978
		[EnumString("n")]
		Number,
		// Token: 0x0400A3FB RID: 41979
		[EnumString("e")]
		Error,
		// Token: 0x0400A3FC RID: 41980
		[EnumString("s")]
		SharedString,
		// Token: 0x0400A3FD RID: 41981
		[EnumString("str")]
		String,
		// Token: 0x0400A3FE RID: 41982
		[EnumString("inlineStr")]
		InlineString,
		// Token: 0x0400A3FF RID: 41983
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[EnumString("d")]
		Date
	}
}
