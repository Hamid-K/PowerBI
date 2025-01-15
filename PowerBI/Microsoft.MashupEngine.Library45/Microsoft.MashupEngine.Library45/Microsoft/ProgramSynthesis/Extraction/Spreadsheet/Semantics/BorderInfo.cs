using System;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000ECB RID: 3787
	public struct BorderInfo
	{
		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06006705 RID: 26373 RVA: 0x0014FD8E File Offset: 0x0014DF8E
		public readonly string BorderStyle { get; }

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06006706 RID: 26374 RVA: 0x0014FD96 File Offset: 0x0014DF96
		public readonly ColorInfo Color { get; }

		// Token: 0x06006707 RID: 26375 RVA: 0x0014FD9E File Offset: 0x0014DF9E
		public BorderInfo(string borderStyle, ColorInfo color)
		{
			this.BorderStyle = borderStyle;
			this.Color = color;
		}

		// Token: 0x06006708 RID: 26376 RVA: 0x0014FDAE File Offset: 0x0014DFAE
		public override string ToString()
		{
			if (this.BorderStyle != null)
			{
				return string.Format("Border({0}, {1})", this.BorderStyle, this.Color);
			}
			return "(null)";
		}
	}
}
