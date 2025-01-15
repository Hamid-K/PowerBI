using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001628 RID: 5672
	public class TableKey
	{
		// Token: 0x06008EFC RID: 36604 RVA: 0x001DC7B8 File Offset: 0x001DA9B8
		public TableKey(int[] columns, bool primary)
		{
			this.columns = columns;
			this.primary = primary;
		}

		// Token: 0x1700257E RID: 9598
		// (get) Token: 0x06008EFD RID: 36605 RVA: 0x001DC7CE File Offset: 0x001DA9CE
		public int[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x1700257F RID: 9599
		// (get) Token: 0x06008EFE RID: 36606 RVA: 0x001DC7D6 File Offset: 0x001DA9D6
		public bool Primary
		{
			get
			{
				return this.primary;
			}
		}

		// Token: 0x06008EFF RID: 36607 RVA: 0x001DC7DE File Offset: 0x001DA9DE
		public TableKey SelectColumns(int[] columns)
		{
			return new TableKey(columns, this.Primary);
		}

		// Token: 0x04004D6C RID: 19820
		private readonly int[] columns;

		// Token: 0x04004D6D RID: 19821
		private readonly bool primary;
	}
}
