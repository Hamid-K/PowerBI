using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x0200053D RID: 1341
	public class DoubleBorderTextRow : IBorderTextRow, ITextRow
	{
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x0005916E File Offset: 0x0005736E
		public char Dash
		{
			get
			{
				return '═';
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x00059175 File Offset: 0x00057375
		// (set) Token: 0x06001E55 RID: 7765 RVA: 0x0005917D File Offset: 0x0005737D
		public bool External { get; set; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x00059186 File Offset: 0x00057386
		// (set) Token: 0x06001E57 RID: 7767 RVA: 0x0005918E File Offset: 0x0005738E
		public bool First { get; set; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x00059198 File Offset: 0x00057398
		// (set) Token: 0x06001E59 RID: 7769 RVA: 0x0000CC37 File Offset: 0x0000AE37
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

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x000591AE File Offset: 0x000573AE
		// (set) Token: 0x06001E5B RID: 7771 RVA: 0x000591B6 File Offset: 0x000573B6
		public int Index { get; set; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x000591BF File Offset: 0x000573BF
		// (set) Token: 0x06001E5D RID: 7773 RVA: 0x000591C7 File Offset: 0x000573C7
		public bool Last { get; set; }

		// Token: 0x06001E5E RID: 7774 RVA: 0x000591D0 File Offset: 0x000573D0
		public char DoubleJunction(ITextColumn col, ITextRow row)
		{
			return DoubleBorderTextRow.DoubleJunctionChars[row.First ? 0 : (row.Last ? 2 : 1), col.First ? 0 : (col.Last ? 2 : 1)];
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x0005920C File Offset: 0x0005740C
		public void Render(StringBuilder output, IReadOnlyList<ITextColumn> columns)
		{
			foreach (ITextColumn textColumn in columns)
			{
				textColumn.Render(output, this, 0);
			}
			output.AppendLine();
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0005925C File Offset: 0x0005745C
		public char SingleJunction(ITextColumn col, ITextRow row)
		{
			return DoubleBorderTextRow.SingleJunctionChars[row.First ? 0 : (row.Last ? 2 : 1), col.First ? 0 : (col.Last ? 2 : 1)];
		}

		// Token: 0x04000E95 RID: 3733
		public const char DashChar = '═';

		// Token: 0x04000E96 RID: 3734
		private static readonly char[,] DoubleJunctionChars = new char[,]
		{
			{ '╔', '╦', '╗' },
			{ '╠', '╬', '╣' },
			{ '╚', '╩', '╝' }
		};

		// Token: 0x04000E97 RID: 3735
		private static readonly char[,] SingleJunctionChars = new char[,]
		{
			{ '╒', '╤', '╕' },
			{ '╞', '╪', '╡' },
			{ '╘', '╧', '╛' }
		};
	}
}
