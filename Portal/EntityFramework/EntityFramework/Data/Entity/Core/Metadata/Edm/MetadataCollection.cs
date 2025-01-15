using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Runtime.CompilerServices;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D8 RID: 1240
	internal class MetadataCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable where T : MetadataItem
	{
		// Token: 0x06003D6C RID: 15724 RVA: 0x000CB603 File Offset: 0x000C9803
		internal MetadataCollection()
		{
			this._metadataList = new List<T>();
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x000CB618 File Offset: 0x000C9818
		internal MetadataCollection(IEnumerable<T> items)
		{
			this._metadataList = new List<T>();
			if (items != null)
			{
				foreach (T t in items)
				{
					if (t == null)
					{
						throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("items"));
					}
					this.AddInternal(t);
				}
			}
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x000CB68C File Offset: 0x000C988C
		private MetadataCollection(List<T> items)
		{
			this._metadataList = items;
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x000CB69B File Offset: 0x000C989B
		internal static MetadataCollection<T> Wrap(List<T> items)
		{
			return new MetadataCollection<T>(items);
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06003D70 RID: 15728 RVA: 0x000CB6A3 File Offset: 0x000C98A3
		public virtual int Count
		{
			get
			{
				return this._metadataList.Count;
			}
		}

		// Token: 0x17000C10 RID: 3088
		public virtual T this[int index]
		{
			get
			{
				return this._metadataList[index];
			}
			set
			{
				this.ThrowIfReadOnly();
				string identity = this._metadataList[index].Identity;
				this._metadataList[index] = value;
				this.HandleIdentityChange(value, identity, false);
			}
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x000CB700 File Offset: 0x000C9900
		internal void HandleIdentityChange(T item, string initialIdentity)
		{
			this.HandleIdentityChange(item, initialIdentity, true);
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x000CB70C File Offset: 0x000C990C
		private void HandleIdentityChange(T item, string initialIdentity, bool validate)
		{
			T t;
			if (this._caseSensitiveDictionary != null && (!validate || (this._caseSensitiveDictionary.TryGetValue(initialIdentity, out t) && t == item)))
			{
				this.RemoveFromCaseSensitiveDictionary(initialIdentity);
				string identity = item.Identity;
				if (this._caseSensitiveDictionary.ContainsKey(identity))
				{
					this._caseSensitiveDictionary = null;
				}
				else
				{
					this._caseSensitiveDictionary.Add(identity, item);
				}
			}
			this._caseInsensitiveDictionary = null;
		}

		// Token: 0x17000C11 RID: 3089
		public virtual T this[string identity]
		{
			get
			{
				return this.GetValue(identity, false);
			}
			set
			{
				throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
			}
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x000CB7A4 File Offset: 0x000C99A4
		public virtual T GetValue(string identity, bool ignoreCase)
		{
			T t;
			if (!this.TryGetValue(identity, ignoreCase, out t))
			{
				throw new ArgumentException(Strings.ItemInvalidIdentity(identity), "identity");
			}
			return t;
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x000CB7CF File Offset: 0x000C99CF
		public virtual bool TryGetValue(string identity, bool ignoreCase, out T item)
		{
			if (!ignoreCase)
			{
				return this.FindCaseSensitive(identity, out item);
			}
			return this.FindCaseInsensitive(identity, out item, false);
		}

		// Token: 0x06003D79 RID: 15737 RVA: 0x000CB7E6 File Offset: 0x000C99E6
		public virtual void Add(T item)
		{
			this.ThrowIfReadOnly();
			this.AddInternal(item);
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x000CB7F8 File Offset: 0x000C99F8
		private void AddInternal(T item)
		{
			string identity = item.Identity;
			if (this.ContainsIdentityCaseSensitive(identity))
			{
				throw new ArgumentException(Strings.ItemDuplicateIdentity(identity), "item");
			}
			this._metadataList.Add(item);
			if (this._caseSensitiveDictionary != null)
			{
				this._caseSensitiveDictionary.Add(identity, item);
			}
			this._caseInsensitiveDictionary = null;
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x000CB85C File Offset: 0x000C9A5C
		internal void AddRange(IEnumerable<T> items)
		{
			Check.NotNull<IEnumerable<T>>(items, "items");
			foreach (T t in items)
			{
				if (t == null)
				{
					throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("items"));
				}
				this.AddInternal(t);
			}
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x000CB8C8 File Offset: 0x000C9AC8
		internal bool Remove(T item)
		{
			this.ThrowIfReadOnly();
			if (!this._metadataList.Remove(item))
			{
				return false;
			}
			if (this._caseSensitiveDictionary != null)
			{
				this.RemoveFromCaseSensitiveDictionary(item.Identity);
			}
			this._caseInsensitiveDictionary = null;
			return true;
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06003D7D RID: 15741 RVA: 0x000CB905 File Offset: 0x000C9B05
		public virtual ReadOnlyCollection<T> AsReadOnly
		{
			get
			{
				return new ReadOnlyCollection<T>(this._metadataList);
			}
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x000CB912 File Offset: 0x000C9B12
		public virtual ReadOnlyMetadataCollection<T> AsReadOnlyMetadataCollection()
		{
			return new ReadOnlyMetadataCollection<T>(this);
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06003D7F RID: 15743 RVA: 0x000CB91A File Offset: 0x000C9B1A
		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x000CB922 File Offset: 0x000C9B22
		internal void ResetReadOnly()
		{
			this._readOnly = false;
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x000CB92C File Offset: 0x000C9B2C
		public MetadataCollection<T> SetReadOnly()
		{
			for (int i = 0; i < this._metadataList.Count; i++)
			{
				this._metadataList[i].SetReadOnly();
			}
			this._readOnly = true;
			this._metadataList.TrimExcess();
			if (this._metadataList.Count <= 8)
			{
				this._caseSensitiveDictionary = null;
				this._caseInsensitiveDictionary = null;
			}
			return this;
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x000CB998 File Offset: 0x000C9B98
		void IList<T>.Insert(int index, T item)
		{
			throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x000CB9A4 File Offset: 0x000C9BA4
		bool ICollection<T>.Remove(T item)
		{
			throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
		}

		// Token: 0x06003D84 RID: 15748 RVA: 0x000CB9B0 File Offset: 0x000C9BB0
		void IList<T>.RemoveAt(int index)
		{
			throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
		}

		// Token: 0x06003D85 RID: 15749 RVA: 0x000CB9BC File Offset: 0x000C9BBC
		void ICollection<T>.Clear()
		{
			throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x000CB9C8 File Offset: 0x000C9BC8
		public bool Contains(T item)
		{
			T t;
			return this.TryGetValue(item.Identity, false, out t) && t == item;
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x000CB9FB File Offset: 0x000C9BFB
		public virtual bool ContainsIdentity(string identity)
		{
			return this.ContainsIdentityCaseSensitive(identity);
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x000CBA04 File Offset: 0x000C9C04
		public virtual int IndexOf(T item)
		{
			return this._metadataList.IndexOf(item);
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x000CBA12 File Offset: 0x000C9C12
		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			this._metadataList.CopyTo(array, arrayIndex);
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x000CBA21 File Offset: 0x000C9C21
		public ReadOnlyMetadataCollection<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlyMetadataCollection<T>.Enumerator(this);
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x000CBA29 File Offset: 0x000C9C29
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003D8C RID: 15756 RVA: 0x000CBA36 File Offset: 0x000C9C36
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003D8D RID: 15757 RVA: 0x000CBA43 File Offset: 0x000C9C43
		internal void InvalidateCache()
		{
			this._caseSensitiveDictionary = null;
			this._caseInsensitiveDictionary = null;
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06003D8E RID: 15758 RVA: 0x000CBA57 File Offset: 0x000C9C57
		internal bool HasCaseSensitiveDictionary
		{
			get
			{
				return this._caseSensitiveDictionary != null;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06003D8F RID: 15759 RVA: 0x000CBA64 File Offset: 0x000C9C64
		internal bool HasCaseInsensitiveDictionary
		{
			get
			{
				return this._caseInsensitiveDictionary != null;
			}
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x000CBA71 File Offset: 0x000C9C71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Dictionary<string, T> GetCaseSensitiveDictionary()
		{
			if (this._caseSensitiveDictionary == null && this._metadataList.Count > 8)
			{
				this._caseSensitiveDictionary = this.CreateCaseSensitiveDictionary();
			}
			return this._caseSensitiveDictionary;
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x000CBAA4 File Offset: 0x000C9CA4
		private Dictionary<string, T> CreateCaseSensitiveDictionary()
		{
			Dictionary<string, T> dictionary = new Dictionary<string, T>(this._metadataList.Count, StringComparer.Ordinal);
			for (int i = 0; i < this._metadataList.Count; i++)
			{
				T t = this._metadataList[i];
				dictionary.Add(t.Identity, t);
			}
			return dictionary;
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x000CBAFD File Offset: 0x000C9CFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Dictionary<string, int> GetCaseInsensitiveDictionary()
		{
			if (this._caseInsensitiveDictionary == null && this._metadataList.Count > 8)
			{
				this._caseInsensitiveDictionary = this.CreateCaseInsensitiveDictionary();
			}
			return this._caseInsensitiveDictionary;
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x000CBB30 File Offset: 0x000C9D30
		private Dictionary<string, int> CreateCaseInsensitiveDictionary()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(this._metadataList.Count, StringComparer.OrdinalIgnoreCase) { 
			{
				this._metadataList[0].Identity,
				0
			} };
			for (int i = 1; i < this._metadataList.Count; i++)
			{
				string identity = this._metadataList[i].Identity;
				int num;
				if (!dictionary.TryGetValue(identity, out num))
				{
					dictionary[identity] = i;
				}
				else if (num >= 0)
				{
					dictionary[identity] = -1;
				}
			}
			return dictionary;
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x000CBBC0 File Offset: 0x000C9DC0
		private bool ContainsIdentityCaseSensitive(string identity)
		{
			Dictionary<string, T> caseSensitiveDictionary = this.GetCaseSensitiveDictionary();
			if (caseSensitiveDictionary != null)
			{
				return caseSensitiveDictionary.ContainsKey(identity);
			}
			return this.ListContainsIdentityCaseSensitive(identity);
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x000CBBE8 File Offset: 0x000C9DE8
		private bool ListContainsIdentityCaseSensitive(string identity)
		{
			for (int i = 0; i < this._metadataList.Count; i++)
			{
				if (this._metadataList[i].Identity.Equals(identity, StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x000CBC30 File Offset: 0x000C9E30
		private bool FindCaseSensitive(string identity, out T item)
		{
			Dictionary<string, T> caseSensitiveDictionary = this.GetCaseSensitiveDictionary();
			if (caseSensitiveDictionary != null)
			{
				return caseSensitiveDictionary.TryGetValue(identity, out item);
			}
			return this.ListFindCaseSensitive(identity, out item);
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x000CBC60 File Offset: 0x000C9E60
		private bool ListFindCaseSensitive(string identity, out T item)
		{
			for (int i = 0; i < this._metadataList.Count; i++)
			{
				T t = this._metadataList[i];
				if (t.Identity.Equals(identity, StringComparison.Ordinal))
				{
					item = t;
					return true;
				}
			}
			item = default(T);
			return false;
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x000CBCB8 File Offset: 0x000C9EB8
		private bool FindCaseInsensitive(string identity, out T item, bool throwOnMultipleMatches)
		{
			Dictionary<string, int> caseInsensitiveDictionary = this.GetCaseInsensitiveDictionary();
			if (caseInsensitiveDictionary != null)
			{
				int num;
				if (caseInsensitiveDictionary.TryGetValue(identity, out num))
				{
					if (num >= 0)
					{
						item = this._metadataList[num];
						return true;
					}
					if (throwOnMultipleMatches)
					{
						throw new InvalidOperationException(Strings.MoreThanOneItemMatchesIdentity(identity));
					}
				}
				item = default(T);
				return false;
			}
			return this.ListFindCaseInsensitive(identity, out item, throwOnMultipleMatches);
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x000CBD14 File Offset: 0x000C9F14
		private bool ListFindCaseInsensitive(string identity, out T item, bool throwOnMultipleMatches)
		{
			bool flag = false;
			item = default(T);
			for (int i = 0; i < this._metadataList.Count; i++)
			{
				T t = this._metadataList[i];
				if (t.Identity.Equals(identity, StringComparison.OrdinalIgnoreCase))
				{
					if (flag)
					{
						if (throwOnMultipleMatches)
						{
							throw new InvalidOperationException(Strings.MoreThanOneItemMatchesIdentity(identity));
						}
						item = default(T);
						return false;
					}
					else
					{
						flag = true;
						item = t;
					}
				}
			}
			return flag;
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x000CBD86 File Offset: 0x000C9F86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void RemoveFromCaseSensitiveDictionary(string identity)
		{
			this._caseSensitiveDictionary.Remove(identity);
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x000CBD97 File Offset: 0x000C9F97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ThrowIfReadOnly()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
			}
		}

		// Token: 0x040014FA RID: 5370
		internal const int UseDictionaryCrossover = 8;

		// Token: 0x040014FB RID: 5371
		private bool _readOnly;

		// Token: 0x040014FC RID: 5372
		private List<T> _metadataList;

		// Token: 0x040014FD RID: 5373
		private volatile Dictionary<string, T> _caseSensitiveDictionary;

		// Token: 0x040014FE RID: 5374
		private volatile Dictionary<string, int> _caseInsensitiveDictionary;
	}
}
