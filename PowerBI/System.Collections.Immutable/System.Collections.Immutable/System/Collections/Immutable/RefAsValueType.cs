using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000044 RID: 68
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{Value,nq}")]
	internal struct RefAsValueType<[Nullable(2)] T>
	{
		// Token: 0x06000369 RID: 873 RVA: 0x000091F4 File Offset: 0x000073F4
		internal RefAsValueType(T value)
		{
			this.Value = value;
		}

		// Token: 0x0400003F RID: 63
		internal T Value;
	}
}
