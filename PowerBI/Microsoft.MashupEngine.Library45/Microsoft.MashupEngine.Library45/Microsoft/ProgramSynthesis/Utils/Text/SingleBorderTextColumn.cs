using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x02000531 RID: 1329
	public class SingleBorderTextColumn : TextColumnBase, IBorderTextColumn, ITextColumn
	{
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x00058A49 File Offset: 0x00056C49
		// (set) Token: 0x06001DEB RID: 7659 RVA: 0x00058A51 File Offset: 0x00056C51
		public bool External { get; set; }

		// Token: 0x06001DEC RID: 7660 RVA: 0x00058A5C File Offset: 0x00056C5C
		public string FormatValue(ITextRow row)
		{
			return '│'.ToString();
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00058A78 File Offset: 0x00056C78
		public override IReadOnlyList<string> Lines(ITextRow row)
		{
			IReadOnlyList<string> readOnlyList;
			if ((readOnlyList = this._lines) == null)
			{
				readOnlyList = (this._lines = new string[] { this.FormatValue(row) });
			}
			return readOnlyList;
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x00058AA8 File Offset: 0x00056CA8
		public override void Render(StringBuilder output, ITextRow row, int lineIndex)
		{
			IBorderTextRow borderTextRow = row as IBorderTextRow;
			if (borderTextRow != null)
			{
				output.Append(borderTextRow.SingleJunction(this, row).ToString());
				return;
			}
			output.Append('│');
		}

		// Token: 0x04000E71 RID: 3697
		private const char VerticalBar = '│';

		// Token: 0x04000E72 RID: 3698
		private IReadOnlyList<string> _lines;
	}
}
