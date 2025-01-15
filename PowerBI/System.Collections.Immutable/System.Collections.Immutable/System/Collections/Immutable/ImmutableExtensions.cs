using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000031 RID: 49
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ImmutableExtensions
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x00006280 File Offset: 0x00004480
		[NullableContext(2)]
		internal static bool IsValueType<T>()
		{
			if (default(T) != null)
			{
				return true;
			}
			Type typeFromHandle = typeof(T);
			return typeFromHandle.IsConstructedGenericType && typeFromHandle.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000062C8 File Offset: 0x000044C8
		internal static IOrderedCollection<T> AsOrderedCollection<[Nullable(2)] T>(this IEnumerable<T> sequence)
		{
			Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
			IOrderedCollection<T> orderedCollection = sequence as IOrderedCollection<T>;
			if (orderedCollection != null)
			{
				return orderedCollection;
			}
			IList<T> list = sequence as IList<T>;
			if (list != null)
			{
				return new ImmutableExtensions.ListOfTWrapper<T>(list);
			}
			return new ImmutableExtensions.FallbackWrapper<T>(sequence);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006303 File Offset: 0x00004503
		internal static void ClearFastWhenEmpty<[Nullable(2)] T>(this Stack<T> stack)
		{
			if (stack.Count > 0)
			{
				stack.Clear();
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006314 File Offset: 0x00004514
		[return: Nullable(new byte[] { 0, 1, 0 })]
		internal static DisposableEnumeratorAdapter<T, TEnumerator> GetEnumerableDisposable<[Nullable(2)] T, [Nullable(0)] TEnumerator>(this IEnumerable<T> enumerable) where TEnumerator : struct, IStrongEnumerator<T>, IEnumerator<T>
		{
			Requires.NotNull<IEnumerable<T>>(enumerable, "enumerable");
			IStrongEnumerable<T, TEnumerator> strongEnumerable = enumerable as IStrongEnumerable<T, TEnumerator>;
			if (strongEnumerable != null)
			{
				return new DisposableEnumeratorAdapter<T, TEnumerator>(strongEnumerable.GetEnumerator());
			}
			return new DisposableEnumeratorAdapter<T, TEnumerator>(enumerable.GetEnumerator());
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000634D File Offset: 0x0000454D
		internal static bool TryGetCount<[Nullable(2)] T>(this IEnumerable<T> sequence, out int count)
		{
			return sequence.TryGetCount(out count);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006358 File Offset: 0x00004558
		internal static bool TryGetCount<[Nullable(2)] T>(this IEnumerable sequence, out int count)
		{
			ICollection collection = sequence as ICollection;
			if (collection != null)
			{
				count = collection.Count;
				return true;
			}
			ICollection<T> collection2 = sequence as ICollection<T>;
			if (collection2 != null)
			{
				count = collection2.Count;
				return true;
			}
			IReadOnlyCollection<T> readOnlyCollection = sequence as IReadOnlyCollection<T>;
			if (readOnlyCollection != null)
			{
				count = readOnlyCollection.Count;
				return true;
			}
			count = 0;
			return false;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000063A8 File Offset: 0x000045A8
		internal static int GetCount<[Nullable(2)] T>(ref IEnumerable<T> sequence)
		{
			int count;
			if (!sequence.TryGetCount(out count))
			{
				List<T> list = sequence.ToList<T>();
				count = list.Count;
				sequence = list;
			}
			return count;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000063D4 File Offset: 0x000045D4
		internal static bool TryCopyTo<[Nullable(2)] T>(this IEnumerable<T> sequence, T[] array, int arrayIndex)
		{
			IList<T> list = sequence as IList<T>;
			if (list != null)
			{
				List<T> list2 = sequence as List<T>;
				if (list2 != null)
				{
					list2.CopyTo(array, arrayIndex);
					return true;
				}
				if (sequence.GetType() == typeof(T[]))
				{
					T[] array2 = (T[])sequence;
					Array.Copy(array2, 0, array, arrayIndex, array2.Length);
					return true;
				}
				if (sequence is ImmutableArray<T>)
				{
					ImmutableArray<T> immutableArray = (ImmutableArray<T>)sequence;
					Array.Copy(immutableArray.array, 0, array, arrayIndex, immutableArray.Length);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006450 File Offset: 0x00004650
		internal static T[] ToArray<[Nullable(2)] T>(this IEnumerable<T> sequence, int count)
		{
			Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
			Requires.Range(count >= 0, "count", null);
			if (count == 0)
			{
				return ImmutableArray<T>.Empty.array;
			}
			T[] array = new T[count];
			if (!sequence.TryCopyTo(array, 0))
			{
				int num = 0;
				foreach (T t in sequence)
				{
					Requires.Argument(num < count);
					array[num++] = t;
				}
				Requires.Argument(num == count);
			}
			return array;
		}

		// Token: 0x0200006B RID: 107
		private class ListOfTWrapper<T> : IOrderedCollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x060004B0 RID: 1200 RVA: 0x0000C308 File Offset: 0x0000A508
			internal ListOfTWrapper(IList<T> collection)
			{
				Requires.NotNull<IList<T>>(collection, "collection");
				this._collection = collection;
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000C322 File Offset: 0x0000A522
			public int Count
			{
				get
				{
					return this._collection.Count;
				}
			}

			// Token: 0x170000E4 RID: 228
			public T this[int index]
			{
				get
				{
					return this._collection[index];
				}
			}

			// Token: 0x060004B3 RID: 1203 RVA: 0x0000C33D File Offset: 0x0000A53D
			public IEnumerator<T> GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			// Token: 0x060004B4 RID: 1204 RVA: 0x0000C34A File Offset: 0x0000A54A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040000AC RID: 172
			private readonly IList<T> _collection;
		}

		// Token: 0x0200006C RID: 108
		private class FallbackWrapper<T> : IOrderedCollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x060004B5 RID: 1205 RVA: 0x0000C352 File Offset: 0x0000A552
			internal FallbackWrapper(IEnumerable<T> sequence)
			{
				Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
				this._sequence = sequence;
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000C36C File Offset: 0x0000A56C
			public int Count
			{
				get
				{
					if (this._collection == null)
					{
						int num;
						if (this._sequence.TryGetCount(out num))
						{
							return num;
						}
						this._collection = this._sequence.ToArray<T>();
					}
					return this._collection.Count;
				}
			}

			// Token: 0x170000E6 RID: 230
			public T this[int index]
			{
				get
				{
					if (this._collection == null)
					{
						this._collection = this._sequence.ToArray<T>();
					}
					return this._collection[index];
				}
			}

			// Token: 0x060004B8 RID: 1208 RVA: 0x0000C3D5 File Offset: 0x0000A5D5
			public IEnumerator<T> GetEnumerator()
			{
				return this._sequence.GetEnumerator();
			}

			// Token: 0x060004B9 RID: 1209 RVA: 0x0000C3E2 File Offset: 0x0000A5E2
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040000AD RID: 173
			private readonly IEnumerable<T> _sequence;

			// Token: 0x040000AE RID: 174
			private IList<T> _collection;
		}
	}
}
