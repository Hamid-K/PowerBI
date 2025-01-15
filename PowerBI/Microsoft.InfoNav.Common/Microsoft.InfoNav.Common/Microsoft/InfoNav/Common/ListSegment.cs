using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000061 RID: 97
	[ImmutableObject(true)]
	internal class ListSegment<T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060003A4 RID: 932 RVA: 0x00009CA8 File Offset: 0x00007EA8
		internal ListSegment(IList<T> list, int offset, int count)
		{
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (list.Count - offset < count)
			{
				throw new ArgumentException();
			}
			this._list = list;
			this._offset = offset;
			this._count = count;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00009CFF File Offset: 0x00007EFF
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00009D07 File Offset: 0x00007F07
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700004F RID: 79
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this._count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._list[this._offset + index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00009D3E File Offset: 0x00007F3E
		public IEnumerator<T> GetEnumerator()
		{
			return new ListSegment<T>.ListSegmentEnumerator(this);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00009D46 File Offset: 0x00007F46
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListSegment<T>.ListSegmentEnumerator(this);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00009D50 File Offset: 0x00007F50
		public int IndexOf(T item)
		{
			int num = this._offset + this._count;
			if (item == null)
			{
				for (int i = this._offset; i < num; i++)
				{
					if (this._list[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = this._offset; j < num; j++)
				{
					object obj = this._list[j];
					if (obj != null && obj.Equals(item))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00009DD2 File Offset: 0x00007FD2
		public void Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00009DD9 File Offset: 0x00007FD9
		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00009DE0 File Offset: 0x00007FE0
		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00009DE7 File Offset: 0x00007FE7
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00009DEE File Offset: 0x00007FEE
		public bool Contains(T item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00009DFD File Offset: 0x00007FFD
		public void CopyTo(T[] array, int arrayIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00009E04 File Offset: 0x00008004
		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040000C7 RID: 199
		private readonly IList<T> _list;

		// Token: 0x040000C8 RID: 200
		private readonly int _offset;

		// Token: 0x040000C9 RID: 201
		private readonly int _count;

		// Token: 0x020000BE RID: 190
		[Serializable]
		private sealed class ListSegmentEnumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060005D8 RID: 1496 RVA: 0x0000F44C File Offset: 0x0000D64C
			internal ListSegmentEnumerator(ListSegment<T> listSegment)
			{
				this._list = listSegment._list;
				this._start = listSegment._offset;
				this._end = this._start + listSegment._count;
				this._current = this._start - 1;
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000F498 File Offset: 0x0000D698
			public T Current
			{
				get
				{
					if (this._current < this._start)
					{
						throw new InvalidOperationException();
					}
					if (this._current >= this._end)
					{
						throw new InvalidOperationException();
					}
					return this._list[this._current];
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000F4D3 File Offset: 0x0000D6D3
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060005DB RID: 1499 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
			public bool MoveNext()
			{
				if (this._current >= this._end)
				{
					return false;
				}
				this._current++;
				return this._current < this._end;
			}

			// Token: 0x060005DC RID: 1500 RVA: 0x0000F50E File Offset: 0x0000D70E
			void IEnumerator.Reset()
			{
				this._current = this._start - 1;
			}

			// Token: 0x060005DD RID: 1501 RVA: 0x0000F51E File Offset: 0x0000D71E
			public void Dispose()
			{
			}

			// Token: 0x040001F1 RID: 497
			private readonly IList<T> _list;

			// Token: 0x040001F2 RID: 498
			private readonly int _start;

			// Token: 0x040001F3 RID: 499
			private readonly int _end;

			// Token: 0x040001F4 RID: 500
			private int _current;
		}
	}
}
