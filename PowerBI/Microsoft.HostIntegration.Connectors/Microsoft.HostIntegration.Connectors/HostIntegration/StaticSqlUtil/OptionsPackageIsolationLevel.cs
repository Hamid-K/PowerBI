using System;

namespace Microsoft.HostIntegration.StaticSqlUtil
{
	// Token: 0x02000A67 RID: 2663
	public enum OptionsPackageIsolationLevel
	{
		// Token: 0x04004174 RID: 16756
		RepeatableRead = 9283,
		// Token: 0x04004175 RID: 16757
		ReadUncommitted = 9281,
		// Token: 0x04004176 RID: 16758
		ReadCommitted,
		// Token: 0x04004177 RID: 16759
		NoCommit = 9285,
		// Token: 0x04004178 RID: 16760
		Serializable = 9284
	}
}
