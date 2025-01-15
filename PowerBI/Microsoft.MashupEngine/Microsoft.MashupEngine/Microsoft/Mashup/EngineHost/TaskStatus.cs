using System;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001981 RID: 6529
	internal enum TaskStatus
	{
		// Token: 0x04005635 RID: 22069
		Created,
		// Token: 0x04005636 RID: 22070
		WaitingForActivation,
		// Token: 0x04005637 RID: 22071
		Running = 3,
		// Token: 0x04005638 RID: 22072
		RanToCompletion = 5,
		// Token: 0x04005639 RID: 22073
		Faulted = 7
	}
}
