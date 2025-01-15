using System;

namespace Microsoft.Cloud.Platform.RequestProtection
{
	// Token: 0x02000465 RID: 1125
	[Flags]
	public enum RequestProtectionOptions
	{
		// Token: 0x04000C4E RID: 3150
		None = 0,
		// Token: 0x04000C4F RID: 3151
		BypassAuthentication = 1,
		// Token: 0x04000C50 RID: 3152
		BypassAuthorization = 2
	}
}
