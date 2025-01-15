using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000013 RID: 19
	public sealed class Column<[Nullable(2)] TLogicalType> : Column
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00003140 File Offset: 0x00001340
		[NullableContext(1)]
		public Column(string name, [Nullable(2)] LogicalType logicalTypeOverride = null)
			: base(typeof(TLogicalType), name, logicalTypeOverride)
		{
		}
	}
}
