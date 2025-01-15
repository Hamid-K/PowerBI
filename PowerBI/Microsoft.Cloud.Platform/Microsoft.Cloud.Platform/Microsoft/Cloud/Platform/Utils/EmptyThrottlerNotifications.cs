using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002B5 RID: 693
	public class EmptyThrottlerNotifications : IThrottlerNotifications
	{
		// Token: 0x060012A6 RID: 4774 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void OnStateChanged(int pendingCount, int runningCount)
		{
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void OnOverflow()
		{
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void OnPotentialDeadlockDetected(string operationName, string actionDetails)
		{
		}
	}
}
