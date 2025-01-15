using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000010 RID: 16
	[Flags]
	public enum EventActivityOptions
	{
		// Token: 0x04000014 RID: 20
		None = 0,
		// Token: 0x04000015 RID: 21
		Disable = 2,
		// Token: 0x04000016 RID: 22
		Recursive = 4,
		// Token: 0x04000017 RID: 23
		Detachable = 8
	}
}
