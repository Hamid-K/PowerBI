using System;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x0200095A RID: 2394
	[Flags]
	public enum MatchOption
	{
		// Token: 0x0400245A RID: 9306
		None = 0,
		// Token: 0x0400245B RID: 9307
		MessageId = 1,
		// Token: 0x0400245C RID: 9308
		Correlator = 2,
		// Token: 0x0400245D RID: 9309
		GroupId = 4,
		// Token: 0x0400245E RID: 9310
		SequenceNumber = 8,
		// Token: 0x0400245F RID: 9311
		Offset = 16,
		// Token: 0x04002460 RID: 9312
		Token = 32
	}
}
