using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000022 RID: 34
	[NullableContext(1)]
	public interface IImmutableQueue<[Nullable(2)] T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009F RID: 159
		bool IsEmpty { get; }

		// Token: 0x060000A0 RID: 160
		IImmutableQueue<T> Clear();

		// Token: 0x060000A1 RID: 161
		T Peek();

		// Token: 0x060000A2 RID: 162
		IImmutableQueue<T> Enqueue(T value);

		// Token: 0x060000A3 RID: 163
		IImmutableQueue<T> Dequeue();
	}
}
