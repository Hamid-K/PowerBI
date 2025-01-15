using System;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x0200052D RID: 1325
	public interface IDynamicWidthTextColumn : ITextColumn
	{
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001DBE RID: 7614
		string Heading { get; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001DBF RID: 7615
		// (set) Token: 0x06001DC0 RID: 7616
		int? LeftPadding { get; set; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001DC1 RID: 7617
		// (set) Token: 0x06001DC2 RID: 7618
		int? MaximumWidth { get; set; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001DC3 RID: 7619
		int MinimumWidth { get; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001DC4 RID: 7620
		// (set) Token: 0x06001DC5 RID: 7621
		int? RightPadding { get; set; }
	}
}
