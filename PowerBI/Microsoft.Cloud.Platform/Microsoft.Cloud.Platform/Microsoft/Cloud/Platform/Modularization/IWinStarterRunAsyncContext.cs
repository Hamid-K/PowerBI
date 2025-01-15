using System;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000DB RID: 219
	public interface IWinStarterRunAsyncContext
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600062D RID: 1581
		ApplicationRoot ApplicationRoot { get; }

		// Token: 0x0600062E RID: 1582
		void StopAndWaitForShutdownToComplete();

		// Token: 0x0600062F RID: 1583
		void WaitForStartToComplete();
	}
}
