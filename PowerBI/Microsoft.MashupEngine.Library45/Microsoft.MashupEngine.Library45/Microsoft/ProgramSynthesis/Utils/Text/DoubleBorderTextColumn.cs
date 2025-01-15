using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x02000532 RID: 1330
	public class DoubleBorderTextColumn : TextColumnBase, IBorderTextColumn, ITextColumn
	{
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x00058AEB File Offset: 0x00056CEB
		// (set) Token: 0x06001DF1 RID: 7665 RVA: 0x00058AF3 File Offset: 0x00056CF3
		public bool External { get; set; }

		// Token: 0x06001DF2 RID: 7666 RVA: 0x00058AFC File Offset: 0x00056CFC
		public virtual string FormatValue(ITextRow row)
		{
			return '║'.ToString();
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00058B18 File Offset: 0x00056D18
		public override IReadOnlyList<string> Lines(ITextRow row)
		{
			IReadOnlyList<string> readOnlyList;
			if ((readOnlyList = this._lines) == null)
			{
				readOnlyList = (this._lines = new string[] { this.FormatValue(row) });
			}
			return readOnlyList;
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00058B48 File Offset: 0x00056D48
		public override void Render(StringBuilder output, ITextRow row, int lineIndex)
		{
			IBorderTextRow borderTextRow = row as IBorderTextRow;
			if (borderTextRow != null)
			{
				output.Append(borderTextRow.DoubleJunction(this, row).ToString());
				return;
			}
			output.Append('║');
		}

		// Token: 0x04000E74 RID: 3700
		private const char VerticalBar = '║';

		// Token: 0x04000E75 RID: 3701
		private IReadOnlyList<string> _lines;
	}
}
