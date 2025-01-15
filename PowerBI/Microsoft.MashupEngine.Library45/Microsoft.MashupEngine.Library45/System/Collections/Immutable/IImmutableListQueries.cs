using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200206E RID: 8302
	[NullableContext(1)]
	internal interface IImmutableListQueries<[Nullable(2)] T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06011424 RID: 70692
		ImmutableList<TOutput> ConvertAll<[Nullable(2)] TOutput>(Func<T, TOutput> converter);

		// Token: 0x06011425 RID: 70693
		void ForEach(Action<T> action);

		// Token: 0x06011426 RID: 70694
		ImmutableList<T> GetRange(int index, int count);

		// Token: 0x06011427 RID: 70695
		void CopyTo(T[] array);

		// Token: 0x06011428 RID: 70696
		void CopyTo(T[] array, int arrayIndex);

		// Token: 0x06011429 RID: 70697
		void CopyTo(int index, T[] array, int arrayIndex, int count);

		// Token: 0x0601142A RID: 70698
		bool Exists(Predicate<T> match);

		// Token: 0x0601142B RID: 70699
		[return: Nullable(2)]
		T Find(Predicate<T> match);

		// Token: 0x0601142C RID: 70700
		ImmutableList<T> FindAll(Predicate<T> match);

		// Token: 0x0601142D RID: 70701
		int FindIndex(Predicate<T> match);

		// Token: 0x0601142E RID: 70702
		int FindIndex(int startIndex, Predicate<T> match);

		// Token: 0x0601142F RID: 70703
		int FindIndex(int startIndex, int count, Predicate<T> match);

		// Token: 0x06011430 RID: 70704
		[return: Nullable(2)]
		T FindLast(Predicate<T> match);

		// Token: 0x06011431 RID: 70705
		int FindLastIndex(Predicate<T> match);

		// Token: 0x06011432 RID: 70706
		int FindLastIndex(int startIndex, Predicate<T> match);

		// Token: 0x06011433 RID: 70707
		int FindLastIndex(int startIndex, int count, Predicate<T> match);

		// Token: 0x06011434 RID: 70708
		bool TrueForAll(Predicate<T> match);

		// Token: 0x06011435 RID: 70709
		int BinarySearch(T item);

		// Token: 0x06011436 RID: 70710
		int BinarySearch(T item, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer);

		// Token: 0x06011437 RID: 70711
		int BinarySearch(int index, int count, T item, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer);
	}
}
