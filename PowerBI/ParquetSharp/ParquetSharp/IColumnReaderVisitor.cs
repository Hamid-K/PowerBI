using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000040 RID: 64
	public interface IColumnReaderVisitor<[Nullable(2)] out TReturn>
	{
		// Token: 0x060001EC RID: 492
		[return: Nullable(1)]
		TReturn OnColumnReader<[IsUnmanaged] TValue>([Nullable(new byte[] { 1, 0 })] ColumnReader<TValue> columnReader) where TValue : struct;
	}
}
