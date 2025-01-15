using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000024 RID: 36
	[NullableContext(1)]
	public interface IImmutableStack<[Nullable(2)] T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B3 RID: 179
		bool IsEmpty { get; }

		// Token: 0x060000B4 RID: 180
		IImmutableStack<T> Clear();

		// Token: 0x060000B5 RID: 181
		IImmutableStack<T> Push(T value);

		// Token: 0x060000B6 RID: 182
		IImmutableStack<T> Pop();

		// Token: 0x060000B7 RID: 183
		T Peek();
	}
}
