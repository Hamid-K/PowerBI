using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200001D RID: 29
	internal enum ActiveDirectoryAuthenticationTimeoutRetryState
	{
		// Token: 0x04000055 RID: 85
		NotStarted,
		// Token: 0x04000056 RID: 86
		Retrying,
		// Token: 0x04000057 RID: 87
		HasLoggedIn
	}
}
