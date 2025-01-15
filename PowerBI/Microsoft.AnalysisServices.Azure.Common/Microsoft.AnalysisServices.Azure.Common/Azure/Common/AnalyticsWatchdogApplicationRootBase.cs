using System;
using Microsoft.Cloud.Platform.Watchdog;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000038 RID: 56
	public abstract class AnalyticsWatchdogApplicationRootBase : AnalyticsApplicationRootBase
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		protected AnalyticsWatchdogApplicationRootBase(string name)
			: base(name)
		{
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000E9C9 File Offset: 0x0000CBC9
		protected override void OnInitialize()
		{
			base.OnInitialize();
			base.AddBlock(new WatchdogTaskExecutorService());
		}
	}
}
