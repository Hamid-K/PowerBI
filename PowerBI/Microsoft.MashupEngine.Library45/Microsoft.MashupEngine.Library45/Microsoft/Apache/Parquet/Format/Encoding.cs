using System;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x0200200B RID: 8203
	internal enum Encoding
	{
		// Token: 0x040067C0 RID: 26560
		PLAIN,
		// Token: 0x040067C1 RID: 26561
		PLAIN_DICTIONARY = 2,
		// Token: 0x040067C2 RID: 26562
		RLE,
		// Token: 0x040067C3 RID: 26563
		BIT_PACKED,
		// Token: 0x040067C4 RID: 26564
		DELTA_BINARY_PACKED,
		// Token: 0x040067C5 RID: 26565
		DELTA_LENGTH_BYTE_ARRAY,
		// Token: 0x040067C6 RID: 26566
		DELTA_BYTE_ARRAY,
		// Token: 0x040067C7 RID: 26567
		RLE_DICTIONARY,
		// Token: 0x040067C8 RID: 26568
		BYTE_STREAM_SPLIT
	}
}
