using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020C6 RID: 8390
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{Value,nq}")]
	internal struct RefAsValueType<[Nullable(2)] T>
	{
		// Token: 0x060119C1 RID: 72129 RVA: 0x003C3598 File Offset: 0x003C1798
		internal RefAsValueType(T value)
		{
			this.Value = value;
		}

		// Token: 0x04006999 RID: 27033
		internal T Value;
	}
}
