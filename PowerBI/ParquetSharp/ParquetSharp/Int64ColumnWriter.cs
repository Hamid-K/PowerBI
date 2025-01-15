using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200002A RID: 42
	public sealed class Int64ColumnWriter : ColumnWriter<long>
	{
		// Token: 0x06000129 RID: 297 RVA: 0x000050F4 File Offset: 0x000032F4
		[NullableContext(1)]
		internal Int64ColumnWriter(IntPtr handle, RowGroupWriter rowGroupWriter, int columnIndex)
			: base(handle, rowGroupWriter, columnIndex)
		{
		}
	}
}
