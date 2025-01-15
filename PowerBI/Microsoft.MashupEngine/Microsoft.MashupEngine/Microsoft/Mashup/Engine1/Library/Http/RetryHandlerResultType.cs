using System;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A97 RID: 2711
	public enum RetryHandlerResultType
	{
		// Token: 0x04002843 RID: 10307
		FailWithOriginalException,
		// Token: 0x04002844 RID: 10308
		FailWithCustomException,
		// Token: 0x04002845 RID: 10309
		RetryAfterDefaultDelay,
		// Token: 0x04002846 RID: 10310
		RetryAfterCustomDelay
	}
}
