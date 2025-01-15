using System;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200072D RID: 1837
	[Flags]
	internal enum RemoteEnvironmentSecurity
	{
		// Token: 0x040022D4 RID: 8916
		Undefined = 0,
		// Token: 0x040022D5 RID: 8917
		Off = 65535,
		// Token: 0x040022D6 RID: 8918
		HIS50NotSet = -1,
		// Token: 0x040022D7 RID: 8919
		Client = 1,
		// Token: 0x040022D8 RID: 8920
		Package = 2,
		// Token: 0x040022D9 RID: 8921
		User = 4,
		// Token: 0x040022DA RID: 8922
		Strong = 8,
		// Token: 0x040022DB RID: 8923
		Verified = 16,
		// Token: 0x040022DC RID: 8924
		IbmCicsSecurityExit = 131072,
		// Token: 0x040022DD RID: 8925
		ImsHWS01SecurityExit = 262144,
		// Token: 0x040022DE RID: 8926
		OverrideSourceTp = 524288,
		// Token: 0x040022DF RID: 8927
		Reserved = 65408
	}
}
