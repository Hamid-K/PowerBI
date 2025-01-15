using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200206F RID: 8303
	[NullableContext(1)]
	public interface IImmutableQueue<[Nullable(2)] T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x17002E21 RID: 11809
		// (get) Token: 0x06011438 RID: 70712
		bool IsEmpty { get; }

		// Token: 0x06011439 RID: 70713
		IImmutableQueue<T> Clear();

		// Token: 0x0601143A RID: 70714
		T Peek();

		// Token: 0x0601143B RID: 70715
		IImmutableQueue<T> Enqueue(T value);

		// Token: 0x0601143C RID: 70716
		IImmutableQueue<T> Dequeue();
	}
}
