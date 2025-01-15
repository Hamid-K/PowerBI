using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000FE RID: 254
	internal enum PreLoginOptions
	{
		// Token: 0x04000838 RID: 2104
		VERSION,
		// Token: 0x04000839 RID: 2105
		ENCRYPT,
		// Token: 0x0400083A RID: 2106
		INSTANCE,
		// Token: 0x0400083B RID: 2107
		THREADID,
		// Token: 0x0400083C RID: 2108
		MARS,
		// Token: 0x0400083D RID: 2109
		TRACEID,
		// Token: 0x0400083E RID: 2110
		FEDAUTHREQUIRED,
		// Token: 0x0400083F RID: 2111
		NUMOPT,
		// Token: 0x04000840 RID: 2112
		LASTOPT = 255
	}
}
