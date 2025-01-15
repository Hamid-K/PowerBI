using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BC8 RID: 3016
	public enum MqCommandType
	{
		// Token: 0x04004F54 RID: 20308
		Unknown,
		// Token: 0x04004F55 RID: 20309
		AsyncStatus,
		// Token: 0x04004F56 RID: 20310
		XaOpen,
		// Token: 0x04004F57 RID: 20311
		XaStart,
		// Token: 0x04004F58 RID: 20312
		XaEnd,
		// Token: 0x04004F59 RID: 20313
		XaPrepare,
		// Token: 0x04004F5A RID: 20314
		XaCommit,
		// Token: 0x04004F5B RID: 20315
		XaRollback,
		// Token: 0x04004F5C RID: 20316
		XaClose,
		// Token: 0x04004F5D RID: 20317
		XaForget,
		// Token: 0x04004F5E RID: 20318
		XaRecover
	}
}
