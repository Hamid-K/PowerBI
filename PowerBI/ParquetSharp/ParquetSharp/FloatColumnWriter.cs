using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200002C RID: 44
	public sealed class FloatColumnWriter : ColumnWriter<float>
	{
		// Token: 0x0600012B RID: 299 RVA: 0x0000510C File Offset: 0x0000330C
		[NullableContext(1)]
		internal FloatColumnWriter(IntPtr handle, RowGroupWriter rowGroupWriter, int columnIndex)
			: base(handle, rowGroupWriter, columnIndex)
		{
		}
	}
}
