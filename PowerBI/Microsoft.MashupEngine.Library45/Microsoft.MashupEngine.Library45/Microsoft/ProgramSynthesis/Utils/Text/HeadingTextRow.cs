using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x02000539 RID: 1337
	public class HeadingTextRow : ITextRow
	{
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001E28 RID: 7720 RVA: 0x00058E42 File Offset: 0x00057042
		// (set) Token: 0x06001E29 RID: 7721 RVA: 0x00058E4A File Offset: 0x0005704A
		public bool First { get; set; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x00058E54 File Offset: 0x00057054
		// (set) Token: 0x06001E2B RID: 7723 RVA: 0x0000CC37 File Offset: 0x0000AE37
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

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x00058E6A File Offset: 0x0005706A
		// (set) Token: 0x06001E2D RID: 7725 RVA: 0x00058E72 File Offset: 0x00057072
		public int Index { get; set; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x00058E7B File Offset: 0x0005707B
		// (set) Token: 0x06001E2F RID: 7727 RVA: 0x00058E83 File Offset: 0x00057083
		public bool Last { get; set; }

		// Token: 0x06001E30 RID: 7728 RVA: 0x00058E8C File Offset: 0x0005708C
		public void Render(StringBuilder output, IReadOnlyList<ITextColumn> columns)
		{
			if (columns.OfType<TextColumnBase>().All((TextColumnBase c) => string.IsNullOrEmpty(c.Heading)))
			{
				return;
			}
			foreach (ITextColumn textColumn in columns)
			{
				textColumn.Render(output, this, 0);
			}
			output.AppendLine();
		}
	}
}
