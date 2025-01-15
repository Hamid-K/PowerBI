using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200001E RID: 30
	public sealed class BoolColumnReader : ColumnReader<bool>
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x000048C0 File Offset: 0x00002AC0
		[NullableContext(1)]
		internal BoolColumnReader(ParquetHandle handle, RowGroupReader rowGroupReader, ColumnChunkMetaData columnChunkMetaData, int columnIndex)
			: base(handle, rowGroupReader, columnChunkMetaData, columnIndex)
		{
		}
	}
}
