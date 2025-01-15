using System;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000ECC RID: 3788
	public struct FillInfo
	{
		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06006709 RID: 26377 RVA: 0x0014FDD4 File Offset: 0x0014DFD4
		public readonly string FillStyle { get; }

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x0600670A RID: 26378 RVA: 0x0014FDDC File Offset: 0x0014DFDC
		public readonly ColorInfo FgColor { get; }

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x0600670B RID: 26379 RVA: 0x0014FDE4 File Offset: 0x0014DFE4
		public readonly ColorInfo BgColor { get; }

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x0600670C RID: 26380 RVA: 0x0014FDEC File Offset: 0x0014DFEC
		public readonly bool IsTrivialFill { get; }

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x0600670D RID: 26381 RVA: 0x0014FDF4 File Offset: 0x0014DFF4
		public readonly bool IsActualFill { get; }

		// Token: 0x0600670E RID: 26382 RVA: 0x0014FDFC File Offset: 0x0014DFFC
		public FillInfo(string fillStyle, ColorInfo fgColor, ColorInfo bgColor)
		{
			this.FillStyle = fillStyle;
			this.FgColor = fgColor;
			this.BgColor = bgColor;
			this.IsTrivialFill = this.FillStyle == "solid" && this.FgColor.IsWhite;
			this.IsActualFill = !this.IsTrivialFill && this.FillStyle != null && this.FillStyle != "none";
		}

		// Token: 0x0600670F RID: 26383 RVA: 0x0014FE6D File Offset: 0x0014E06D
		public override string ToString()
		{
			if (this.FillStyle != null)
			{
				return "Fill(" + this.FillStyle + ")";
			}
			return "(null)";
		}
	}
}
