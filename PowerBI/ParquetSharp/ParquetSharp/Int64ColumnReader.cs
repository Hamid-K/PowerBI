using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000020 RID: 32
	public sealed class Int64ColumnReader : ColumnReader<long>
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x000048E0 File Offset: 0x00002AE0
		[NullableContext(1)]
		internal Int64ColumnReader(ParquetHandle handle, RowGroupReader rowGroupReader, ColumnChunkMetaData columnChunkMetaData, int columnIndex)
			: base(handle, rowGroupReader, columnChunkMetaData, columnIndex)
		{
		}
	}
}
