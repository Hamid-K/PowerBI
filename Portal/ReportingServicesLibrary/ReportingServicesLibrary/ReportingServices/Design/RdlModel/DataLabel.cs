using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003B1 RID: 945
	public class DataLabel
	{
		// Token: 0x06001EBD RID: 7869 RVA: 0x0007D91C File Offset: 0x0007BB1C
		public DataLabel()
		{
			this.Position = DataLabel.Positions.Auto;
			this.Rotation = 0;
			this.Visible = false;
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x0007D939 File Offset: 0x0007BB39
		public DataLabel(string value, bool visible, Style style, DataLabel.Positions position, int rotation)
		{
			this.Value = value;
			this.Visible = visible;
			this.Style = style;
			this.Position = position;
			this.Rotation = rotation;
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x0007D966 File Offset: 0x0007BB66
		public DataLabel(string value)
		{
			this.Value = value;
		}

		// Token: 0x04000D31 RID: 3377
		public Style Style;

		// Token: 0x04000D32 RID: 3378
		[DefaultValue("")]
		public string Value;

		// Token: 0x04000D33 RID: 3379
		[DefaultValue(DataLabel.Positions.Auto)]
		public DataLabel.Positions Position;

		// Token: 0x04000D34 RID: 3380
		[DefaultValue(0)]
		public int Rotation;

		// Token: 0x04000D35 RID: 3381
		[DefaultValue(false)]
		public bool Visible;

		// Token: 0x0200050C RID: 1292
		public enum Positions
		{
			// Token: 0x04001238 RID: 4664
			Auto,
			// Token: 0x04001239 RID: 4665
			Top,
			// Token: 0x0400123A RID: 4666
			TopLeft,
			// Token: 0x0400123B RID: 4667
			TopRight,
			// Token: 0x0400123C RID: 4668
			Left,
			// Token: 0x0400123D RID: 4669
			Center,
			// Token: 0x0400123E RID: 4670
			Right,
			// Token: 0x0400123F RID: 4671
			BottomRight,
			// Token: 0x04001240 RID: 4672
			Bottom,
			// Token: 0x04001241 RID: 4673
			BottomLeft
		}
	}
}
