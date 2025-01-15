using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x0200052F RID: 1327
	public abstract class TextColumnBase : ITextColumn
	{
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x00058660 File Offset: 0x00056860
		// (set) Token: 0x06001DC8 RID: 7624 RVA: 0x00058668 File Offset: 0x00056868
		public bool AlignRight { get; set; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x00058671 File Offset: 0x00056871
		// (set) Token: 0x06001DCA RID: 7626 RVA: 0x00058679 File Offset: 0x00056879
		public bool First { get; set; }

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001DCB RID: 7627 RVA: 0x00058682 File Offset: 0x00056882
		// (set) Token: 0x06001DCC RID: 7628 RVA: 0x0005868A File Offset: 0x0005688A
		public string Heading { get; set; } = string.Empty;

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001DCD RID: 7629 RVA: 0x00058693 File Offset: 0x00056893
		// (set) Token: 0x06001DCE RID: 7630 RVA: 0x0005869B File Offset: 0x0005689B
		public int Index { get; set; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001DCF RID: 7631 RVA: 0x000586A4 File Offset: 0x000568A4
		// (set) Token: 0x06001DD0 RID: 7632 RVA: 0x000586AC File Offset: 0x000568AC
		public bool Last { get; set; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x000586B5 File Offset: 0x000568B5
		// (set) Token: 0x06001DD2 RID: 7634 RVA: 0x000586BD File Offset: 0x000568BD
		public int Width { get; set; }

		// Token: 0x06001DD3 RID: 7635
		public abstract IReadOnlyList<string> Lines(ITextRow row);

		// Token: 0x06001DD4 RID: 7636 RVA: 0x000586C8 File Offset: 0x000568C8
		public virtual void Render(StringBuilder output, ITextRow row, int lineIndex)
		{
			if (row is HeadingTextRow)
			{
				output.Append(this.AlignRight ? this.Heading.PadLeft(this.Width) : this.Heading.PadRight(this.Width));
				return;
			}
			IBorderTextRow borderTextRow = row as IBorderTextRow;
			if (borderTextRow != null)
			{
				output.Append(borderTextRow.Dash, this.Width);
				return;
			}
			IReadOnlyList<string> readOnlyList = this.Lines(row);
			string text = ((0 <= lineIndex && lineIndex < readOnlyList.Count) ? (readOnlyList[lineIndex] ?? string.Empty) : string.Empty);
			output.Append(this.AlignRight ? text.PadLeft(this.Width) : text.PadRight(this.Width));
		}

		// Token: 0x04000E62 RID: 3682
		public static char Space = ' ';
	}
}
