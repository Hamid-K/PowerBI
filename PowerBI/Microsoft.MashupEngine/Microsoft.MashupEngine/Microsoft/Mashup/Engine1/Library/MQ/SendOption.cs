using System;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000959 RID: 2393
	[Flags]
	public enum SendOption
	{
		// Token: 0x04002454 RID: 9300
		None = 0,
		// Token: 0x04002455 RID: 9301
		NewMessageId = 64,
		// Token: 0x04002456 RID: 9302
		NewCorrelationId = 128,
		// Token: 0x04002457 RID: 9303
		FailOnQuiesce = 8192,
		// Token: 0x04002458 RID: 9304
		LogicalOrder = 32768
	}
}
