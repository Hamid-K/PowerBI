using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200005E RID: 94
	[NullableContext(1)]
	[Nullable(0)]
	public class LogicalReadConverterFactory
	{
		// Token: 0x06000284 RID: 644 RVA: 0x0000A0C0 File Offset: 0x000082C0
		[NullableContext(2)]
		public virtual Delegate GetDirectReader<TLogical, [IsUnmanaged, Nullable(0)] TPhysical>() where TPhysical : struct
		{
			return LogicalRead<TLogical, TPhysical>.GetDirectReader();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000A0C8 File Offset: 0x000082C8
		public virtual Delegate GetConverter<[Nullable(2)] TLogical, [IsUnmanaged, Nullable(0)] TPhysical>(ColumnDescriptor columnDescriptor, ColumnChunkMetaData columnChunkMetaData) where TPhysical : struct
		{
			return LogicalRead<TLogical, TPhysical>.GetConverter(columnDescriptor, columnChunkMetaData);
		}

		// Token: 0x040000BC RID: 188
		public static readonly LogicalReadConverterFactory Default = new LogicalReadConverterFactory();
	}
}
