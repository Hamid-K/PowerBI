using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000027 RID: 39
	[NullableContext(1)]
	internal interface IStrongEnumerator<[Nullable(2)] T>
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000FB RID: 251
		T Current { get; }

		// Token: 0x060000FC RID: 252
		bool MoveNext();
	}
}
