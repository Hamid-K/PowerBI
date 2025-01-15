using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200002D RID: 45
	public sealed class DoubleColumnWriter : ColumnWriter<double>
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00005118 File Offset: 0x00003318
		[NullableContext(1)]
		internal DoubleColumnWriter(IntPtr handle, RowGroupWriter rowGroupWriter, int columnIndex)
			: base(handle, rowGroupWriter, columnIndex)
		{
		}
	}
}
