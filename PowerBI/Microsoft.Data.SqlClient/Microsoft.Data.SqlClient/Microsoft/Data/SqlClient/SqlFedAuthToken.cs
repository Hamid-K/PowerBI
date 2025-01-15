using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000109 RID: 265
	internal sealed class SqlFedAuthToken
	{
		// Token: 0x0400087C RID: 2172
		internal uint dataLen;

		// Token: 0x0400087D RID: 2173
		internal byte[] accessToken;

		// Token: 0x0400087E RID: 2174
		internal long expirationFileTime;
	}
}
