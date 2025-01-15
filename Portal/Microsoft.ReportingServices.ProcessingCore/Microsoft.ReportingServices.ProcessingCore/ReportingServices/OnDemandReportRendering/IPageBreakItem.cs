using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002D2 RID: 722
	internal interface IPageBreakItem
	{
		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06001B1B RID: 6939
		[Obsolete("Use PageBreak.BreakLocation instead.")]
		PageBreakLocation PageBreakLocation { get; }

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06001B1C RID: 6940
		PageBreak PageBreak { get; }
	}
}
