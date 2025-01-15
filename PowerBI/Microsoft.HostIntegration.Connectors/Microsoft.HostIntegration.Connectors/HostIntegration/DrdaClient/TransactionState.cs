using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009D5 RID: 2517
	internal enum TransactionState
	{
		// Token: 0x04003DB0 RID: 15792
		AutoCommit,
		// Token: 0x04003DB1 RID: 15793
		LocalStarted,
		// Token: 0x04003DB2 RID: 15794
		GlobalStarted,
		// Token: 0x04003DB3 RID: 15795
		Commit,
		// Token: 0x04003DB4 RID: 15796
		RollBack
	}
}
