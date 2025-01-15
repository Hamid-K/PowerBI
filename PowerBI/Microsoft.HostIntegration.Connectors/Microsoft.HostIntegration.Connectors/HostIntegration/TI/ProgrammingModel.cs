using System;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000725 RID: 1829
	public enum ProgrammingModel
	{
		// Token: 0x0400227A RID: 8826
		UserData,
		// Token: 0x0400227B RID: 8827
		DistributedProgramCall = 2097152,
		// Token: 0x0400227C RID: 8828
		ELMUserData = 131072,
		// Token: 0x0400227D RID: 8829
		ELMLink,
		// Token: 0x0400227E RID: 8830
		TRMUserData = 65536,
		// Token: 0x0400227F RID: 8831
		TRMLink,
		// Token: 0x04002280 RID: 8832
		Link = 1,
		// Token: 0x04002281 RID: 8833
		IMSConnect = 64,
		// Token: 0x04002282 RID: 8834
		IMSImplicit = 262144,
		// Token: 0x04002283 RID: 8835
		IMSExplicit = 524288
	}
}
