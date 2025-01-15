using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000022 RID: 34
	public sealed class FloatColumnReader : ColumnReader<float>
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00004900 File Offset: 0x00002B00
		[NullableContext(1)]
		internal FloatColumnReader(ParquetHandle handle, RowGroupReader rowGroupReader, ColumnChunkMetaData columnChunkMetaData, int columnIndex)
			: base(handle, rowGroupReader, columnChunkMetaData, columnIndex)
		{
		}
	}
}
