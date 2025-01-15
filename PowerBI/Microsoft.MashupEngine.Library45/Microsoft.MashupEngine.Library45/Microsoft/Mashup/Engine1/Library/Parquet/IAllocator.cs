using System;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F1A RID: 7962
	internal interface IAllocator
	{
		// Token: 0x06010C30 RID: 68656
		ByteArray Allocate(int length);
	}
}
