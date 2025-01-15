using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000010 RID: 16
	[ImmutableObject(true)]
	public sealed class DisposableList<T> : IDisposable, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable where T : IDisposable
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00003003 File Offset: 0x00001203
		public DisposableList(IList<T> list)
		{
			this._list = list;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003012 File Offset: 0x00001212
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000301F File Offset: 0x0000121F
		public bool IsReadOnly
		{
			get
			{
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x17000013 RID: 19
		public T this[int index]
		{
			get
			{
				return this._list[index];
			}
			set
			{
				this._list[index] = value;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000304C File Offset: 0x0000124C
		public void Dispose()
		{
			List<Exception> list = null;
			for (int i = 0; i < this._list.Count; i++)
			{
				if (this._list[i] != null)
				{
					try
					{
						T t = this._list[i];
						t.Dispose();
					}
					catch (Exception ex) when (!ex.IsStoppingException())
					{
						if (list == null)
						{
							list = new List<Exception>();
						}
						list.Add(ex);
					}
				}
			}
			this._list = Util.EmptyReadOnlyCollection<T>();
			if (list != null)
			{
				throw new AggregateException(list);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000030F4 File Offset: 0x000012F4
		public IEnumerator<T> GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003101 File Offset: 0x00001301
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000310E File Offset: 0x0000130E
		public int IndexOf(T item)
		{
			return this._list.IndexOf(item);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000311C File Offset: 0x0000131C
		public void Insert(int index, T item)
		{
			this._list.Insert(index, item);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000312B File Offset: 0x0000132B
		public bool Contains(T item)
		{
			return this._list.Contains(item);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003139 File Offset: 0x00001339
		public void Add(T item)
		{
			this._list.Add(item);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003147 File Offset: 0x00001347
		public bool Remove(T item)
		{
			return this._list.Remove(item);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003155 File Offset: 0x00001355
		public void RemoveAt(int index)
		{
			this._list.RemoveAt(index);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003163 File Offset: 0x00001363
		public void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003170 File Offset: 0x00001370
		public void CopyTo(T[] array, int arrayIndex)
		{
			this._list.CopyTo(array, arrayIndex);
		}

		// Token: 0x0400003C RID: 60
		internal static readonly DisposableList<T> Empty = new DisposableList<T>(Util.EmptyReadOnlyCollection<T>());

		// Token: 0x0400003D RID: 61
		private IList<T> _list;
	}
}
