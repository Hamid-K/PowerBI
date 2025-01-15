using System;

namespace Microsoft.HostIntegration.DrdaClient.Packages
{
	// Token: 0x02000A41 RID: 2625
	public enum OptionsPackageIsolationLevel
	{
		// Token: 0x04004088 RID: 16520
		RepeatableRead = 9283,
		// Token: 0x04004089 RID: 16521
		ReadUncommitted = 9281,
		// Token: 0x0400408A RID: 16522
		ReadCommitted,
		// Token: 0x0400408B RID: 16523
		NoCommit = 9285,
		// Token: 0x0400408C RID: 16524
		Serializable = 9284
	}
}
