using System;

namespace Microsoft.Mashup.Engine1.Library.Uris.Internal
{
	// Token: 0x020002CB RID: 715
	[Flags]
	internal enum UnescapeMode
	{
		// Token: 0x04000946 RID: 2374
		CopyOnly = 0,
		// Token: 0x04000947 RID: 2375
		Escape = 1,
		// Token: 0x04000948 RID: 2376
		Unescape = 2,
		// Token: 0x04000949 RID: 2377
		EscapeUnescape = 3,
		// Token: 0x0400094A RID: 2378
		V1ToStringFlag = 4,
		// Token: 0x0400094B RID: 2379
		UnescapeAll = 8,
		// Token: 0x0400094C RID: 2380
		UnescapeAllOrThrow = 24
	}
}
