using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000029 RID: 41
	public sealed class Int32ColumnWriter : ColumnWriter<int>
	{
		// Token: 0x06000128 RID: 296 RVA: 0x000050E8 File Offset: 0x000032E8
		[NullableContext(1)]
		internal Int32ColumnWriter(IntPtr handle, RowGroupWriter rowGroupWriter, int columnIndex)
			: base(handle, rowGroupWriter, columnIndex)
		{
		}
	}
}
