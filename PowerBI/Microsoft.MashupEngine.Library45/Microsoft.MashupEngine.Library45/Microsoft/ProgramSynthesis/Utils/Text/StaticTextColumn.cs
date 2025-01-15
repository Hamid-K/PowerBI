using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x02000535 RID: 1333
	public class StaticTextColumn : TextColumnBase
	{
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x00058C65 File Offset: 0x00056E65
		// (set) Token: 0x06001E04 RID: 7684 RVA: 0x00058C6D File Offset: 0x00056E6D
		public bool ShowInHeading { get; set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x00058C76 File Offset: 0x00056E76
		// (set) Token: 0x06001E06 RID: 7686 RVA: 0x00058C7E File Offset: 0x00056E7E
		public bool ShowInRow { get; set; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x00058C87 File Offset: 0x00056E87
		// (set) Token: 0x06001E08 RID: 7688 RVA: 0x00058C8F File Offset: 0x00056E8F
		public bool ShowInRowExtraLines { get; set; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x00058C98 File Offset: 0x00056E98
		// (set) Token: 0x06001E0A RID: 7690 RVA: 0x00058CA0 File Offset: 0x00056EA0
		public string Text { get; set; }

		// Token: 0x06001E0B RID: 7691 RVA: 0x00058CA9 File Offset: 0x00056EA9
		public virtual string FormatValue(ITextRow row)
		{
			return this.Text;
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x00058CB4 File Offset: 0x00056EB4
		public override IReadOnlyList<string> Lines(ITextRow row)
		{
			IReadOnlyList<string> readOnlyList;
			if ((readOnlyList = this._lines) == null)
			{
				readOnlyList = (this._lines = new string[] { this.Text });
			}
			return readOnlyList;
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00058CE4 File Offset: 0x00056EE4
		public override void Render(StringBuilder output, ITextRow row, int lineIndex)
		{
			string text = new string(TextColumnBase.Space, base.Width);
			string text2 = this.FormatValue(row);
			if (row is HeadingTextRow)
			{
				output.Append(this.ShowInHeading ? text2 : text);
				return;
			}
			IBorderTextRow borderTextRow = row as IBorderTextRow;
			if (borderTextRow != null)
			{
				output.Append(borderTextRow.Dash, base.Width);
				return;
			}
			output.Append((lineIndex > 0 && !this.ShowInRowExtraLines) ? text : (this.ShowInRow ? text2 : text));
		}

		// Token: 0x04000E7B RID: 3707
		private IReadOnlyList<string> _lines;
	}
}
