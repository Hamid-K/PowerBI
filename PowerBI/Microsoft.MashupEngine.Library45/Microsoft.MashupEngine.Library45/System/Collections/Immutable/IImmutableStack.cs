using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02002071 RID: 8305
	[NullableContext(1)]
	public interface IImmutableStack<[Nullable(2)] T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x17002E22 RID: 11810
		// (get) Token: 0x0601144C RID: 70732
		bool IsEmpty { get; }

		// Token: 0x0601144D RID: 70733
		IImmutableStack<T> Clear();

		// Token: 0x0601144E RID: 70734
		IImmutableStack<T> Push(T value);

		// Token: 0x0601144F RID: 70735
		IImmutableStack<T> Pop();

		// Token: 0x06011450 RID: 70736
		T Peek();
	}
}
