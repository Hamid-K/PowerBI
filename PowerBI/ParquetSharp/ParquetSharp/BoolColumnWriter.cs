using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000028 RID: 40
	public sealed class BoolColumnWriter : ColumnWriter<bool>
	{
		// Token: 0x06000127 RID: 295 RVA: 0x000050DC File Offset: 0x000032DC
		[NullableContext(1)]
		internal BoolColumnWriter(IntPtr handle, RowGroupWriter rowGroupWriter, int columnIndex)
			: base(handle, rowGroupWriter, columnIndex)
		{
		}
	}
}
