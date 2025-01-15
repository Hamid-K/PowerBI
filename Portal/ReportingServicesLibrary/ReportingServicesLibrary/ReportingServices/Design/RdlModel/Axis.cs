using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003B9 RID: 953
	public class Axis
	{
		// Token: 0x06001ECD RID: 7885 RVA: 0x0007DB00 File Offset: 0x0007BD00
		public Axis()
		{
			this.Visible = false;
			this.MajorTickMarks = Axis.TickMarks.None;
			this.MinorTickMarks = Axis.TickMarks.None;
			this.Min = null;
			this.Max = null;
			this.MajorInterval = null;
			this.MinorInterval = null;
			this.CrossAt = null;
			this.Reverse = false;
			this.Interlaced = false;
			this.Margin = false;
			this.Scalar = false;
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x0007DB68 File Offset: 0x0007BD68
		public Axis(Title title, Style style, MajorGridLines majorGridLines, MinorGridLines minorGridLines, Axis.TickMarks majorTickMarks, Axis.TickMarks minorTickMarks, string min, string max, string majorInterval, string minorInterval, string crossAt, bool logScale, bool reverse, bool interlaced, bool margin, bool visible, bool scalar)
		{
			this.Title = title;
			this.Style = style;
			this.MajorGridLines = majorGridLines;
			this.MinorGridLines = minorGridLines;
			this.MajorTickMarks = majorTickMarks;
			this.MinorTickMarks = minorTickMarks;
			this.Min = min;
			this.Max = max;
			this.MajorInterval = majorInterval;
			this.MinorInterval = minorInterval;
			this.CrossAt = crossAt;
			this.LogScale = logScale;
			this.Reverse = reverse;
			this.Interlaced = interlaced;
			this.Margin = margin;
			this.Visible = visible;
			this.Scalar = scalar;
		}

		// Token: 0x04000D4F RID: 3407
		public Title Title;

		// Token: 0x04000D50 RID: 3408
		public Style Style;

		// Token: 0x04000D51 RID: 3409
		public MajorGridLines MajorGridLines;

		// Token: 0x04000D52 RID: 3410
		public MinorGridLines MinorGridLines;

		// Token: 0x04000D53 RID: 3411
		[DefaultValue(Axis.TickMarks.None)]
		public Axis.TickMarks MajorTickMarks;

		// Token: 0x04000D54 RID: 3412
		[DefaultValue(Axis.TickMarks.None)]
		public Axis.TickMarks MinorTickMarks;

		// Token: 0x04000D55 RID: 3413
		[DefaultValue("")]
		public string Min;

		// Token: 0x04000D56 RID: 3414
		[DefaultValue("")]
		public string Max;

		// Token: 0x04000D57 RID: 3415
		[DefaultValue("")]
		public string MajorInterval;

		// Token: 0x04000D58 RID: 3416
		[DefaultValue("")]
		public string MinorInterval;

		// Token: 0x04000D59 RID: 3417
		[DefaultValue("")]
		public string CrossAt;

		// Token: 0x04000D5A RID: 3418
		[DefaultValue(false)]
		public bool LogScale;

		// Token: 0x04000D5B RID: 3419
		[DefaultValue(false)]
		public bool Reverse;

		// Token: 0x04000D5C RID: 3420
		[DefaultValue(false)]
		public bool Interlaced;

		// Token: 0x04000D5D RID: 3421
		[DefaultValue(false)]
		public bool Margin;

		// Token: 0x04000D5E RID: 3422
		[DefaultValue(false)]
		public bool Visible;

		// Token: 0x04000D5F RID: 3423
		[DefaultValue(false)]
		public bool Scalar;

		// Token: 0x02000515 RID: 1301
		public enum TickMarks
		{
			// Token: 0x0400126D RID: 4717
			None,
			// Token: 0x0400126E RID: 4718
			Inside,
			// Token: 0x0400126F RID: 4719
			Outside,
			// Token: 0x04001270 RID: 4720
			Cross
		}
	}
}
