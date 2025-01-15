using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200002E RID: 46
	public sealed class ByteArrayColumnWriter : ColumnWriter<ByteArray>
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00005124 File Offset: 0x00003324
		[NullableContext(1)]
		internal ByteArrayColumnWriter(IntPtr handle, RowGroupWriter rowGroupWriter, int columnIndex)
			: base(handle, rowGroupWriter, columnIndex)
		{
		}
	}
}
