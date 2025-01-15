using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200026F RID: 623
	[Flags]
	public enum ExtendedProcessOptions
	{
		// Token: 0x04000622 RID: 1570
		None = 0,
		// Token: 0x04000623 RID: 1571
		KillProcessOnTimeout = 1,
		// Token: 0x04000624 RID: 1572
		DumpOnProcessTimeout = 2
	}
}
