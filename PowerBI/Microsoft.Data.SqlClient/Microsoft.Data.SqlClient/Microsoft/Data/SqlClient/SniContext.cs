using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000A6 RID: 166
	internal enum SniContext
	{
		// Token: 0x0400050C RID: 1292
		Undefined,
		// Token: 0x0400050D RID: 1293
		Snix_Connect,
		// Token: 0x0400050E RID: 1294
		Snix_PreLoginBeforeSuccessfulWrite,
		// Token: 0x0400050F RID: 1295
		Snix_PreLogin,
		// Token: 0x04000510 RID: 1296
		Snix_LoginSspi,
		// Token: 0x04000511 RID: 1297
		Snix_ProcessSspi,
		// Token: 0x04000512 RID: 1298
		Snix_Login,
		// Token: 0x04000513 RID: 1299
		Snix_EnableMars,
		// Token: 0x04000514 RID: 1300
		Snix_AutoEnlist,
		// Token: 0x04000515 RID: 1301
		Snix_GetMarsSession,
		// Token: 0x04000516 RID: 1302
		Snix_Execute,
		// Token: 0x04000517 RID: 1303
		Snix_Read,
		// Token: 0x04000518 RID: 1304
		Snix_Close,
		// Token: 0x04000519 RID: 1305
		Snix_SendRows
	}
}
