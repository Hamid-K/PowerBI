using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02002082 RID: 8322
	[NullableContext(1)]
	internal interface IOrderedCollection<[Nullable(2)] out T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x17002E40 RID: 11840
		// (get) Token: 0x060114EB RID: 70891
		int Count { get; }

		// Token: 0x17002E41 RID: 11841
		T this[int index] { get; }
	}
}
