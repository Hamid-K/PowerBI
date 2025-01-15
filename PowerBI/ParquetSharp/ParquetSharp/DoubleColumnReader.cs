using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000023 RID: 35
	public sealed class DoubleColumnReader : ColumnReader<double>
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00004910 File Offset: 0x00002B10
		[NullableContext(1)]
		internal DoubleColumnReader(ParquetHandle handle, RowGroupReader rowGroupReader, ColumnChunkMetaData columnChunkMetaData, int columnIndex)
			: base(handle, rowGroupReader, columnChunkMetaData, columnIndex)
		{
		}
	}
}
