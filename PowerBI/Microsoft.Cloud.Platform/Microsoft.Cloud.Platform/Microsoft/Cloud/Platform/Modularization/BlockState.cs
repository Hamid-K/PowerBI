using System;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000C0 RID: 192
	public enum BlockState
	{
		// Token: 0x040001E9 RID: 489
		Uninitialized,
		// Token: 0x040001EA RID: 490
		Initializing,
		// Token: 0x040001EB RID: 491
		Initialized,
		// Token: 0x040001EC RID: 492
		Starting,
		// Token: 0x040001ED RID: 493
		Started,
		// Token: 0x040001EE RID: 494
		Stopping,
		// Token: 0x040001EF RID: 495
		WaitingForStopToComplete,
		// Token: 0x040001F0 RID: 496
		Stopped,
		// Token: 0x040001F1 RID: 497
		ShuttingDown
	}
}
