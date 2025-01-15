using System;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004B8 RID: 1208
	public enum AffiliateApplicationType
	{
		// Token: 0x04001858 RID: 6232
		None,
		// Token: 0x04001859 RID: 6233
		Individual,
		// Token: 0x0400185A RID: 6234
		Group,
		// Token: 0x0400185B RID: 6235
		ConfigStore = 4,
		// Token: 0x0400185C RID: 6236
		HostGroup = 8,
		// Token: 0x0400185D RID: 6237
		PasswordSyncAdapter = 16,
		// Token: 0x0400185E RID: 6238
		PasswordSyncGroupAdapter = 32,
		// Token: 0x0400185F RID: 6239
		All = 268435455
	}
}
