using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x0200053B RID: 1339
	public class EllipsisTextRow : ITextRow
	{
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x00058F21 File Offset: 0x00057121
		// (set) Token: 0x06001E36 RID: 7734 RVA: 0x00058F29 File Offset: 0x00057129
		public int ColumnIndex { get; set; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x00058F32 File Offset: 0x00057132
		// (set) Token: 0x06001E38 RID: 7736 RVA: 0x00058F3A File Offset: 0x0005713A
		public bool First { get; set; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x00058F44 File Offset: 0x00057144
		// (set) Token: 0x06001E3A RID: 7738 RVA: 0x0000CC37 File Offset: 0x0000AE37
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

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001E3B RID: 7739 RVA: 0x00058F5A File Offset: 0x0005715A
		// (set) Token: 0x06001E3C RID: 7740 RVA: 0x00058F62 File Offset: 0x00057162
		public int Index { get; set; }

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001E3D RID: 7741 RVA: 0x00058F6B File Offset: 0x0005716B
		// (set) Token: 0x06001E3E RID: 7742 RVA: 0x00058F73 File Offset: 0x00057173
		public string Indicator { get; set; } = "...";

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001E3F RID: 7743 RVA: 0x00058F7C File Offset: 0x0005717C
		// (set) Token: 0x06001E40 RID: 7744 RVA: 0x00058F84 File Offset: 0x00057184
		public bool Last { get; set; }

		// Token: 0x06001E41 RID: 7745 RVA: 0x00058F90 File Offset: 0x00057190
		public void Render(StringBuilder output, IReadOnlyList<ITextColumn> columns)
		{
			int num = 0;
			foreach (ITextColumn textColumn in columns)
			{
				output.Append((num == this.ColumnIndex) ? this.Indicator : new string(' ', textColumn.Width));
				num++;
			}
			output.AppendLine();
		}
	}
}
