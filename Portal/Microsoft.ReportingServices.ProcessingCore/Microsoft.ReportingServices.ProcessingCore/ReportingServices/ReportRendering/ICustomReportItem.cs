using System;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000062 RID: 98
	public interface ICustomReportItem
	{
		// Token: 0x170004FF RID: 1279
		// (set) Token: 0x060006B3 RID: 1715
		CustomReportItem CustomItem { set; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060006B4 RID: 1716
		ReportItem RenderItem { get; }

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060006B5 RID: 1717
		Action Action { get; }

		// Token: 0x060006B6 RID: 1718
		ChangeType Process();
	}
}
