using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003B6 RID: 950
	public class Legend
	{
		// Token: 0x06001EC7 RID: 7879 RVA: 0x0007DA78 File Offset: 0x0007BC78
		public Legend()
		{
			this.Style = new Style();
			this.Layout = Legend.Layouts.Column;
			this.Position = Legend.Positions.RightTop;
			this.InsidePlotArea = false;
			this.Visible = false;
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x0007DAA8 File Offset: 0x0007BCA8
		public Legend(bool visible, Style style, Legend.Positions position, Legend.Layouts layout, bool insidePlotArea)
		{
			this.Visible = visible;
			this.Style = style;
			this.Position = position;
			this.Layout = layout;
			this.InsidePlotArea = insidePlotArea;
		}

		// Token: 0x04000D48 RID: 3400
		[DefaultValue(false)]
		public bool Visible;

		// Token: 0x04000D49 RID: 3401
		public Style Style;

		// Token: 0x04000D4A RID: 3402
		[DefaultValue(Legend.Positions.RightTop)]
		public Legend.Positions Position;

		// Token: 0x04000D4B RID: 3403
		[DefaultValue(Legend.Layouts.Column)]
		public Legend.Layouts Layout;

		// Token: 0x04000D4C RID: 3404
		[DefaultValue(false)]
		public bool InsidePlotArea;

		// Token: 0x02000513 RID: 1299
		public enum Positions
		{
			// Token: 0x0400125C RID: 4700
			TopLeft,
			// Token: 0x0400125D RID: 4701
			TopCenter,
			// Token: 0x0400125E RID: 4702
			TopRight,
			// Token: 0x0400125F RID: 4703
			LeftTop,
			// Token: 0x04001260 RID: 4704
			LeftBottom,
			// Token: 0x04001261 RID: 4705
			LeftCenter,
			// Token: 0x04001262 RID: 4706
			BottomRight,
			// Token: 0x04001263 RID: 4707
			BottomCenter,
			// Token: 0x04001264 RID: 4708
			BottomLeft,
			// Token: 0x04001265 RID: 4709
			RightTop,
			// Token: 0x04001266 RID: 4710
			RightCenter,
			// Token: 0x04001267 RID: 4711
			RightBottom
		}

		// Token: 0x02000514 RID: 1300
		public enum Layouts
		{
			// Token: 0x04001269 RID: 4713
			Column,
			// Token: 0x0400126A RID: 4714
			Row,
			// Token: 0x0400126B RID: 4715
			Table
		}
	}
}
