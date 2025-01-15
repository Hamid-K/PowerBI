using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x02000538 RID: 1336
	public class DataTextRow : List<object>, ITextRow
	{
		// Token: 0x06001E1D RID: 7709 RVA: 0x00058D66 File Offset: 0x00056F66
		public DataTextRow(int id, IEnumerable<object> values)
			: base(values)
		{
			this.Id = new int?(id);
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x00058D7B File Offset: 0x00056F7B
		// (set) Token: 0x06001E1F RID: 7711 RVA: 0x00058D83 File Offset: 0x00056F83
		public bool First { get; set; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x00058D8C File Offset: 0x00056F8C
		// (set) Token: 0x06001E21 RID: 7713 RVA: 0x00058D94 File Offset: 0x00056F94
		public int? Id { get; set; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001E22 RID: 7714 RVA: 0x00058D9D File Offset: 0x00056F9D
		// (set) Token: 0x06001E23 RID: 7715 RVA: 0x00058DA5 File Offset: 0x00056FA5
		public int Index { get; set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x00058DAE File Offset: 0x00056FAE
		// (set) Token: 0x06001E25 RID: 7717 RVA: 0x00058DB6 File Offset: 0x00056FB6
		public bool Last { get; set; }

		// Token: 0x06001E26 RID: 7718 RVA: 0x00058DC0 File Offset: 0x00056FC0
		public void Render(StringBuilder output, IReadOnlyList<ITextColumn> columns)
		{
			int num = columns.Select((ITextColumn c) => c.Lines(this).Count).Max();
			for (int i = 0; i < num; i++)
			{
				foreach (ITextColumn textColumn in columns)
				{
					textColumn.Render(output, this, i);
				}
				output.AppendLine();
			}
		}
	}
}
