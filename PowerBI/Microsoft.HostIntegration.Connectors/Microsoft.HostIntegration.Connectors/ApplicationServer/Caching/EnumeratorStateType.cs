using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000228 RID: 552
	internal enum EnumeratorStateType : byte
	{
		// Token: 0x04000B29 RID: 2857
		Invalid,
		// Token: 0x04000B2A RID: 2858
		BaseEnumeratorState,
		// Token: 0x04000B2B RID: 2859
		FindEnumeratorState,
		// Token: 0x04000B2C RID: 2860
		FixedDepthEnumeratorState,
		// Token: 0x04000B2D RID: 2861
		EnumeratorStateForTagsIntersection,
		// Token: 0x04000B2E RID: 2862
		EnumeratorStateForUnionAll
	}
}
