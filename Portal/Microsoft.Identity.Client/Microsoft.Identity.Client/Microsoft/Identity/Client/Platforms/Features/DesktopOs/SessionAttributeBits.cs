using System;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs
{
	// Token: 0x0200018A RID: 394
	[Flags]
	internal enum SessionAttributeBits
	{
		// Token: 0x04000716 RID: 1814
		SessionIsRoot = 1,
		// Token: 0x04000717 RID: 1815
		SessionHasGraphicAccess = 16,
		// Token: 0x04000718 RID: 1816
		SessionHasTty = 32,
		// Token: 0x04000719 RID: 1817
		SessionIsRemote = 4096
	}
}
