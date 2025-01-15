using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000113 RID: 275
	internal class ReadOnlySet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600133D RID: 4925 RVA: 0x00032750 File Offset: 0x00030950
		public ReadOnlySet(ISet<T> set)
		{
			this._set = set;
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0003275F File Offset: 0x0003095F
		public bool Add(T item)
		{
			throw Error.DbPropertyValues_PropertyValueNamesAreReadonly();
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00032766 File Offset: 0x00030966
		public void ExceptWith(IEnumerable<T> other)
		{
			this._set.ExceptWith(other);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00032774 File Offset: 0x00030974
		public void IntersectWith(IEnumerable<T> other)
		{
			this._set.IntersectWith(other);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00032782 File Offset: 0x00030982
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return this._set.IsProperSubsetOf(other);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00032790 File Offset: 0x00030990
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return this._set.IsProperSupersetOf(other);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0003279E File Offset: 0x0003099E
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return this._set.IsSubsetOf(other);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000327AC File Offset: 0x000309AC
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return this._set.IsSupersetOf(other);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000327BA File Offset: 0x000309BA
		public bool Overlaps(IEnumerable<T> other)
		{
			return this._set.Overlaps(other);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x000327C8 File Offset: 0x000309C8
		public bool SetEquals(IEnumerable<T> other)
		{
			return this._set.SetEquals(other);
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x000327D6 File Offset: 0x000309D6
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			this._set.SymmetricExceptWith(other);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000327E4 File Offset: 0x000309E4
		public void UnionWith(IEnumerable<T> other)
		{
			this._set.UnionWith(other);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x000327F2 File Offset: 0x000309F2
		void ICollection<T>.Add(T item)
		{
			throw Error.DbPropertyValues_PropertyValueNamesAreReadonly();
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x000327F9 File Offset: 0x000309F9
		public void Clear()
		{
			throw Error.DbPropertyValues_PropertyValueNamesAreReadonly();
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00032800 File Offset: 0x00030A00
		public bool Contains(T item)
		{
			return this._set.Contains(item);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0003280E File Offset: 0x00030A0E
		public void CopyTo(T[] array, int arrayIndex)
		{
			this._set.CopyTo(array, arrayIndex);
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x0003281D File Offset: 0x00030A1D
		public int Count
		{
			get
			{
				return this._set.Count;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x0003282A File Offset: 0x00030A2A
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0003282D File Offset: 0x00030A2D
		public bool Remove(T item)
		{
			throw Error.DbPropertyValues_PropertyValueNamesAreReadonly();
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00032834 File Offset: 0x00030A34
		public IEnumerator<T> GetEnumerator()
		{
			return this._set.GetEnumerator();
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00032841 File Offset: 0x00030A41
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._set.GetEnumerator();
		}

		// Token: 0x0400094F RID: 2383
		private readonly ISet<T> _set;
	}
}
