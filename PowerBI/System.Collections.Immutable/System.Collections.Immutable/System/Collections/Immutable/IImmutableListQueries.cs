using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000021 RID: 33
	[NullableContext(1)]
	internal interface IImmutableListQueries<[Nullable(2)] T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600008B RID: 139
		ImmutableList<TOutput> ConvertAll<[Nullable(2)] TOutput>(Func<T, TOutput> converter);

		// Token: 0x0600008C RID: 140
		void ForEach(Action<T> action);

		// Token: 0x0600008D RID: 141
		ImmutableList<T> GetRange(int index, int count);

		// Token: 0x0600008E RID: 142
		void CopyTo(T[] array);

		// Token: 0x0600008F RID: 143
		void CopyTo(T[] array, int arrayIndex);

		// Token: 0x06000090 RID: 144
		void CopyTo(int index, T[] array, int arrayIndex, int count);

		// Token: 0x06000091 RID: 145
		bool Exists(Predicate<T> match);

		// Token: 0x06000092 RID: 146
		[return: Nullable(2)]
		T Find(Predicate<T> match);

		// Token: 0x06000093 RID: 147
		ImmutableList<T> FindAll(Predicate<T> match);

		// Token: 0x06000094 RID: 148
		int FindIndex(Predicate<T> match);

		// Token: 0x06000095 RID: 149
		int FindIndex(int startIndex, Predicate<T> match);

		// Token: 0x06000096 RID: 150
		int FindIndex(int startIndex, int count, Predicate<T> match);

		// Token: 0x06000097 RID: 151
		[return: Nullable(2)]
		T FindLast(Predicate<T> match);

		// Token: 0x06000098 RID: 152
		int FindLastIndex(Predicate<T> match);

		// Token: 0x06000099 RID: 153
		int FindLastIndex(int startIndex, Predicate<T> match);

		// Token: 0x0600009A RID: 154
		int FindLastIndex(int startIndex, int count, Predicate<T> match);

		// Token: 0x0600009B RID: 155
		bool TrueForAll(Predicate<T> match);

		// Token: 0x0600009C RID: 156
		int BinarySearch(T item);

		// Token: 0x0600009D RID: 157
		int BinarySearch(T item, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer);

		// Token: 0x0600009E RID: 158
		int BinarySearch(int index, int count, T item, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer);
	}
}
