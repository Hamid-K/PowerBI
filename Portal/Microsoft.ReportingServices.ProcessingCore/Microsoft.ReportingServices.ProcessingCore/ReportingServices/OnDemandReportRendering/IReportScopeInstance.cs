using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002BF RID: 703
	public interface IReportScopeInstance
	{
		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06001AB3 RID: 6835
		IReportScope ReportScope { get; }

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06001AB4 RID: 6836
		string UniqueName { get; }

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06001AB5 RID: 6837
		// (set) Token: 0x06001AB6 RID: 6838
		bool IsNewContext { get; set; }
	}
}
