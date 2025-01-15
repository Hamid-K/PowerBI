using System;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos
{
	// Token: 0x0200019A RID: 410
	[Flags]
	internal enum InitContextFlag
	{
		// Token: 0x04000753 RID: 1875
		Zero = 0,
		// Token: 0x04000754 RID: 1876
		Delegate = 1,
		// Token: 0x04000755 RID: 1877
		MutualAuth = 2,
		// Token: 0x04000756 RID: 1878
		ReplayDetect = 4,
		// Token: 0x04000757 RID: 1879
		SequenceDetect = 8,
		// Token: 0x04000758 RID: 1880
		Confidentiality = 16,
		// Token: 0x04000759 RID: 1881
		UseSessionKey = 32,
		// Token: 0x0400075A RID: 1882
		AllocateMemory = 256,
		// Token: 0x0400075B RID: 1883
		Connection = 2048,
		// Token: 0x0400075C RID: 1884
		InitExtendedError = 16384,
		// Token: 0x0400075D RID: 1885
		InitStream = 32768,
		// Token: 0x0400075E RID: 1886
		InitIntegrity = 65536,
		// Token: 0x0400075F RID: 1887
		InitManualCredValidation = 524288,
		// Token: 0x04000760 RID: 1888
		InitUseSuppliedCreds = 128,
		// Token: 0x04000761 RID: 1889
		InitIdentify = 131072,
		// Token: 0x04000762 RID: 1890
		ProxyBindings = 67108864,
		// Token: 0x04000763 RID: 1891
		AllowMissingBindings = 268435456,
		// Token: 0x04000764 RID: 1892
		UnverifiedTargetName = 536870912
	}
}
