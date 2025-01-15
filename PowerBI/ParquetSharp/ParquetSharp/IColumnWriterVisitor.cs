using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000041 RID: 65
	public interface IColumnWriterVisitor<[Nullable(2)] out TReturn>
	{
		// Token: 0x060001ED RID: 493
		[return: Nullable(1)]
		TReturn OnColumnWriter<[IsUnmanaged] TValue>([Nullable(new byte[] { 1, 0 })] ColumnWriter<TValue> columnWriter) where TValue : struct;
	}
}
