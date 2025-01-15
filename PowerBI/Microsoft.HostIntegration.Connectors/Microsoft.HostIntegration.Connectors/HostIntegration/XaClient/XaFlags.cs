using System;

namespace Microsoft.HostIntegration.XaClient
{
	// Token: 0x02000701 RID: 1793
	[Flags]
	public enum XaFlags
	{
		// Token: 0x0400210B RID: 8459
		None = 0,
		// Token: 0x0400210C RID: 8460
		Asynchronous = -2147483648,
		// Token: 0x0400210D RID: 8461
		OnePhaseOptimisation = 1073741824,
		// Token: 0x0400210E RID: 8462
		Fail = 536870912,
		// Token: 0x0400210F RID: 8463
		NoWait = 268435456,
		// Token: 0x04002110 RID: 8464
		Resume = 134217728,
		// Token: 0x04002111 RID: 8465
		Success = 67108864,
		// Token: 0x04002112 RID: 8466
		Suspend = 33554432,
		// Token: 0x04002113 RID: 8467
		StartRecoveryScan = 16777216,
		// Token: 0x04002114 RID: 8468
		EndRecoveryScan = 8388608,
		// Token: 0x04002115 RID: 8469
		Multiple = 4194304,
		// Token: 0x04002116 RID: 8470
		Join = 2097152,
		// Token: 0x04002117 RID: 8471
		Migrate = 1048576
	}
}
