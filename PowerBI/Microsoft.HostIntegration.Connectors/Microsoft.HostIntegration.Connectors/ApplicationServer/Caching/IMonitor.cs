using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002E0 RID: 736
	internal interface IMonitor
	{
		// Token: 0x06001B2A RID: 6954
		void Cancel();

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001B2B RID: 6955
		bool InCriticalMemoryPressureState { get; }
	}
}
