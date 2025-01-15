using System;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000415 RID: 1045
	internal sealed class ColumnSapHanaDimensionAttribute : SapHanaDimensionAttribute
	{
		// Token: 0x060023B9 RID: 9145 RVA: 0x00064E6C File Offset: 0x0006306C
		public ColumnSapHanaDimensionAttribute(SapHanaDimension dimension, string name, string caption, SapHanaColumn column, SapHanaColumn captionColumn)
			: base(dimension, name, caption)
		{
			this.column = column;
			this.captionColumn = captionColumn;
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x00064E87 File Offset: 0x00063087
		public SapHanaColumn Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x00064E8F File Offset: 0x0006308F
		public SapHanaColumn CaptionColumn
		{
			get
			{
				return this.captionColumn;
			}
		}

		// Token: 0x04000E58 RID: 3672
		private readonly SapHanaColumn column;

		// Token: 0x04000E59 RID: 3673
		private readonly SapHanaColumn captionColumn;
	}
}
