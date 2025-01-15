using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.InfoNav.Common;

namespace Microsoft.InfoNav
{
	// Token: 0x0200000D RID: 13
	[ImmutableObject(false)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(EnumerableDebugView<>))]
	internal sealed class CopyOnWriteList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00002C29 File Offset: 0x00000E29
		internal CopyOnWriteList(IList<T> list)
		{
			this._list = CopyOnWriteList<T>.Unwrap(list);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002C3D File Offset: 0x00000E3D
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002C4A File Offset: 0x00000E4A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000D RID: 13
		public T this[int index]
		{
			get
			{
				return this._list[index];
			}
			set
			{
				this.EnsureCopied();
				this._list[index] = value;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002C70 File Offset: 0x00000E70
		public int IndexOf(T item)
		{
			return this._list.IndexOf(item);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002C7E File Offset: 0x00000E7E
		public bool Contains(T item)
		{
			return this._list.Contains(item);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002C8C File Offset: 0x00000E8C
		public void CopyTo(T[] array, int arrayIndex)
		{
			this._list.CopyTo(array, arrayIndex);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002C9B File Offset: 0x00000E9B
		public IEnumerator<T> GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002CA8 File Offset: 0x00000EA8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002CB5 File Offset: 0x00000EB5
		public void Insert(int index, T item)
		{
			this.EnsureCopied();
			this._list.Insert(index, item);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002CCA File Offset: 0x00000ECA
		public void RemoveAt(int index)
		{
			this.EnsureCopied();
			this._list.RemoveAt(index);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002CDE File Offset: 0x00000EDE
		public bool Remove(T item)
		{
			this.EnsureCopied();
			return this._list.Remove(item);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002CF2 File Offset: 0x00000EF2
		public void Add(T item)
		{
			this.EnsureCopied();
			this._list.Add(item);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002D06 File Offset: 0x00000F06
		public void Clear()
		{
			if (this._copied)
			{
				this._list.Clear();
				return;
			}
			this._list = new List<T>();
			this._copied = true;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002D30 File Offset: 0x00000F30
		private void EnsureCopied()
		{
			if (!this._copied)
			{
				List<T> list = new List<T>(this._list.Count + 4);
				list.AddRange(this._list);
				this._list = list;
				this._copied = true;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00002D74 File Offset: 0x00000F74
		private static IList<T> Unwrap(IList<T> list)
		{
			CopyOnWriteList<T> copyOnWriteList = list as CopyOnWriteList<T>;
			if (copyOnWriteList != null)
			{
				list = copyOnWriteList._list;
			}
			return list;
		}

		// Token: 0x04000038 RID: 56
		private IList<T> _list;

		// Token: 0x04000039 RID: 57
		private bool _copied;
	}
}
