using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000024 RID: 36
	public sealed class ByteArrayColumnReader : ColumnReader<ByteArray>
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00004920 File Offset: 0x00002B20
		[NullableContext(1)]
		internal ByteArrayColumnReader(ParquetHandle handle, RowGroupReader rowGroupReader, ColumnChunkMetaData columnChunkMetaData, int columnIndex)
			: base(handle, rowGroupReader, columnChunkMetaData, columnIndex)
		{
		}
	}
}
