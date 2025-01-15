using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000025 RID: 37
	public sealed class FixedLenByteArrayColumnReader : ColumnReader<FixedLenByteArray>
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00004930 File Offset: 0x00002B30
		[NullableContext(1)]
		internal FixedLenByteArrayColumnReader(ParquetHandle handle, RowGroupReader rowGroupReader, ColumnChunkMetaData columnChunkMetaData, int columnIndex)
			: base(handle, rowGroupReader, columnChunkMetaData, columnIndex)
		{
		}
	}
}
