using System;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x02000306 RID: 774
	public enum CompressionKind : byte
	{
		// Token: 0x040009D6 RID: 2518
		None,
		// Token: 0x040009D7 RID: 2519
		Deflate,
		// Token: 0x040009D8 RID: 2520
		Zlib,
		// Token: 0x040009D9 RID: 2521
		Default = 1
	}
}
