using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B05 RID: 2821
	[Flags]
	public enum ReceiveOption
	{
		// Token: 0x0400463A RID: 17978
		None = 0,
		// Token: 0x0400463B RID: 17979
		BrowseFirst = 16,
		// Token: 0x0400463C RID: 17980
		BrowseNext = 32,
		// Token: 0x0400463D RID: 17981
		MessageUnderCursor = 256,
		// Token: 0x0400463E RID: 17982
		Lock = 512,
		// Token: 0x0400463F RID: 17983
		Unlock = 1024,
		// Token: 0x04004640 RID: 17984
		BrowseMessageUnderCursor = 2048,
		// Token: 0x04004641 RID: 17985
		FailOnQuiesce = 8192,
		// Token: 0x04004642 RID: 17986
		LogicalOrder = 32768,
		// Token: 0x04004643 RID: 17987
		CompleteMessage = 65536
	}
}
