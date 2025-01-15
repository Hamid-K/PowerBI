using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002B4 RID: 692
	public interface IThrottlerNotifications
	{
		// Token: 0x060012A3 RID: 4771
		void OnStateChanged(int pendingCount, int runningCount);

		// Token: 0x060012A4 RID: 4772
		void OnOverflow();

		// Token: 0x060012A5 RID: 4773
		void OnPotentialDeadlockDetected(string operationName, string actionDetails);
	}
}
