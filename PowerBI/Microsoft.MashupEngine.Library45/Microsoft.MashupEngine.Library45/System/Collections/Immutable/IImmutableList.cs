using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200206D RID: 8301
	[NullableContext(1)]
	public interface IImmutableList<[Nullable(2)] T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06011416 RID: 70678
		IImmutableList<T> Clear();

		// Token: 0x06011417 RID: 70679
		int IndexOf(T item, int index, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer);

		// Token: 0x06011418 RID: 70680
		int LastIndexOf(T item, int index, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer);

		// Token: 0x06011419 RID: 70681
		IImmutableList<T> Add(T value);

		// Token: 0x0601141A RID: 70682
		IImmutableList<T> AddRange(IEnumerable<T> items);

		// Token: 0x0601141B RID: 70683
		IImmutableList<T> Insert(int index, T element);

		// Token: 0x0601141C RID: 70684
		IImmutableList<T> InsertRange(int index, IEnumerable<T> items);

		// Token: 0x0601141D RID: 70685
		IImmutableList<T> Remove(T value, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer);

		// Token: 0x0601141E RID: 70686
		IImmutableList<T> RemoveAll(Predicate<T> match);

		// Token: 0x0601141F RID: 70687
		IImmutableList<T> RemoveRange(IEnumerable<T> items, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer);

		// Token: 0x06011420 RID: 70688
		IImmutableList<T> RemoveRange(int index, int count);

		// Token: 0x06011421 RID: 70689
		IImmutableList<T> RemoveAt(int index);

		// Token: 0x06011422 RID: 70690
		IImmutableList<T> SetItem(int index, T value);

		// Token: 0x06011423 RID: 70691
		IImmutableList<T> Replace(T oldValue, T newValue, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer);
	}
}
