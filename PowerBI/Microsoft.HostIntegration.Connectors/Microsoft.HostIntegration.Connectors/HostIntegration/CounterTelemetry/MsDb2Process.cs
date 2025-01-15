using System;

namespace Microsoft.HostIntegration.CounterTelemetry
{
	// Token: 0x020005F6 RID: 1526
	public enum MsDb2Process
	{
		// Token: 0x04001D3C RID: 7484
		Executions,
		// Token: 0x04001D3D RID: 7485
		Receive,
		// Token: 0x04001D3E RID: 7486
		ReceiveSelectCommand,
		// Token: 0x04001D3F RID: 7487
		ReceiveSelectCommandWithUpdate,
		// Token: 0x04001D40 RID: 7488
		ReceiveSelectCommandWithDelete,
		// Token: 0x04001D41 RID: 7489
		ReceiveStoredProcedure,
		// Token: 0x04001D42 RID: 7490
		Send,
		// Token: 0x04001D43 RID: 7491
		SendDynamic,
		// Token: 0x04001D44 RID: 7492
		SendOrdered,
		// Token: 0x04001D45 RID: 7493
		SendUpdategramInsert,
		// Token: 0x04001D46 RID: 7494
		SendUpdategramUpdate,
		// Token: 0x04001D47 RID: 7495
		SendUpdategramDelete,
		// Token: 0x04001D48 RID: 7496
		SendCustomInsert,
		// Token: 0x04001D49 RID: 7497
		SendCustomUpdate,
		// Token: 0x04001D4A RID: 7498
		SendCustomDelete,
		// Token: 0x04001D4B RID: 7499
		SendSelect,
		// Token: 0x04001D4C RID: 7500
		SendStoredProcedure
	}
}
