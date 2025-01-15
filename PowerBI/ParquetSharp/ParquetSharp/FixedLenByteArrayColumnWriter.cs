using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200002F RID: 47
	public sealed class FixedLenByteArrayColumnWriter : ColumnWriter<FixedLenByteArray>
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00005130 File Offset: 0x00003330
		[NullableContext(1)]
		internal FixedLenByteArrayColumnWriter(IntPtr handle, RowGroupWriter rowGroupWriter, int columnIndex)
			: base(handle, rowGroupWriter, columnIndex)
		{
		}
	}
}
