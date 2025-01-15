using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000101 RID: 257
	internal class FederatedAuthenticationFeatureExtensionData
	{
		// Token: 0x0400084B RID: 2123
		internal TdsEnums.FedAuthLibrary libraryType;

		// Token: 0x0400084C RID: 2124
		internal bool fedAuthRequiredPreLoginResponse;

		// Token: 0x0400084D RID: 2125
		internal SqlAuthenticationMethod authentication;

		// Token: 0x0400084E RID: 2126
		internal byte[] accessToken;
	}
}
