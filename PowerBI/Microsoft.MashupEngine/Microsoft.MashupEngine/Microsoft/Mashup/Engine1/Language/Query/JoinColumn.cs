using System;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200187D RID: 6269
	public struct JoinColumn
	{
		// Token: 0x06009F1E RID: 40734 RVA: 0x0020E469 File Offset: 0x0020C669
		public JoinColumn(int leftColumn, int rightColumn)
		{
			this.leftColumn = leftColumn;
			this.rightColumn = rightColumn;
		}

		// Token: 0x17002914 RID: 10516
		// (get) Token: 0x06009F1F RID: 40735 RVA: 0x0020E479 File Offset: 0x0020C679
		public bool Left
		{
			get
			{
				return this.leftColumn != -1;
			}
		}

		// Token: 0x17002915 RID: 10517
		// (get) Token: 0x06009F20 RID: 40736 RVA: 0x0020E487 File Offset: 0x0020C687
		public bool Right
		{
			get
			{
				return this.rightColumn != -1;
			}
		}

		// Token: 0x17002916 RID: 10518
		// (get) Token: 0x06009F21 RID: 40737 RVA: 0x0020E495 File Offset: 0x0020C695
		public int LeftColumn
		{
			get
			{
				return this.leftColumn;
			}
		}

		// Token: 0x17002917 RID: 10519
		// (get) Token: 0x06009F22 RID: 40738 RVA: 0x0020E49D File Offset: 0x0020C69D
		public int RightColumn
		{
			get
			{
				return this.rightColumn;
			}
		}

		// Token: 0x04005383 RID: 21379
		private int leftColumn;

		// Token: 0x04005384 RID: 21380
		private int rightColumn;
	}
}
