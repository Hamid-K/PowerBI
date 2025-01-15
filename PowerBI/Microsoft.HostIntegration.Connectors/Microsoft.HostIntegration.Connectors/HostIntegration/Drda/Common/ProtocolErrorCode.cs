using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007A1 RID: 1953
	public enum ProtocolErrorCode
	{
		// Token: 0x04002938 RID: 10552
		ReplyReceivedByServer = 1,
		// Token: 0x04002939 RID: 10553
		MultipleDssWithoutChaining,
		// Token: 0x0400293A RID: 10554
		ObjDssSentNotAllowed,
		// Token: 0x0400293B RID: 10555
		ExcsatWasNotFirstCommand = 6,
		// Token: 0x0400293C RID: 10556
		AccsecSecchkWrongState = 17,
		// Token: 0x0400293D RID: 10557
		RdbnamMismatch
	}
}
