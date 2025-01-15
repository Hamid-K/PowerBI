using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000074 RID: 116
	[NullableContext(1)]
	[Nullable(0)]
	public class LogicalWriteConverterFactory
	{
		// Token: 0x06000304 RID: 772 RVA: 0x0000C7FC File Offset: 0x0000A9FC
		public virtual Delegate GetConverter<[Nullable(2)] TLogical, [IsUnmanaged, Nullable(0)] TPhysical>(ColumnDescriptor columnDescriptor, [Nullable(2)] ByteBuffer byteBuffer) where TPhysical : struct
		{
			return LogicalWrite<TLogical, TPhysical>.GetConverter(columnDescriptor, byteBuffer);
		}

		// Token: 0x040000D8 RID: 216
		public static readonly LogicalWriteConverterFactory Default = new LogicalWriteConverterFactory();
	}
}
