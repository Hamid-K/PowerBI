using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000020 RID: 32
	[NullableContext(1)]
	public interface IImmutableList<[Nullable(2)] T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600007D RID: 125
		IImmutableList<T> Clear();

		// Token: 0x0600007E RID: 126
		int IndexOf(T item, int index, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer);

		// Token: 0x0600007F RID: 127
		int LastIndexOf(T item, int index, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer);

		// Token: 0x06000080 RID: 128
		IImmutableList<T> Add(T value);

		// Token: 0x06000081 RID: 129
		IImmutableList<T> AddRange(IEnumerable<T> items);

		// Token: 0x06000082 RID: 130
		IImmutableList<T> Insert(int index, T element);

		// Token: 0x06000083 RID: 131
		IImmutableList<T> InsertRange(int index, IEnumerable<T> items);

		// Token: 0x06000084 RID: 132
		IImmutableList<T> Remove(T value, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer);

		// Token: 0x06000085 RID: 133
		IImmutableList<T> RemoveAll(Predicate<T> match);

		// Token: 0x06000086 RID: 134
		IImmutableList<T> RemoveRange(IEnumerable<T> items, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer);

		// Token: 0x06000087 RID: 135
		IImmutableList<T> RemoveRange(int index, int count);

		// Token: 0x06000088 RID: 136
		IImmutableList<T> RemoveAt(int index);

		// Token: 0x06000089 RID: 137
		IImmutableList<T> SetItem(int index, T value);

		// Token: 0x0600008A RID: 138
		IImmutableList<T> Replace(T oldValue, T newValue, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer);
	}
}
