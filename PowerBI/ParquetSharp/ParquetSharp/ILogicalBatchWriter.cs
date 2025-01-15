using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200004F RID: 79
	[NullableContext(2)]
	internal interface ILogicalBatchWriter<TElement>
	{
		// Token: 0x06000221 RID: 545
		void WriteBatch([Nullable(new byte[] { 0, 1 })] ReadOnlySpan<TElement> values);
	}
}
