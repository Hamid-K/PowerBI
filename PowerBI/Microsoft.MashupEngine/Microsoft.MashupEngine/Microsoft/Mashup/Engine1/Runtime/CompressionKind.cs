using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012A3 RID: 4771
	public enum CompressionKind
	{
		// Token: 0x04004504 RID: 17668
		None = -1,
		// Token: 0x04004505 RID: 17669
		GZip,
		// Token: 0x04004506 RID: 17670
		Deflate,
		// Token: 0x04004507 RID: 17671
		Snappy,
		// Token: 0x04004508 RID: 17672
		Brotli,
		// Token: 0x04004509 RID: 17673
		LZ4,
		// Token: 0x0400450A RID: 17674
		Zstandard
	}
}
