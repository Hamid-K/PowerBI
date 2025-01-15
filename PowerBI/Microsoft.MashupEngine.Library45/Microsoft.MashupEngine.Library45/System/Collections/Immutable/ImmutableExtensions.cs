using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020A1 RID: 8353
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ImmutableExtensions
	{
		// Token: 0x060116AD RID: 71341 RVA: 0x003BBFF8 File Offset: 0x003BA1F8
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

		// Token: 0x060116AE RID: 71342 RVA: 0x003BC044 File Offset: 0x003BA244
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

		// Token: 0x060116AF RID: 71343 RVA: 0x003BC07F File Offset: 0x003BA27F
		internal static void ClearFastWhenEmpty<[Nullable(2)] T>(this Stack<T> stack)
		{
			if (stack.Count > 0)
			{
				stack.Clear();
			}
		}

		// Token: 0x060116B0 RID: 71344 RVA: 0x003BC090 File Offset: 0x003BA290
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

		// Token: 0x060116B1 RID: 71345 RVA: 0x003BC0C9 File Offset: 0x003BA2C9
		internal static bool TryGetCount<[Nullable(2)] T>(this IEnumerable<T> sequence, out int count)
		{
			return sequence.TryGetCount(out count);
		}

		// Token: 0x060116B2 RID: 71346 RVA: 0x003BC0D4 File Offset: 0x003BA2D4
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

		// Token: 0x060116B3 RID: 71347 RVA: 0x003BC124 File Offset: 0x003BA324
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

		// Token: 0x060116B4 RID: 71348 RVA: 0x003BC150 File Offset: 0x003BA350
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

		// Token: 0x060116B5 RID: 71349 RVA: 0x003BC1D0 File Offset: 0x003BA3D0
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

		// Token: 0x020020A2 RID: 8354
		private class ListOfTWrapper<T> : IOrderedCollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x060116B6 RID: 71350 RVA: 0x003BC270 File Offset: 0x003BA470
			internal ListOfTWrapper(IList<T> collection)
			{
				Requires.NotNull<IList<T>>(collection, "collection");
				this._collection = collection;
			}

			// Token: 0x17002EA2 RID: 11938
			// (get) Token: 0x060116B7 RID: 71351 RVA: 0x003BC28A File Offset: 0x003BA48A
			public int Count
			{
				get
				{
					return this._collection.Count;
				}
			}

			// Token: 0x17002EA3 RID: 11939
			public T this[int index]
			{
				get
				{
					return this._collection[index];
				}
			}

			// Token: 0x060116B9 RID: 71353 RVA: 0x003BC2A5 File Offset: 0x003BA4A5
			public IEnumerator<T> GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			// Token: 0x060116BA RID: 71354 RVA: 0x003BC2B2 File Offset: 0x003BA4B2
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400692C RID: 26924
			private readonly IList<T> _collection;
		}

		// Token: 0x020020A3 RID: 8355
		private class FallbackWrapper<T> : IOrderedCollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x060116BB RID: 71355 RVA: 0x003BC2BA File Offset: 0x003BA4BA
			internal FallbackWrapper(IEnumerable<T> sequence)
			{
				Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
				this._sequence = sequence;
			}

			// Token: 0x17002EA4 RID: 11940
			// (get) Token: 0x060116BC RID: 71356 RVA: 0x003BC2D4 File Offset: 0x003BA4D4
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

			// Token: 0x17002EA5 RID: 11941
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

			// Token: 0x060116BE RID: 71358 RVA: 0x003BC33D File Offset: 0x003BA53D
			public IEnumerator<T> GetEnumerator()
			{
				return this._sequence.GetEnumerator();
			}

			// Token: 0x060116BF RID: 71359 RVA: 0x003BC34A File Offset: 0x003BA54A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400692D RID: 26925
			private readonly IEnumerable<T> _sequence;

			// Token: 0x0400692E RID: 26926
			private IList<T> _collection;
		}
	}
}
