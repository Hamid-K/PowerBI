using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006E1 RID: 1761
	internal interface IPageBreakItem
	{
		// Token: 0x06005FF9 RID: 24569
		bool HasPageBreaks(bool atStart);

		// Token: 0x06005FFA RID: 24570
		bool IgnorePageBreaks();
	}
}
