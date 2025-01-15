using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000AD RID: 173
	public enum SqlAuthenticationMethod
	{
		// Token: 0x0400054C RID: 1356
		NotSpecified,
		// Token: 0x0400054D RID: 1357
		SqlPassword,
		// Token: 0x0400054E RID: 1358
		ActiveDirectoryPassword,
		// Token: 0x0400054F RID: 1359
		ActiveDirectoryIntegrated,
		// Token: 0x04000550 RID: 1360
		ActiveDirectoryInteractive,
		// Token: 0x04000551 RID: 1361
		ActiveDirectoryServicePrincipal,
		// Token: 0x04000552 RID: 1362
		ActiveDirectoryDeviceCodeFlow,
		// Token: 0x04000553 RID: 1363
		ActiveDirectoryManagedIdentity,
		// Token: 0x04000554 RID: 1364
		ActiveDirectoryMSI,
		// Token: 0x04000555 RID: 1365
		ActiveDirectoryDefault
	}
}
