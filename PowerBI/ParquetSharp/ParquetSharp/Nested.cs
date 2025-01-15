using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000077 RID: 119
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct Nested<[Nullable(2)] T>
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0000C95C File Offset: 0x0000AB5C
		public Nested(T value)
		{
			this.Value = value;
		}

		// Token: 0x040000DA RID: 218
		public readonly T Value;
	}
}
