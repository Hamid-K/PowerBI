using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000042 RID: 66
	[NullableContext(1)]
	public interface ILogicalColumnReaderVisitor<[Nullable(2)] out TReturn>
	{
		// Token: 0x060001EE RID: 494
		TReturn OnLogicalColumnReader<[Nullable(2)] TValue>(LogicalColumnReader<TValue> columnReader);
	}
}
