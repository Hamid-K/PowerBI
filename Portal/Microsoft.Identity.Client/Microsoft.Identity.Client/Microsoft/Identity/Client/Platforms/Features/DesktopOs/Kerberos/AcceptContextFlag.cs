using System;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos
{
	// Token: 0x02000196 RID: 406
	[Flags]
	internal enum AcceptContextFlag
	{
		// Token: 0x0400073E RID: 1854
		Zero = 0,
		// Token: 0x0400073F RID: 1855
		Delegate = 1,
		// Token: 0x04000740 RID: 1856
		MutualAuth = 2,
		// Token: 0x04000741 RID: 1857
		ReplayDetect = 4,
		// Token: 0x04000742 RID: 1858
		SequenceDetect = 8,
		// Token: 0x04000743 RID: 1859
		Confidentiality = 16,
		// Token: 0x04000744 RID: 1860
		UseSessionKey = 32,
		// Token: 0x04000745 RID: 1861
		AllocateMemory = 256,
		// Token: 0x04000746 RID: 1862
		Connection = 2048,
		// Token: 0x04000747 RID: 1863
		AcceptExtendedError = 32768,
		// Token: 0x04000748 RID: 1864
		AcceptStream = 65536,
		// Token: 0x04000749 RID: 1865
		AcceptIntegrity = 131072,
		// Token: 0x0400074A RID: 1866
		AcceptIdentify = 524288,
		// Token: 0x0400074B RID: 1867
		ProxyBindings = 67108864,
		// Token: 0x0400074C RID: 1868
		AllowMissingBindings = 268435456,
		// Token: 0x0400074D RID: 1869
		UnverifiedTargetName = 536870912
	}
}
