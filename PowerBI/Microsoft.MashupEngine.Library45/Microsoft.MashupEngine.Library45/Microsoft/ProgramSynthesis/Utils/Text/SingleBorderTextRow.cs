using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x0200053C RID: 1340
	public class SingleBorderTextRow : IBorderTextRow, ITextRow
	{
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001E43 RID: 7747 RVA: 0x00059017 File Offset: 0x00057217
		public char Dash
		{
			get
			{
				return '─';
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x0005901E File Offset: 0x0005721E
		// (set) Token: 0x06001E45 RID: 7749 RVA: 0x00059026 File Offset: 0x00057226
		public bool External { get; set; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x0005902F File Offset: 0x0005722F
		// (set) Token: 0x06001E47 RID: 7751 RVA: 0x00059037 File Offset: 0x00057237
		public bool First { get; set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x00059040 File Offset: 0x00057240
		// (set) Token: 0x06001E49 RID: 7753 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public int? Id
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x00059056 File Offset: 0x00057256
		// (set) Token: 0x06001E4B RID: 7755 RVA: 0x0005905E File Offset: 0x0005725E
		public int Index { get; set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x00059067 File Offset: 0x00057267
		// (set) Token: 0x06001E4D RID: 7757 RVA: 0x0005906F File Offset: 0x0005726F
		public bool Last { get; set; }

		// Token: 0x06001E4E RID: 7758 RVA: 0x00059078 File Offset: 0x00057278
		public char DoubleJunction(ITextColumn col, ITextRow row)
		{
			return SingleBorderTextRow.DoubleJunctionChars[row.First ? 0 : (row.Last ? 2 : 1), col.First ? 0 : (col.Last ? 2 : 1)];
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x000590B4 File Offset: 0x000572B4
		public void Render(StringBuilder output, IReadOnlyList<ITextColumn> columns)
		{
			foreach (ITextColumn textColumn in columns)
			{
				textColumn.Render(output, this, 0);
			}
			output.AppendLine();
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x00059104 File Offset: 0x00057304
		public char SingleJunction(ITextColumn col, ITextRow row)
		{
			return SingleBorderTextRow.SingleJunctionChars[row.First ? 0 : (row.Last ? 2 : 1), col.First ? 0 : (col.Last ? 2 : 1)];
		}

		// Token: 0x04000E8E RID: 3726
		public const char DashChar = '─';

		// Token: 0x04000E8F RID: 3727
		private static readonly char[,] DoubleJunctionChars = new char[,]
		{
			{ '╓', '╥', '╖' },
			{ '╟', '╫', '╢' },
			{ '╙', '╨', '╜' }
		};

		// Token: 0x04000E90 RID: 3728
		private static readonly char[,] SingleJunctionChars = new char[,]
		{
			{ '┌', '┬', '┐' },
			{ '├', '┼', '┤' },
			{ '└', '┴', '┘' }
		};
	}
}
