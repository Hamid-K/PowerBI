using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02002070 RID: 8304
	[NullableContext(1)]
	public interface IImmutableSet<[Nullable(2)] T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0601143D RID: 70717
		IImmutableSet<T> Clear();

		// Token: 0x0601143E RID: 70718
		bool Contains(T value);

		// Token: 0x0601143F RID: 70719
		IImmutableSet<T> Add(T value);

		// Token: 0x06011440 RID: 70720
		IImmutableSet<T> Remove(T value);

		// Token: 0x06011441 RID: 70721
		bool TryGetValue(T equalValue, out T actualValue);

		// Token: 0x06011442 RID: 70722
		IImmutableSet<T> Intersect(IEnumerable<T> other);

		// Token: 0x06011443 RID: 70723
		IImmutableSet<T> Except(IEnumerable<T> other);

		// Token: 0x06011444 RID: 70724
		IImmutableSet<T> SymmetricExcept(IEnumerable<T> other);

		// Token: 0x06011445 RID: 70725
		IImmutableSet<T> Union(IEnumerable<T> other);

		// Token: 0x06011446 RID: 70726
		bool SetEquals(IEnumerable<T> other);

		// Token: 0x06011447 RID: 70727
		bool IsProperSubsetOf(IEnumerable<T> other);

		// Token: 0x06011448 RID: 70728
		bool IsProperSupersetOf(IEnumerable<T> other);

		// Token: 0x06011449 RID: 70729
		bool IsSubsetOf(IEnumerable<T> other);

		// Token: 0x0601144A RID: 70730
		bool IsSupersetOf(IEnumerable<T> other);

		// Token: 0x0601144B RID: 70731
		bool Overlaps(IEnumerable<T> other);
	}
}
