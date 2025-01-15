using System;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000957 RID: 2391
	[Flags]
	public enum OpenOption
	{
		// Token: 0x0400243B RID: 9275
		None = 0,
		// Token: 0x0400243C RID: 9276
		Shared = 2,
		// Token: 0x0400243D RID: 9277
		Exclusive = 4,
		// Token: 0x0400243E RID: 9278
		Browse = 8,
		// Token: 0x0400243F RID: 9279
		Inquire = 32,
		// Token: 0x04002440 RID: 9280
		SaveContext = 128,
		// Token: 0x04002441 RID: 9281
		PassIdentityContext = 256,
		// Token: 0x04002442 RID: 9282
		PassAllContext = 512,
		// Token: 0x04002443 RID: 9283
		SetIdentityContext = 1024,
		// Token: 0x04002444 RID: 9284
		SetAllContext = 2048,
		// Token: 0x04002445 RID: 9285
		FailOnQuiesce = 8192,
		// Token: 0x04002446 RID: 9286
		NoReadAhead = 524288,
		// Token: 0x04002447 RID: 9287
		ReadAhead = 1048576
	}
}
