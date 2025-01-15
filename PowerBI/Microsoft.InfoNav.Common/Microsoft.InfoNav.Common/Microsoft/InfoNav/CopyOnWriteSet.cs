using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.InfoNav.Common;

namespace Microsoft.InfoNav
{
	// Token: 0x0200000E RID: 14
	[ImmutableObject(false)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(EnumerableDebugView<>))]
	internal sealed class CopyOnWriteSet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00002D94 File Offset: 0x00000F94
		internal CopyOnWriteSet(ISet<T> set)
		{
			this._set = CopyOnWriteSet<T>.Unwrap(set);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00002DAB File Offset: 0x00000FAB
		public int Count
		{
			get
			{
				return this._set.Count;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00002DB8 File Offset: 0x00000FB8
		internal ISet<T> InnerSet
		{
			get
			{
				return this._set;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public bool Contains(T item)
		{
			return this._set.Contains(item);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002DCE File Offset: 0x00000FCE
		public void CopyTo(T[] array, int arrayIndex)
		{
			this._set.CopyTo(array, arrayIndex);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002DDD File Offset: 0x00000FDD
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return this._set.IsProperSubsetOf(other);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002DEB File Offset: 0x00000FEB
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return this._set.IsProperSupersetOf(other);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002DF9 File Offset: 0x00000FF9
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return this._set.IsSubsetOf(other);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002E07 File Offset: 0x00001007
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return this._set.IsSupersetOf(other);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002E15 File Offset: 0x00001015
		public bool Overlaps(IEnumerable<T> other)
		{
			return this._set.Overlaps(other);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00002E23 File Offset: 0x00001023
		public bool SetEquals(IEnumerable<T> other)
		{
			return this._set.SetEquals(other);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002E31 File Offset: 0x00001031
		public void ExceptWith(IEnumerable<T> other)
		{
			this.EnsureCopied();
			this._set.ExceptWith(other);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00002E45 File Offset: 0x00001045
		public void IntersectWith(IEnumerable<T> other)
		{
			this.EnsureCopied();
			this._set.IntersectWith(other);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00002E59 File Offset: 0x00001059
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			this.EnsureCopied();
			this._set.SymmetricExceptWith(other);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00002E6D File Offset: 0x0000106D
		public void UnionWith(IEnumerable<T> other)
		{
			this.EnsureCopied();
			this._set.UnionWith(other);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00002E81 File Offset: 0x00001081
		public bool Remove(T item)
		{
			if (this._copied)
			{
				return this._set.Remove(item);
			}
			if (!this._set.Contains(item))
			{
				return false;
			}
			this.EnsureCopied();
			return this._set.Remove(item);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00002EBA File Offset: 0x000010BA
		void ICollection<T>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00002EC4 File Offset: 0x000010C4
		public bool Add(T item)
		{
			if (this._copied)
			{
				return this._set.Add(item);
			}
			if (this._set.Contains(item))
			{
				return false;
			}
			this.EnsureCopied();
			return this._set.Add(item);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00002EFD File Offset: 0x000010FD
		public void Clear()
		{
			if (this._set.Count == 0)
			{
				return;
			}
			if (this._copied)
			{
				this._set.Clear();
				return;
			}
			this._set = new HashSet<T>(this.GetComparer());
			this._copied = true;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00002F39 File Offset: 0x00001139
		public IEnumerator<T> GetEnumerator()
		{
			return this._set.GetEnumerator();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00002F46 File Offset: 0x00001146
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._set.GetEnumerator();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00002F53 File Offset: 0x00001153
		private void EnsureCopied()
		{
			if (!this._copied)
			{
				this._set = new HashSet<T>(this._set, this.GetComparer());
				this._copied = true;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00002F7C File Offset: 0x0000117C
		private IEqualityComparer<T> GetComparer()
		{
			HashSet<T> hashSet = this._set as HashSet<T>;
			if (hashSet != null)
			{
				return hashSet.Comparer;
			}
			ReadOnlySetSingleValue<T> readOnlySetSingleValue = this._set as ReadOnlySetSingleValue<T>;
			if (readOnlySetSingleValue != null)
			{
				return readOnlySetSingleValue.Comparer;
			}
			return EqualityComparer<T>.Default;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00002FBC File Offset: 0x000011BC
		private static ISet<T> Unwrap(ISet<T> set)
		{
			CopyOnWriteSet<T> copyOnWriteSet = set as CopyOnWriteSet<T>;
			if (copyOnWriteSet != null)
			{
				set = copyOnWriteSet._set;
			}
			return set;
		}

		// Token: 0x0400003A RID: 58
		private ISet<T> _set;

		// Token: 0x0400003B RID: 59
		private bool _copied;
	}
}
