using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000145 RID: 325
	public class SchedulerActivityType : SingletonActivityType<SchedulerActivityType>
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x0001D80F File Offset: 0x0001BA0F
		public SchedulerActivityType()
			: base("SCHD")
		{
		}

		// Token: 0x04000331 RID: 817
		private const string c_activityType = "SCHD";
	}
}
