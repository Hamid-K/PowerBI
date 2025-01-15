using System;

namespace Microsoft.HostIntegration.CounterTelemetry
{
	// Token: 0x020005F7 RID: 1527
	public enum InformixProcess
	{
		// Token: 0x04001D4E RID: 7502
		Executions,
		// Token: 0x04001D4F RID: 7503
		Receive,
		// Token: 0x04001D50 RID: 7504
		ReceiveSelectCommand,
		// Token: 0x04001D51 RID: 7505
		ReceiveSelectCommandWithUpdate,
		// Token: 0x04001D52 RID: 7506
		ReceiveSelectCommandWithDelete,
		// Token: 0x04001D53 RID: 7507
		ReceiveStoredProcedure,
		// Token: 0x04001D54 RID: 7508
		Send,
		// Token: 0x04001D55 RID: 7509
		SendDynamic,
		// Token: 0x04001D56 RID: 7510
		SendOrdered,
		// Token: 0x04001D57 RID: 7511
		SendUpdategramInsert,
		// Token: 0x04001D58 RID: 7512
		SendUpdategramUpdate,
		// Token: 0x04001D59 RID: 7513
		SendUpdategramDelete,
		// Token: 0x04001D5A RID: 7514
		SendCustomInsert,
		// Token: 0x04001D5B RID: 7515
		SendCustomUpdate,
		// Token: 0x04001D5C RID: 7516
		SendCustomDelete,
		// Token: 0x04001D5D RID: 7517
		SendSelect,
		// Token: 0x04001D5E RID: 7518
		SendStoredProcedure
	}
}
