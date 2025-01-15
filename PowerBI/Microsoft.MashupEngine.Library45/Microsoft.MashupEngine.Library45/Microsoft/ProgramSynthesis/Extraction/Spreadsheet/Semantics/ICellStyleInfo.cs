using System;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000ECE RID: 3790
	public interface ICellStyleInfo
	{
		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x0600671C RID: 26396
		string NumberFormat { get; }

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x0600671D RID: 26397
		string FontName { get; }

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x0600671E RID: 26398
		int? FontSize { get; }

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x0600671F RID: 26399
		ColorInfo Color { get; }

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06006720 RID: 26400
		bool Bold { get; }

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06006721 RID: 26401
		bool Italic { get; }

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06006722 RID: 26402
		bool Underline { get; }

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06006723 RID: 26403
		bool Strikethrough { get; }

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06006724 RID: 26404
		Directed<BorderInfo> Borders { get; }

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06006725 RID: 26405
		FillInfo Fill { get; }

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06006726 RID: 26406
		AxisAligned<string> Alignment { get; }
	}
}
