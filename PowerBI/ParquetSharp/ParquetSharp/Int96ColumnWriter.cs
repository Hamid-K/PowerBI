using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200002B RID: 43
	public sealed class Int96ColumnWriter : ColumnWriter<Int96>
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00005100 File Offset: 0x00003300
		[NullableContext(1)]
		internal Int96ColumnWriter(IntPtr handle, RowGroupWriter rowGroupWriter, int columnIndex)
			: base(handle, rowGroupWriter, columnIndex)
		{
		}
	}
}
