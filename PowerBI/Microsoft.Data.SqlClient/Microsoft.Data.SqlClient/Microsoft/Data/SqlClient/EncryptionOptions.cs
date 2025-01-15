using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000FC RID: 252
	internal enum EncryptionOptions
	{
		// Token: 0x0400082C RID: 2092
		OFF,
		// Token: 0x0400082D RID: 2093
		ON,
		// Token: 0x0400082E RID: 2094
		NOT_SUP,
		// Token: 0x0400082F RID: 2095
		REQ,
		// Token: 0x04000830 RID: 2096
		LOGIN,
		// Token: 0x04000831 RID: 2097
		OPTIONS_MASK = 63,
		// Token: 0x04000832 RID: 2098
		CTAIP,
		// Token: 0x04000833 RID: 2099
		CLIENT_CERT = 128
	}
}
