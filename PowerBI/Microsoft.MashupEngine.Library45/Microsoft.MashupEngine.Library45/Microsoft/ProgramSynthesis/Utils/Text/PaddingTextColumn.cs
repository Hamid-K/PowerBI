using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x02000534 RID: 1332
	public class PaddingTextColumn : TextColumnBase
	{
		// Token: 0x06001E00 RID: 7680 RVA: 0x00058BFD File Offset: 0x00056DFD
		public PaddingTextColumn(int width)
		{
			this._width = width;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x00058C0C File Offset: 0x00056E0C
		public override IReadOnlyList<string> Lines(ITextRow row)
		{
			return new string[]
			{
				new string(TextColumnBase.Space, this._width)
			};
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x00058C28 File Offset: 0x00056E28
		public override void Render(StringBuilder output, ITextRow row, int lineIndex)
		{
			IBorderTextRow borderTextRow = row as IBorderTextRow;
			if (borderTextRow != null)
			{
				output.Append(borderTextRow.Dash, base.Width);
				return;
			}
			output.Append(TextColumnBase.Space, this._width);
		}

		// Token: 0x04000E7A RID: 3706
		private readonly int _width;
	}
}
