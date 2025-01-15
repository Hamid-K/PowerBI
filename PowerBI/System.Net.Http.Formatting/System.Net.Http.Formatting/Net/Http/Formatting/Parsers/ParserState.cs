using System;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x02000059 RID: 89
	internal enum ParserState
	{
		// Token: 0x0400012E RID: 302
		NeedMoreData,
		// Token: 0x0400012F RID: 303
		Done,
		// Token: 0x04000130 RID: 304
		Invalid,
		// Token: 0x04000131 RID: 305
		DataTooBig
	}
}
