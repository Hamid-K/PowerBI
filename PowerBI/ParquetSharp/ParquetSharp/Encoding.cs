using System;

namespace ParquetSharp
{
	// Token: 0x02000036 RID: 54
	public enum Encoding
	{
		// Token: 0x04000060 RID: 96
		Plain,
		// Token: 0x04000061 RID: 97
		PlainDictionary = 2,
		// Token: 0x04000062 RID: 98
		Rle,
		// Token: 0x04000063 RID: 99
		BitPacked,
		// Token: 0x04000064 RID: 100
		DeltaBinaryPacked,
		// Token: 0x04000065 RID: 101
		DeltaLengthByteArray,
		// Token: 0x04000066 RID: 102
		DeltaByteArray,
		// Token: 0x04000067 RID: 103
		RleDictionary,
		// Token: 0x04000068 RID: 104
		ByteStreamSplit,
		// Token: 0x04000069 RID: 105
		Undefined,
		// Token: 0x0400006A RID: 106
		Unknown = 999
	}
}
