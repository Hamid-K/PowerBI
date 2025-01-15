using System;

namespace Microsoft.BIServer.HostingEnvironment.HostingInfo
{
	// Token: 0x02000044 RID: 68
	public enum DatabaseValidationStatus
	{
		// Token: 0x040000F0 RID: 240
		Pending = 1,
		// Token: 0x040000F1 RID: 241
		Valid,
		// Token: 0x040000F2 RID: 242
		PbiToRsAttach,
		// Token: 0x040000F3 RID: 243
		DowngradeDetected,
		// Token: 0x040000F4 RID: 244
		UpgradeRequired,
		// Token: 0x040000F5 RID: 245
		DatabaseUnavailable,
		// Token: 0x040000F6 RID: 246
		EncryptedContentNotAccessible
	}
}
