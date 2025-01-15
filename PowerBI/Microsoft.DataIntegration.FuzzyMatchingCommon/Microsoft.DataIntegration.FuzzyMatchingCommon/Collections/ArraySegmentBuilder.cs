using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	public sealed class ArraySegmentBuilder<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IArray<T>, IMemoryUsage
	{
		// Token: 0x06000367 RID: 871 RVA: 0x00018F40 File Offset: 0x00017140
		public static implicit operator ArraySegment<T>(ArraySegmentBuilder<T> list)
		{
			if (list.Array == null)
			{
				return default(ArraySegment<T>);
			}
			return new ArraySegment<T>(list.Array, 0, list.Count);
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00018F71 File Offset: 0x00017171
		// (set) Token: 0x06000369 RID: 873 RVA: 0x00018F85 File Offset: 0x00017185
		public int Capacity
		{
			get
			{
				if (this.Array != null)
				{
					return this.Array.Length;
				}
				return 0;
			}
			set
			{
				if (this.Count != 0)
				{
					throw new InvalidOperationException("Capacity may only be set when Count is zero.");
				}
				if (this.Array.Length < this.Capacity)
				{
					this.Array = new T[this.Capacity];
				}
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00018FBB File Offset: 0x000171BB
		public ArraySegmentBuilder()
		{
			this.Count = 0;
			this.Array = null;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00018FD1 File Offset: 0x000171D1
		public ArraySegmentBuilder(int capacity)
		{
			this.Count = 0;
			this.Array = new T[this.Capacity];
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00018FF1 File Offset: 0x000171F1
		public long MemoryUsage
		{
			get
			{
				return (long)(this.Capacity * Utilities.SizeOf(typeof(T)) + 24);
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001900D File Offset: 0x0001720D
		public ArraySegmentBuilder<T> Clone(ISegmentAllocator<T> allocator)
		{
			return new ArraySegmentBuilder<T>
			{
				Array = (T[])this.Array.Clone(),
				Count = this.Count
			};
		}

		// Token: 0x17000082 RID: 130
		public T this[int index]
		{
			get
			{
				if (index >= this.Count)
				{
					throw new IndexOutOfRangeException();
				}
				return this.Array[index];
			}
			set
			{
				if (index >= this.Count)
				{
					throw new IndexOutOfRangeException();
				}
				this.Array[index] = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00019071 File Offset: 0x00017271
		int IArray<T>.Length
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00019079 File Offset: 0x00017279
		public int IndexOf(T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00019080 File Offset: 0x00017280
		public void Insert(int index, T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00019087 File Offset: 0x00017287
		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0001908E File Offset: 0x0001728E
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00019094 File Offset: 0x00017294
		public void Add(T item)
		{
			if (this.Array == null)
			{
				this.Array = new T[2];
			}
			else if (this.Array.Length == this.Count)
			{
				global::System.Array.Resize<T>(ref this.Array, Math.Max(this.Count + 1, this.Count * 2));
			}
			T[] array = this.Array;
			int count = this.Count;
			this.Count = count + 1;
			array[count] = item;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00019104 File Offset: 0x00017304
		public void Add(ArraySegment<T> items)
		{
			for (int i = 0; i < items.Count; i++)
			{
				this.Add(items.Array[items.Offset + i]);
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00019140 File Offset: 0x00017340
		public void Add(IEnumerable<T> items)
		{
			foreach (T t in items)
			{
				this.Add(t);
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00019188 File Offset: 0x00017388
		public void Reset()
		{
			this.Count = 0;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00019191 File Offset: 0x00017391
		public void Sort(IComparer<T> comparer)
		{
			global::System.Array.Sort<T>(this.Array, 0, this.Count, comparer);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000191A6 File Offset: 0x000173A6
		public bool Contains(T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000191AD File Offset: 0x000173AD
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (this.Count > 0)
			{
				global::System.Array.ConstrainedCopy(this.Array, 0, array, arrayIndex, this.Count);
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000191CC File Offset: 0x000173CC
		public ArraySegment<T> ToSegment(ISegmentAllocator<T> allocator)
		{
			ArraySegment<T> arraySegment = allocator.New(this.Count);
			if (this.Count > 0)
			{
				global::System.Array.ConstrainedCopy(this.Array, 0, arraySegment.Array, arraySegment.Offset, this.Count);
			}
			return arraySegment;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00019210 File Offset: 0x00017410
		public T[] ToArray()
		{
			T[] array = new T[this.Count];
			if (this.Count > 0)
			{
				global::System.Array.ConstrainedCopy(this.Array, 0, array, 0, this.Count);
			}
			return array;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00019247 File Offset: 0x00017447
		public bool Remove(T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0001924E File Offset: 0x0001744E
		int ICollection<T>.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00019256 File Offset: 0x00017456
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00019259 File Offset: 0x00017459
		void ICollection<T>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00019262 File Offset: 0x00017462
		void ICollection<T>.Clear()
		{
			this.Reset();
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001926A File Offset: 0x0001746A
		bool ICollection<T>.Contains(T item)
		{
			return this.Contains(item);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00019273 File Offset: 0x00017473
		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			this.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001927D File Offset: 0x0001747D
		bool ICollection<T>.Remove(T item)
		{
			return this.Remove(item);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00019286 File Offset: 0x00017486
		public IEnumerator<T> GetEnumerator()
		{
			return new ArraySegmentBuilder<T>.Enumerator
			{
				Array = this.Array,
				Count = this.Count
			};
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000192A5 File Offset: 0x000174A5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ArraySegmentBuilder<T>.Enumerator
			{
				Array = this.Array,
				Count = this.Count
			};
		}

		// Token: 0x0400008B RID: 139
		public T[] Array;

		// Token: 0x0400008C RID: 140
		public int Count;

		// Token: 0x020000E4 RID: 228
		private class Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x1700016B RID: 363
			// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0002CABF File Offset: 0x0002ACBF
			public T Current
			{
				get
				{
					return this.Array[this.Position];
				}
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x060008DA RID: 2266 RVA: 0x0002CAD2 File Offset: 0x0002ACD2
			object IEnumerator.Current
			{
				get
				{
					return this.Array[this.Position];
				}
			}

			// Token: 0x060008DB RID: 2267 RVA: 0x0002CAEA File Offset: 0x0002ACEA
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060008DC RID: 2268 RVA: 0x0002CAEC File Offset: 0x0002ACEC
			public bool MoveNext()
			{
				if (this.Position + 1 < this.Count)
				{
					this.Position++;
					return true;
				}
				return false;
			}

			// Token: 0x060008DD RID: 2269 RVA: 0x0002CB0F File Offset: 0x0002AD0F
			public void Reset()
			{
				this.Position = -1;
			}

			// Token: 0x04000246 RID: 582
			public T[] Array;

			// Token: 0x04000247 RID: 583
			public int Count;

			// Token: 0x04000248 RID: 584
			public int Position = -1;
		}
	}
}
