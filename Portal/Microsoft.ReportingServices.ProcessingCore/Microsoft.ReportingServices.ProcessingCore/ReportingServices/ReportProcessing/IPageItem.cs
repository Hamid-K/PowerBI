using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200071B RID: 1819
	internal interface IPageItem
	{
		// Token: 0x170023F6 RID: 9206
		// (get) Token: 0x0600658A RID: 25994
		// (set) Token: 0x0600658B RID: 25995
		int StartPage { get; set; }

		// Token: 0x170023F7 RID: 9207
		// (get) Token: 0x0600658C RID: 25996
		// (set) Token: 0x0600658D RID: 25997
		int EndPage { get; set; }
	}
}
