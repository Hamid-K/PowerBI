using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200006E RID: 110
	internal enum SqlConnectionTimeoutErrorPhase
	{
		// Token: 0x040001FF RID: 511
		Undefined,
		// Token: 0x04000200 RID: 512
		PreLoginBegin,
		// Token: 0x04000201 RID: 513
		InitializeConnection,
		// Token: 0x04000202 RID: 514
		SendPreLoginHandshake,
		// Token: 0x04000203 RID: 515
		ConsumePreLoginHandshake,
		// Token: 0x04000204 RID: 516
		LoginBegin,
		// Token: 0x04000205 RID: 517
		ProcessConnectionAuth,
		// Token: 0x04000206 RID: 518
		PostLogin,
		// Token: 0x04000207 RID: 519
		Complete,
		// Token: 0x04000208 RID: 520
		Count
	}
}
