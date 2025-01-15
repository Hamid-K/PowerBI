using System;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200034D RID: 845
	[Flags]
	public enum EventsKitFactoryOptions
	{
		// Token: 0x0400089C RID: 2204
		None = 0,
		// Token: 0x0400089D RID: 2205
		EmitPerformanceCounters = 1,
		// Token: 0x0400089E RID: 2206
		EmitWindowsEventLogEvents = 2,
		// Token: 0x0400089F RID: 2207
		EmitEventingServerEvents = 4,
		// Token: 0x040008A0 RID: 2208
		EmitEtwEvents = 8,
		// Token: 0x040008A1 RID: 2209
		All = 15
	}
}
