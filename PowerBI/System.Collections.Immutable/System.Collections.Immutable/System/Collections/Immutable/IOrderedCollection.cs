using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000028 RID: 40
	[NullableContext(1)]
	internal interface IOrderedCollection<[Nullable(2)] out T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000FD RID: 253
		int Count { get; }

		// Token: 0x1700002F RID: 47
		T this[int index] { get; }
	}
}
