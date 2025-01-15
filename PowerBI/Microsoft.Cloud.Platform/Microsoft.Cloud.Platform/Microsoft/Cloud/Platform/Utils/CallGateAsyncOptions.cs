using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001B8 RID: 440
	[Flags]
	public enum CallGateAsyncOptions
	{
		// Token: 0x04000475 RID: 1141
		None = 0,
		// Token: 0x04000476 RID: 1142
		AllowSyncCall = 1,
		// Token: 0x04000477 RID: 1143
		PropagateCallContext = 2
	}
}
