using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200009A RID: 154
	internal class MetadataCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable where T : MetadataItem
	{
		// Token: 0x06000ABF RID: 2751 RVA: 0x00019962 File Offset: 0x00017B62
		internal MetadataCollection()
			: this(null)
		{
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0001996C File Offset: 0x00017B6C
		internal MetadataCollection(IEnumerable<T> items)
		{
			this._collectionData = new MetadataCollection<T>.CollectionData();
			if (items != null)
			{
				foreach (T t in items)
				{
					if (t == null)
					{
						throw EntityUtil.CollectionParameterElementIsNull("items");
					}
					this.AddInternal(t);
				}
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x000199DC File Offset: 0x00017BDC
		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x000199E4 File Offset: 0x00017BE4
		public virtual ReadOnlyCollection<T> AsReadOnly
		{
			get
			{
				return this._collectionData.OrderedList.AsReadOnly();
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000199F6 File Offset: 0x00017BF6
		public virtual ReadOnlyMetadataCollection<T> AsReadOnlyMetadataCollection()
		{
			return new ReadOnlyMetadataCollection<T>(this);
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x000199FE File Offset: 0x00017BFE
		public virtual int Count
		{
			get
			{
				return this._collectionData.OrderedList.Count;
			}
		}

		// Token: 0x170003E0 RID: 992
		public virtual T this[int index]
		{
			get
			{
				return this._collectionData.OrderedList[index];
			}
			set
			{
				throw EntityUtil.OperationOnReadOnlyCollection();
			}
		}

		// Token: 0x170003E1 RID: 993
		public virtual T this[string identity]
		{
			get
			{
				return this.GetValue(identity, false);
			}
			set
			{
				throw EntityUtil.OperationOnReadOnlyCollection();
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00019A3C File Offset: 0x00017C3C
		public virtual T GetValue(string identity, bool ignoreCase)
		{
			T t = this.InternalTryGetValue(identity, ignoreCase);
			if (t == null)
			{
				throw EntityUtil.ItemInvalidIdentity(identity, "identity");
			}
			return t;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00019A67 File Offset: 0x00017C67
		public virtual void Add(T item)
		{
			this.AddInternal(item);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00019A70 File Offset: 0x00017C70
		private static int AddToDictionary(MetadataCollection<T>.CollectionData collectionData, string identity, int index, bool updateIfFound)
		{
			int[] array = null;
			int num = index;
			MetadataCollection<T>.OrderedIndex orderedIndex;
			if (collectionData.IdentityDictionary.TryGetValue(identity, out orderedIndex))
			{
				if (MetadataCollection<T>.EqualIdentity(collectionData.OrderedList, orderedIndex.ExactIndex, identity))
				{
					if (updateIfFound)
					{
						return orderedIndex.ExactIndex;
					}
					throw EntityUtil.ItemDuplicateIdentity(identity, "item", null);
				}
				else
				{
					if (orderedIndex.InexactIndexes != null)
					{
						int i = 0;
						while (i < orderedIndex.InexactIndexes.Length)
						{
							if (MetadataCollection<T>.EqualIdentity(collectionData.OrderedList, orderedIndex.InexactIndexes[i], identity))
							{
								if (updateIfFound)
								{
									return orderedIndex.InexactIndexes[i];
								}
								throw EntityUtil.ItemDuplicateIdentity(identity, "item", null);
							}
							else
							{
								i++;
							}
						}
						array = new int[orderedIndex.InexactIndexes.Length + 1];
						orderedIndex.InexactIndexes.CopyTo(array, 0);
						array[array.Length - 1] = index;
					}
					else
					{
						array = new int[] { index };
					}
					num = orderedIndex.ExactIndex;
				}
			}
			collectionData.IdentityDictionary[identity] = new MetadataCollection<T>.OrderedIndex(num, array);
			return index;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00019B56 File Offset: 0x00017D56
		private void AddInternal(T item)
		{
			this.ThrowIfReadOnly();
			MetadataCollection<T>.AddInternalHelper(item, this._collectionData, false);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00019B6C File Offset: 0x00017D6C
		private static void AddInternalHelper(T item, MetadataCollection<T>.CollectionData collectionData, bool updateIfFound)
		{
			int count = collectionData.OrderedList.Count;
			int num;
			if (collectionData.IdentityDictionary != null)
			{
				num = MetadataCollection<T>.AddToDictionary(collectionData, item.Identity, count, updateIfFound);
			}
			else
			{
				num = MetadataCollection<T>.IndexOf(collectionData, item.Identity, false);
				if (0 <= num)
				{
					if (!updateIfFound)
					{
						throw EntityUtil.ItemDuplicateIdentity(item.Identity, "item", null);
					}
				}
				else if (25 <= count)
				{
					collectionData.IdentityDictionary = new Dictionary<string, MetadataCollection<T>.OrderedIndex>(collectionData.OrderedList.Count + 1, StringComparer.OrdinalIgnoreCase);
					for (int i = 0; i < collectionData.OrderedList.Count; i++)
					{
						MetadataCollection<T>.AddToDictionary(collectionData, collectionData.OrderedList[i].Identity, i, false);
					}
					MetadataCollection<T>.AddToDictionary(collectionData, item.Identity, count, false);
				}
			}
			if (0 <= num && num < count)
			{
				collectionData.OrderedList[num] = item;
				return;
			}
			collectionData.OrderedList.Add(item);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00019C68 File Offset: 0x00017E68
		internal bool AtomicAddRange(List<T> items)
		{
			MetadataCollection<T>.CollectionData collectionData = this._collectionData;
			MetadataCollection<T>.CollectionData collectionData2 = new MetadataCollection<T>.CollectionData(collectionData, items.Count);
			foreach (T t in items)
			{
				MetadataCollection<T>.AddInternalHelper(t, collectionData2, false);
			}
			return Interlocked.CompareExchange<MetadataCollection<T>.CollectionData>(ref this._collectionData, collectionData2, collectionData) == collectionData;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00019CDC File Offset: 0x00017EDC
		private static bool EqualIdentity(List<T> orderedList, int index, string identity)
		{
			return orderedList[index].Identity == identity;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00019CF5 File Offset: 0x00017EF5
		void IList<T>.Insert(int index, T item)
		{
			throw EntityUtil.OperationOnReadOnlyCollection();
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00019CFC File Offset: 0x00017EFC
		bool ICollection<T>.Remove(T item)
		{
			throw EntityUtil.OperationOnReadOnlyCollection();
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00019D03 File Offset: 0x00017F03
		void IList<T>.RemoveAt(int index)
		{
			throw EntityUtil.OperationOnReadOnlyCollection();
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00019D0A File Offset: 0x00017F0A
		void ICollection<T>.Clear()
		{
			throw EntityUtil.OperationOnReadOnlyCollection();
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00019D11 File Offset: 0x00017F11
		public bool Contains(T item)
		{
			return -1 != this.IndexOf(item);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00019D20 File Offset: 0x00017F20
		public virtual bool ContainsIdentity(string identity)
		{
			EntityUtil.CheckStringArgument(identity, "identity");
			return 0 <= MetadataCollection<T>.IndexOf(this._collectionData, identity, false);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00019D40 File Offset: 0x00017F40
		private static int IndexOf(MetadataCollection<T>.CollectionData collectionData, string identity, bool ignoreCase)
		{
			int num = -1;
			if (collectionData.IdentityDictionary != null)
			{
				MetadataCollection<T>.OrderedIndex orderedIndex;
				if (collectionData.IdentityDictionary.TryGetValue(identity, out orderedIndex))
				{
					if (ignoreCase)
					{
						num = orderedIndex.ExactIndex;
					}
					else if (MetadataCollection<T>.EqualIdentity(collectionData.OrderedList, orderedIndex.ExactIndex, identity))
					{
						return orderedIndex.ExactIndex;
					}
					if (orderedIndex.InexactIndexes != null)
					{
						if (ignoreCase)
						{
							throw EntityUtil.MoreThanOneItemMatchesIdentity(identity);
						}
						for (int i = 0; i < orderedIndex.InexactIndexes.Length; i++)
						{
							if (MetadataCollection<T>.EqualIdentity(collectionData.OrderedList, orderedIndex.InexactIndexes[i], identity))
							{
								return orderedIndex.InexactIndexes[i];
							}
						}
					}
				}
			}
			else if (ignoreCase)
			{
				for (int j = 0; j < collectionData.OrderedList.Count; j++)
				{
					if (string.Equals(collectionData.OrderedList[j].Identity, identity, StringComparison.OrdinalIgnoreCase))
					{
						if (0 <= num)
						{
							throw EntityUtil.MoreThanOneItemMatchesIdentity(identity);
						}
						num = j;
					}
				}
			}
			else
			{
				for (int k = 0; k < collectionData.OrderedList.Count; k++)
				{
					if (MetadataCollection<T>.EqualIdentity(collectionData.OrderedList, k, identity))
					{
						return k;
					}
				}
			}
			return num;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00019E54 File Offset: 0x00018054
		public virtual int IndexOf(T item)
		{
			int num = MetadataCollection<T>.IndexOf(this._collectionData, item.Identity, false);
			if (num != -1 && this._collectionData.OrderedList[num] == item)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00019EA0 File Offset: 0x000180A0
		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			EntityUtil.GenericCheckArgumentNull<T[]>(array, "array");
			if (arrayIndex < 0)
			{
				throw EntityUtil.ArgumentOutOfRange("arrayIndex");
			}
			if (this._collectionData.OrderedList.Count > array.Length - arrayIndex)
			{
				throw EntityUtil.ArrayTooSmall("arrayIndex");
			}
			this._collectionData.OrderedList.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00019EFC File Offset: 0x000180FC
		public ReadOnlyMetadataCollection<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlyMetadataCollection<T>.Enumerator(this);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00019F04 File Offset: 0x00018104
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00019F11 File Offset: 0x00018111
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00019F20 File Offset: 0x00018120
		public MetadataCollection<T> SetReadOnly()
		{
			for (int i = 0; i < this._collectionData.OrderedList.Count; i++)
			{
				this._collectionData.OrderedList[i].SetReadOnly();
			}
			this._collectionData.OrderedList.TrimExcess();
			this._readOnly = true;
			return this;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00019F7B File Offset: 0x0001817B
		public virtual bool TryGetValue(string identity, bool ignoreCase, out T item)
		{
			item = this.InternalTryGetValue(identity, ignoreCase);
			return item != null;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00019F9C File Offset: 0x0001819C
		private T InternalTryGetValue(string identity, bool ignoreCase)
		{
			int num = MetadataCollection<T>.IndexOf(this._collectionData, EntityUtil.GenericCheckArgumentNull<string>(identity, "identity"), ignoreCase);
			if (0 > num)
			{
				return default(T);
			}
			return this._collectionData.OrderedList[num];
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00019FE0 File Offset: 0x000181E0
		internal void ThrowIfReadOnly()
		{
			if (this.IsReadOnly)
			{
				throw EntityUtil.OperationOnReadOnlyCollection();
			}
		}

		// Token: 0x0400085A RID: 2138
		private MetadataCollection<T>.CollectionData _collectionData;

		// Token: 0x0400085B RID: 2139
		private bool _readOnly;

		// Token: 0x0400085C RID: 2140
		private const int UseSortedListCrossover = 25;

		// Token: 0x020002BD RID: 701
		private struct OrderedIndex
		{
			// Token: 0x06001C6D RID: 7277 RVA: 0x0004ED25 File Offset: 0x0004CF25
			internal OrderedIndex(int exactIndex, int[] inexactIndexes)
			{
				this.ExactIndex = exactIndex;
				this.InexactIndexes = inexactIndexes;
			}

			// Token: 0x04000FC1 RID: 4033
			internal readonly int ExactIndex;

			// Token: 0x04000FC2 RID: 4034
			internal readonly int[] InexactIndexes;
		}

		// Token: 0x020002BE RID: 702
		private class CollectionData
		{
			// Token: 0x06001C6E RID: 7278 RVA: 0x0004ED35 File Offset: 0x0004CF35
			internal CollectionData()
			{
				this.OrderedList = new List<T>();
			}

			// Token: 0x06001C6F RID: 7279 RVA: 0x0004ED48 File Offset: 0x0004CF48
			internal CollectionData(MetadataCollection<T>.CollectionData original, int additionalCapacity)
			{
				this.OrderedList = new List<T>(original.OrderedList.Count + additionalCapacity);
				foreach (T t in original.OrderedList)
				{
					this.OrderedList.Add(t);
				}
				if (25 <= this.OrderedList.Capacity)
				{
					this.IdentityDictionary = new Dictionary<string, MetadataCollection<T>.OrderedIndex>(this.OrderedList.Capacity, StringComparer.OrdinalIgnoreCase);
					if (original.IdentityDictionary != null)
					{
						using (Dictionary<string, MetadataCollection<T>.OrderedIndex>.Enumerator enumerator2 = original.IdentityDictionary.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								KeyValuePair<string, MetadataCollection<T>.OrderedIndex> keyValuePair = enumerator2.Current;
								this.IdentityDictionary.Add(keyValuePair.Key, keyValuePair.Value);
							}
							return;
						}
					}
					for (int i = 0; i < this.OrderedList.Count; i++)
					{
						MetadataCollection<T>.AddToDictionary(this, this.OrderedList[i].Identity, i, false);
					}
				}
			}

			// Token: 0x04000FC3 RID: 4035
			internal Dictionary<string, MetadataCollection<T>.OrderedIndex> IdentityDictionary;

			// Token: 0x04000FC4 RID: 4036
			internal List<T> OrderedList;
		}
	}
}
