using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Internal
{
	// Token: 0x020001B1 RID: 433
	public static class IListExtensions
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x0000F823 File Offset: 0x0000DA23
		public static IEnumerable<T> MergeSort<T>(this IList<IEnumerable<T>> items) where T : class
		{
			return items.MergeSort(Comparer<T>.Default);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0000F830 File Offset: 0x0000DA30
		public static IEnumerable<T> MergeSort<T>(this IList<IEnumerable<T>> items, IComparer<T> comparer) where T : class
		{
			return new IListExtensions.MergeEnumerable<T>(items, comparer);
		}

		// Token: 0x020001B2 RID: 434
		private class MergeEnumerable<T> : IEnumerable<T>, IEnumerable where T : class
		{
			// Token: 0x06000838 RID: 2104 RVA: 0x0000F839 File Offset: 0x0000DA39
			public MergeEnumerable(IList<IEnumerable<T>> items, IComparer<T> comparer)
			{
				this.items = items;
				this.comparer = comparer;
			}

			// Token: 0x06000839 RID: 2105 RVA: 0x0000F84F File Offset: 0x0000DA4F
			public IEnumerator<T> GetEnumerator()
			{
				return new IListExtensions.MergeEnumerable<T>.MergeEnumerator(this.items, this.comparer);
			}

			// Token: 0x0600083A RID: 2106 RVA: 0x0000F862 File Offset: 0x0000DA62
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040004AE RID: 1198
			private IList<IEnumerable<T>> items;

			// Token: 0x040004AF RID: 1199
			private IComparer<T> comparer;

			// Token: 0x020001B3 RID: 435
			private class MergeEnumerator : Enumerator<T>
			{
				// Token: 0x0600083B RID: 2107 RVA: 0x0000F86C File Offset: 0x0000DA6C
				public MergeEnumerator(IList<IEnumerable<T>> items, IComparer<T> comparer)
				{
					this.enumerators = new IEnumerator<T>[items.Count];
					this.mins = new T[items.Count];
					this.queue = new PriorityQueue<int>(items.Count, new IListExtensions.MergeEnumerable<T>.MergeEnumerator.MergeComparer(this.mins, comparer));
					for (int i = 0; i < items.Count; i++)
					{
						this.enumerators[i] = items[i].GetEnumerator();
						if (this.enumerators[i].MoveNext())
						{
							this.mins[i] = this.enumerators[i].Current;
							this.queue.Insert(i);
						}
					}
				}

				// Token: 0x0600083C RID: 2108 RVA: 0x0000F918 File Offset: 0x0000DB18
				public override void Dispose()
				{
					for (int i = 0; i < this.enumerators.Length; i++)
					{
						this.enumerators[i].Dispose();
					}
				}

				// Token: 0x1700026E RID: 622
				// (get) Token: 0x0600083D RID: 2109 RVA: 0x0000F945 File Offset: 0x0000DB45
				public override T Current
				{
					get
					{
						return this.current;
					}
				}

				// Token: 0x0600083E RID: 2110 RVA: 0x0000F950 File Offset: 0x0000DB50
				public override bool MoveNext()
				{
					if (this.queue.Count == 0)
					{
						return false;
					}
					int num = this.queue.Remove();
					this.current = this.mins[num];
					if (this.enumerators[num].MoveNext())
					{
						this.mins[num] = this.enumerators[num].Current;
						this.queue.Insert(num);
					}
					else
					{
						this.mins[num] = default(T);
					}
					return true;
				}

				// Token: 0x040004B0 RID: 1200
				private IEnumerator<T>[] enumerators;

				// Token: 0x040004B1 RID: 1201
				private T[] mins;

				// Token: 0x040004B2 RID: 1202
				private PriorityQueue<int> queue;

				// Token: 0x040004B3 RID: 1203
				private T current;

				// Token: 0x020001B4 RID: 436
				private class MergeComparer : IComparer<int>
				{
					// Token: 0x0600083F RID: 2111 RVA: 0x0000F9D6 File Offset: 0x0000DBD6
					public MergeComparer(T[] mins, IComparer<T> comparer)
					{
						this.mins = mins;
						this.comparer = comparer;
					}

					// Token: 0x06000840 RID: 2112 RVA: 0x0000F9EC File Offset: 0x0000DBEC
					public int Compare(int x, int y)
					{
						T t = this.mins[x];
						T t2 = this.mins[y];
						if (t == null)
						{
							return -1;
						}
						if (t2 == null)
						{
							return 1;
						}
						return this.comparer.Compare(t, t2);
					}

					// Token: 0x040004B4 RID: 1204
					private T[] mins;

					// Token: 0x040004B5 RID: 1205
					private IComparer<T> comparer;
				}
			}
		}
	}
}
