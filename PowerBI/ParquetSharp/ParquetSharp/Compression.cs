using System;

namespace ParquetSharp
{
	// Token: 0x02000030 RID: 48
	public enum Compression
	{
		// Token: 0x0400003E RID: 62
		Uncompressed,
		// Token: 0x0400003F RID: 63
		Snappy,
		// Token: 0x04000040 RID: 64
		Gzip,
		// Token: 0x04000041 RID: 65
		Brotli,
		// Token: 0x04000042 RID: 66
		Zstd,
		// Token: 0x04000043 RID: 67
		Lz4,
		// Token: 0x04000044 RID: 68
		Lz4Frame,
		// Token: 0x04000045 RID: 69
		Lzo,
		// Token: 0x04000046 RID: 70
		Bz2,
		// Token: 0x04000047 RID: 71
		Lz4Hadoop
	}
}
