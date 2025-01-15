using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000021 RID: 33
	public sealed class Int96ColumnReader : ColumnReader<Int96>
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x000048F0 File Offset: 0x00002AF0
		[NullableContext(1)]
		internal Int96ColumnReader(ParquetHandle handle, RowGroupReader rowGroupReader, ColumnChunkMetaData columnChunkMetaData, int columnIndex)
			: base(handle, rowGroupReader, columnChunkMetaData, columnIndex)
		{
		}
	}
}
