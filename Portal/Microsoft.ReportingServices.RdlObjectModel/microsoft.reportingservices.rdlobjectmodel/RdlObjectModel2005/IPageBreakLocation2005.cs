using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000038 RID: 56
	internal interface IPageBreakLocation2005 : IUpgradeable
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001E6 RID: 486
		// (set) Token: 0x060001E7 RID: 487
		bool PageBreakAtStart { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001E8 RID: 488
		// (set) Token: 0x060001E9 RID: 489
		bool PageBreakAtEnd { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001EA RID: 490
		// (set) Token: 0x060001EB RID: 491
		PageBreak PageBreak { get; set; }
	}
}
