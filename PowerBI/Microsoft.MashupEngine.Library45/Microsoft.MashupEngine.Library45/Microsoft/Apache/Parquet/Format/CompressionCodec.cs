using System;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002004 RID: 8196
	internal enum CompressionCodec
	{
		// Token: 0x0400678E RID: 26510
		UNCOMPRESSED,
		// Token: 0x0400678F RID: 26511
		SNAPPY,
		// Token: 0x04006790 RID: 26512
		GZIP,
		// Token: 0x04006791 RID: 26513
		LZO,
		// Token: 0x04006792 RID: 26514
		BROTLI,
		// Token: 0x04006793 RID: 26515
		LZ4,
		// Token: 0x04006794 RID: 26516
		ZSTD,
		// Token: 0x04006795 RID: 26517
		LZ4_RAW
	}
}
