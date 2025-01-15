using System;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200004C RID: 76
	public enum MediaTypeFormatterMatchRanking
	{
		// Token: 0x040000DE RID: 222
		None,
		// Token: 0x040000DF RID: 223
		MatchOnCanWriteType,
		// Token: 0x040000E0 RID: 224
		MatchOnRequestAcceptHeaderLiteral,
		// Token: 0x040000E1 RID: 225
		MatchOnRequestAcceptHeaderSubtypeMediaRange,
		// Token: 0x040000E2 RID: 226
		MatchOnRequestAcceptHeaderAllMediaRange,
		// Token: 0x040000E3 RID: 227
		MatchOnRequestWithMediaTypeMapping,
		// Token: 0x040000E4 RID: 228
		MatchOnRequestMediaType
	}
}
