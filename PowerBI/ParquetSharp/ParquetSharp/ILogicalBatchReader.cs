using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000046 RID: 70
	[NullableContext(2)]
	internal interface ILogicalBatchReader<TElement>
	{
		// Token: 0x06000200 RID: 512
		int ReadBatch([Nullable(new byte[] { 0, 1 })] Span<TElement> destination);

		// Token: 0x06000201 RID: 513
		bool HasNext();
	}
}
