using System;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x02000019 RID: 25
	public interface IAnalyticsFeatureSwitchProvider
	{
		// Token: 0x0600005A RID: 90
		bool IsEnabled(AnalyticsFeatureSwitchKind kind);
	}
}
