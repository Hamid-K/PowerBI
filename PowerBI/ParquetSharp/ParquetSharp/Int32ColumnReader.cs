using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200001F RID: 31
	public sealed class Int32ColumnReader : ColumnReader<int>
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x000048D0 File Offset: 0x00002AD0
		[NullableContext(1)]
		internal Int32ColumnReader(ParquetHandle handle, RowGroupReader rowGroupReader, ColumnChunkMetaData columnChunkMetaData, int columnIndex)
			: base(handle, rowGroupReader, columnChunkMetaData, columnIndex)
		{
		}
	}
}
