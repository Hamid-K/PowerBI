using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B1D RID: 2845
	public enum CicsBridgeReturnCode
	{
		// Token: 0x040046E1 RID: 18145
		Ok,
		// Token: 0x040046E2 RID: 18146
		CicsExecError,
		// Token: 0x040046E3 RID: 18147
		ApiError,
		// Token: 0x040046E4 RID: 18148
		BridgeError,
		// Token: 0x040046E5 RID: 18149
		BridgeAbend,
		// Token: 0x040046E6 RID: 18150
		ApplicationAbend,
		// Token: 0x040046E7 RID: 18151
		SecurityError,
		// Token: 0x040046E8 RID: 18152
		ProgramNotAvailable,
		// Token: 0x040046E9 RID: 18153
		BridgeTimeout,
		// Token: 0x040046EA RID: 18154
		TransactionNotAvailable
	}
}
