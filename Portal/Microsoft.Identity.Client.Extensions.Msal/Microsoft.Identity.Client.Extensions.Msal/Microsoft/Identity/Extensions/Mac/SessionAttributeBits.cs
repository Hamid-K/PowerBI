using System;

namespace Microsoft.Identity.Extensions.Mac
{
	// Token: 0x0200000A RID: 10
	[Flags]
	internal enum SessionAttributeBits
	{
		// Token: 0x0400002A RID: 42
		SessionIsRoot = 1,
		// Token: 0x0400002B RID: 43
		SessionHasGraphicAccess = 16,
		// Token: 0x0400002C RID: 44
		SessionHasTty = 32,
		// Token: 0x0400002D RID: 45
		SessionIsRemote = 4096
	}
}
