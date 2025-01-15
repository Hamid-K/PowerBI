using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007AB RID: 1963
	public enum TYPSQLDA : byte
	{
		// Token: 0x0400297C RID: 10620
		None = 10,
		// Token: 0x0400297D RID: 10621
		StandardOutput = 0,
		// Token: 0x0400297E RID: 10622
		StandardInput,
		// Token: 0x0400297F RID: 10623
		LightOutput,
		// Token: 0x04002980 RID: 10624
		LightInput,
		// Token: 0x04002981 RID: 10625
		ExtendedOutput,
		// Token: 0x04002982 RID: 10626
		ExtendedInput
	}
}
