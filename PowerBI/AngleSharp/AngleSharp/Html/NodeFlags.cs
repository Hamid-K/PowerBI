using System;

namespace AngleSharp.Html
{
	// Token: 0x020000BE RID: 190
	[Flags]
	internal enum NodeFlags : uint
	{
		// Token: 0x04000516 RID: 1302
		None = 0U,
		// Token: 0x04000517 RID: 1303
		SelfClosing = 1U,
		// Token: 0x04000518 RID: 1304
		Special = 2U,
		// Token: 0x04000519 RID: 1305
		LiteralText = 4U,
		// Token: 0x0400051A RID: 1306
		LineTolerance = 8U,
		// Token: 0x0400051B RID: 1307
		ImplicitelyClosed = 16U,
		// Token: 0x0400051C RID: 1308
		ImpliedEnd = 32U,
		// Token: 0x0400051D RID: 1309
		Scoped = 64U,
		// Token: 0x0400051E RID: 1310
		HtmlMember = 256U,
		// Token: 0x0400051F RID: 1311
		HtmlTip = 512U,
		// Token: 0x04000520 RID: 1312
		HtmlFormatting = 2048U,
		// Token: 0x04000521 RID: 1313
		HtmlListScoped = 4096U,
		// Token: 0x04000522 RID: 1314
		HtmlSelectScoped = 8192U,
		// Token: 0x04000523 RID: 1315
		HtmlTableSectionScoped = 16384U,
		// Token: 0x04000524 RID: 1316
		HtmlTableScoped = 32768U,
		// Token: 0x04000525 RID: 1317
		MathMember = 65536U,
		// Token: 0x04000526 RID: 1318
		MathTip = 131072U,
		// Token: 0x04000527 RID: 1319
		SvgMember = 16777216U,
		// Token: 0x04000528 RID: 1320
		SvgTip = 33554432U
	}
}
