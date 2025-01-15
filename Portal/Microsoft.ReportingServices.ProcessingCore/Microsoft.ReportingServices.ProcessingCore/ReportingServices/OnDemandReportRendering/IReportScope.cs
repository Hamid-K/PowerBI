using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002BE RID: 702
	public interface IReportScope
	{
		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06001AB1 RID: 6833
		IReportScopeInstance ReportScopeInstance { get; }

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06001AB2 RID: 6834
		IRIFReportScope RIFReportScope { get; }
	}
}
