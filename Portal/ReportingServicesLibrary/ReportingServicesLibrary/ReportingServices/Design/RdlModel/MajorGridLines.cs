using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003B7 RID: 951
	public class MajorGridLines
	{
		// Token: 0x06001EC9 RID: 7881 RVA: 0x000025F4 File Offset: 0x000007F4
		public MajorGridLines()
		{
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x0007DAD5 File Offset: 0x0007BCD5
		public MajorGridLines(bool show, Style style)
		{
			this.ShowGridLines = show;
			this.Style = style;
		}

		// Token: 0x04000D4D RID: 3405
		[DefaultValue(false)]
		public bool ShowGridLines;

		// Token: 0x04000D4E RID: 3406
		public Style Style;
	}
}
