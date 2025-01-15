using System;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000025 RID: 37
	[Flags]
	public enum UrlEndpointUsage
	{
		// Token: 0x040000F5 RID: 245
		None = 0,
		// Token: 0x040000F6 RID: 246
		Prod = 1,
		// Token: 0x040000F7 RID: 247
		Dev = 2,
		// Token: 0x040000F8 RID: 248
		Ssrs = 4,
		// Token: 0x040000F9 RID: 249
		Pbirs = 8,
		// Token: 0x040000FA RID: 250
		PortalConfigured = 16,
		// Token: 0x040000FB RID: 251
		RsConfigured = 32,
		// Token: 0x040000FC RID: 252
		InConfigFile = 64,
		// Token: 0x040000FD RID: 253
		RegisterOnUpgrade = 128
	}
}
