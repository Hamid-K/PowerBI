using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002BA RID: 698
	public interface IThrottlingPolicy
	{
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060012CD RID: 4813
		int CurrentlyRunningOperations { get; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060012CE RID: 4814
		int MaxConcurrentOperations { get; }

		// Token: 0x060012CF RID: 4815
		TimeToRun GetTimeToRun(DateTime now);

		// Token: 0x060012D0 RID: 4816
		void OnOperationStarted(DateTime now);

		// Token: 0x060012D1 RID: 4817
		void OnOperationCompleted(DateTime now);
	}
}
