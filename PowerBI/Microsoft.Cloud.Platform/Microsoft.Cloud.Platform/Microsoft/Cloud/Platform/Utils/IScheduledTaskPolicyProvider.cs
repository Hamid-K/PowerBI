using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000154 RID: 340
	public interface IScheduledTaskPolicyProvider
	{
		// Token: 0x060008DF RID: 2271
		IEnumerable<ExclusionGroupIdentifier> GetExclusionGroups();

		// Token: 0x060008E0 RID: 2272
		TimeSpan GetTimeout();

		// Token: 0x060008E1 RID: 2273
		bool GetIsCrashAllowedOnTimeout();

		// Token: 0x060008E2 RID: 2274
		DateTime GetNextRunTime(DateTime now);
	}
}
