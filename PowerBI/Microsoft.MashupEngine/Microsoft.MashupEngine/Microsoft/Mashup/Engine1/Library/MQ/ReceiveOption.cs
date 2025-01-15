using System;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000958 RID: 2392
	[Flags]
	public enum ReceiveOption
	{
		// Token: 0x04002449 RID: 9289
		None = 0,
		// Token: 0x0400244A RID: 9290
		BrowseFirst = 16,
		// Token: 0x0400244B RID: 9291
		BrowseNext = 32,
		// Token: 0x0400244C RID: 9292
		MessageUnderCursor = 256,
		// Token: 0x0400244D RID: 9293
		Lock = 512,
		// Token: 0x0400244E RID: 9294
		Unlock = 1024,
		// Token: 0x0400244F RID: 9295
		BrowseMessageUnderCursor = 2048,
		// Token: 0x04002450 RID: 9296
		FailOnQuiesce = 8192,
		// Token: 0x04002451 RID: 9297
		LogicalOrder = 32768,
		// Token: 0x04002452 RID: 9298
		CompleteMessage = 65536
	}
}
