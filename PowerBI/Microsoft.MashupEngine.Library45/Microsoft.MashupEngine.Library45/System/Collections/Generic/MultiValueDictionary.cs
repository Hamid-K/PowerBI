using System;
using Properties;

namespace System.Collections.Generic
{
	// Token: 0x02000069 RID: 105
	public class MultiValueDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, IReadOnlyCollection<TValue>>, IReadOnlyCollection<KeyValuePair<TKey, IReadOnlyCollection<TValue>>>, IEnumerable<KeyValuePair<TKey, IReadOnlyCollection<TValue>>>, IEnumerable
	{
		// Token: 0x0600024B RID: 587 RVA: 0x0000997B File Offset: 0x00007B7B
		public MultiValueDictionary()
		{
			this.dictionary = new Dictionary<TKey, MultiValueDictionary<TKey, TValue>.InnerCollectionView>();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000099B4 File Offset: 0x00007BB4
		public MultiValueDictionary(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Resources.ArgumentOutOfRange_NeedNonNegNum);
			}
			this.dictionary = new Dictionary<TKey, MultiValueDictionary<TKey, TValue>.InnerCollectionView>(capacity);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009A0C File Offset: 0x00007C0C
		public MultiValueDictionary(IEqualityComparer<TKey> comparer)
		{
			this.dictionary = new Dictionary<TKey, MultiValueDictionary<TKey, TValue>.InnerCollectionView>(comparer);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009A48 File Offset: 0x00007C48
		public MultiValueDictionary(int capacity, IEqualityComparer<TKey> comparer)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Resources.ArgumentOutOfRange_NeedNonNegNum);
			}
			this.dictionary = new Dictionary<TKey, MultiValueDictionary<TKey, TValue>.InnerCollectionView>(capacity, comparer);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009AA1 File Offset: 0x00007CA1
		public MultiValueDictionary(IEnumerable<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> enumerable)
			: this(enumerable, null)
		{
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009AAC File Offset: 0x00007CAC
		public MultiValueDictionary(IEnumerable<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> enumerable, IEqualityComparer<TKey> comparer)
		{
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			this.dictionary = new Dictionary<TKey, MultiValueDictionary<TKey, TValue>.InnerCollectionView>(comparer);
			foreach (KeyValuePair<TKey, IReadOnlyCollection<TValue>> keyValuePair in enumerable)
			{
				this.AddRange(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009B48 File Offset: 0x00007D48
		public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>() where TValueCollection : ICollection<TValue>, new()
		{
			TValueCollection tvalueCollection = new TValueCollection();
			if (tvalueCollection.IsReadOnly)
			{
				throw new InvalidOperationException(Resources.Create_TValueCollectionReadOnly);
			}
			MultiValueDictionary<TKey, TValue> multiValueDictionary = new MultiValueDictionary<TKey, TValue>();
			multiValueDictionary.NewCollectionFactory = () => new TValueCollection();
			return multiValueDictionary;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009BA0 File Offset: 0x00007DA0
		public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(int capacity) where TValueCollection : ICollection<TValue>, new()
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Resources.ArgumentOutOfRange_NeedNonNegNum);
			}
			TValueCollection tvalueCollection = new TValueCollection();
			if (tvalueCollection.IsReadOnly)
			{
				throw new InvalidOperationException(Resources.Create_TValueCollectionReadOnly);
			}
			MultiValueDictionary<TKey, TValue> multiValueDictionary = new MultiValueDictionary<TKey, TValue>(capacity);
			multiValueDictionary.NewCollectionFactory = () => new TValueCollection();
			return multiValueDictionary;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00009C0C File Offset: 0x00007E0C
		public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(IEqualityComparer<TKey> comparer) where TValueCollection : ICollection<TValue>, new()
		{
			TValueCollection tvalueCollection = new TValueCollection();
			if (tvalueCollection.IsReadOnly)
			{
				throw new InvalidOperationException(Resources.Create_TValueCollectionReadOnly);
			}
			MultiValueDictionary<TKey, TValue> multiValueDictionary = new MultiValueDictionary<TKey, TValue>(comparer);
			multiValueDictionary.NewCollectionFactory = () => new TValueCollection();
			return multiValueDictionary;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009C64 File Offset: 0x00007E64
		public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(int capacity, IEqualityComparer<TKey> comparer) where TValueCollection : ICollection<TValue>, new()
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Resources.ArgumentOutOfRange_NeedNonNegNum);
			}
			TValueCollection tvalueCollection = new TValueCollection();
			if (tvalueCollection.IsReadOnly)
			{
				throw new InvalidOperationException(Resources.Create_TValueCollectionReadOnly);
			}
			MultiValueDictionary<TKey, TValue> multiValueDictionary = new MultiValueDictionary<TKey, TValue>(capacity, comparer);
			multiValueDictionary.NewCollectionFactory = () => new TValueCollection();
			return multiValueDictionary;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(IEnumerable<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> enumerable) where TValueCollection : ICollection<TValue>, new()
		{
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			TValueCollection tvalueCollection = new TValueCollection();
			if (tvalueCollection.IsReadOnly)
			{
				throw new InvalidOperationException(Resources.Create_TValueCollectionReadOnly);
			}
			MultiValueDictionary<TKey, TValue> multiValueDictionary = new MultiValueDictionary<TKey, TValue>();
			multiValueDictionary.NewCollectionFactory = () => new TValueCollection();
			foreach (KeyValuePair<TKey, IReadOnlyCollection<TValue>> keyValuePair in enumerable)
			{
				multiValueDictionary.AddRange(keyValuePair.Key, keyValuePair.Value);
			}
			return multiValueDictionary;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009D84 File Offset: 0x00007F84
		public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(IEnumerable<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> enumerable, IEqualityComparer<TKey> comparer) where TValueCollection : ICollection<TValue>, new()
		{
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			TValueCollection tvalueCollection = new TValueCollection();
			if (tvalueCollection.IsReadOnly)
			{
				throw new InvalidOperationException(Resources.Create_TValueCollectionReadOnly);
			}
			MultiValueDictionary<TKey, TValue> multiValueDictionary = new MultiValueDictionary<TKey, TValue>(comparer);
			multiValueDictionary.NewCollectionFactory = () => new TValueCollection();
			foreach (KeyValuePair<TKey, IReadOnlyCollection<TValue>> keyValuePair in enumerable)
			{
				multiValueDictionary.AddRange(keyValuePair.Key, keyValuePair.Value);
			}
			return multiValueDictionary;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009E34 File Offset: 0x00008034
		public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(Func<TValueCollection> collectionFactory) where TValueCollection : ICollection<TValue>
		{
			TValueCollection tvalueCollection = collectionFactory();
			if (tvalueCollection.IsReadOnly)
			{
				throw new InvalidOperationException(Resources.Create_TValueCollectionReadOnly);
			}
			return new MultiValueDictionary<TKey, TValue>
			{
				NewCollectionFactory = (Func<ICollection<TValue>>)collectionFactory
			};
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009E74 File Offset: 0x00008074
		public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(int capacity, Func<TValueCollection> collectionFactory) where TValueCollection : ICollection<TValue>
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Resources.ArgumentOutOfRange_NeedNonNegNum);
			}
			TValueCollection tvalueCollection = collectionFactory();
			if (tvalueCollection.IsReadOnly)
			{
				throw new InvalidOperationException(Resources.Create_TValueCollectionReadOnly);
			}
			return new MultiValueDictionary<TKey, TValue>(capacity)
			{
				NewCollectionFactory = (Func<ICollection<TValue>>)collectionFactory
			};
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009EC8 File Offset: 0x000080C8
		public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(IEqualityComparer<TKey> comparer, Func<TValueCollection> collectionFactory) where TValueCollection : ICollection<TValue>
		{
			TValueCollection tvalueCollection = collectionFactory();
			if (tvalueCollection.IsReadOnly)
			{
				throw new InvalidOperationException(Resources.Create_TValueCollectionReadOnly);
			}
			return new MultiValueDictionary<TKey, TValue>(comparer)
			{
				NewCollectionFactory = (Func<ICollection<TValue>>)collectionFactory
			};
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009F08 File Offset: 0x00008108
		public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(int capacity, IEqualityComparer<TKey> comparer, Func<TValueCollection> collectionFactory) where TValueCollection : ICollection<TValue>
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Resources.ArgumentOutOfRange_NeedNonNegNum);
			}
			TValueCollection tvalueCollection = collectionFactory();
			if (tvalueCollection.IsReadOnly)
			{
				throw new InvalidOperationException(Resources.Create_TValueCollectionReadOnly);
			}
			return new MultiValueDictionary<TKey, TValue>(capacity, comparer)
			{
				NewCollectionFactory = (Func<ICollection<TValue>>)collectionFactory
			};
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009F60 File Offset: 0x00008160
		public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(IEnumerable<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> enumerable, Func<TValueCollection> collectionFactory) where TValueCollection : ICollection<TValue>
		{
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			TValueCollection tvalueCollection = collectionFactory();
			if (tvalueCollection.IsReadOnly)
			{
				throw new InvalidOperationException(Resources.Create_TValueCollectionReadOnly);
			}
			MultiValueDictionary<TKey, TValue> multiValueDictionary = new MultiValueDictionary<TKey, TValue>();
			multiValueDictionary.NewCollectionFactory = (Func<ICollection<TValue>>)collectionFactory;
			foreach (KeyValuePair<TKey, IReadOnlyCollection<TValue>> keyValuePair in enumerable)
			{
				multiValueDictionary.AddRange(keyValuePair.Key, keyValuePair.Value);
			}
			return multiValueDictionary;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009FF8 File Offset: 0x000081F8
		public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(IEnumerable<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> enumerable, IEqualityComparer<TKey> comparer, Func<TValueCollection> collectionFactory) where TValueCollection : ICollection<TValue>
		{
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			TValueCollection tvalueCollection = collectionFactory();
			if (tvalueCollection.IsReadOnly)
			{
				throw new InvalidOperationException(Resources.Create_TValueCollectionReadOnly);
			}
			MultiValueDictionary<TKey, TValue> multiValueDictionary = new MultiValueDictionary<TKey, TValue>(comparer);
			multiValueDictionary.NewCollectionFactory = (Func<ICollection<TValue>>)collectionFactory;
			foreach (KeyValuePair<TKey, IReadOnlyCollection<TValue>> keyValuePair in enumerable)
			{
				multiValueDictionary.AddRange(keyValuePair.Key, keyValuePair.Value);
			}
			return multiValueDictionary;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000A090 File Offset: 0x00008290
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			MultiValueDictionary<TKey, TValue>.InnerCollectionView innerCollectionView;
			if (!this.dictionary.TryGetValue(key, out innerCollectionView))
			{
				innerCollectionView = new MultiValueDictionary<TKey, TValue>.InnerCollectionView(key, this.NewCollectionFactory());
				this.dictionary.Add(key, innerCollectionView);
			}
			innerCollectionView.AddValue(value);
			this.version++;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A0F4 File Offset: 0x000082F4
		public void AddRange(TKey key, IEnumerable<TValue> values)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			MultiValueDictionary<TKey, TValue>.InnerCollectionView innerCollectionView;
			if (!this.dictionary.TryGetValue(key, out innerCollectionView))
			{
				innerCollectionView = new MultiValueDictionary<TKey, TValue>.InnerCollectionView(key, this.NewCollectionFactory());
				this.dictionary.Add(key, innerCollectionView);
			}
			foreach (TValue tvalue in values)
			{
				innerCollectionView.AddValue(tvalue);
			}
			this.version++;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A19C File Offset: 0x0000839C
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			MultiValueDictionary<TKey, TValue>.InnerCollectionView innerCollectionView;
			if (this.dictionary.TryGetValue(key, out innerCollectionView) && this.dictionary.Remove(key))
			{
				this.version++;
				return true;
			}
			return false;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000A1EC File Offset: 0x000083EC
		public bool Remove(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			MultiValueDictionary<TKey, TValue>.InnerCollectionView innerCollectionView;
			if (this.dictionary.TryGetValue(key, out innerCollectionView) && innerCollectionView.RemoveValue(value))
			{
				if (innerCollectionView.Count == 0)
				{
					this.dictionary.Remove(key);
				}
				this.version++;
				return true;
			}
			return false;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A24C File Offset: 0x0000844C
		public bool Contains(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			MultiValueDictionary<TKey, TValue>.InnerCollectionView innerCollectionView;
			return this.dictionary.TryGetValue(key, out innerCollectionView) && innerCollectionView.Contains(value);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A288 File Offset: 0x00008488
		public bool ContainsValue(TValue value)
		{
			using (Dictionary<TKey, MultiValueDictionary<TKey, TValue>.InnerCollectionView>.ValueCollection.Enumerator enumerator = this.dictionary.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Contains(value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A2E8 File Offset: 0x000084E8
		public void Clear()
		{
			this.dictionary.Clear();
			this.version++;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000A303 File Offset: 0x00008503
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.dictionary.ContainsKey(key);
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000A324 File Offset: 0x00008524
		public IEnumerable<TKey> Keys
		{
			get
			{
				return this.dictionary.Keys;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000A334 File Offset: 0x00008534
		public bool TryGetValue(TKey key, out IReadOnlyCollection<TValue> value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			MultiValueDictionary<TKey, TValue>.InnerCollectionView innerCollectionView;
			bool flag = this.dictionary.TryGetValue(key, out innerCollectionView);
			value = innerCollectionView;
			return flag;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000A365 File Offset: 0x00008565
		public IEnumerable<IReadOnlyCollection<TValue>> Values
		{
			get
			{
				return this.dictionary.Values;
			}
		}

		// Token: 0x170000E7 RID: 231
		public IReadOnlyCollection<TValue> this[TKey key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				MultiValueDictionary<TKey, TValue>.InnerCollectionView innerCollectionView;
				if (this.dictionary.TryGetValue(key, out innerCollectionView))
				{
					return innerCollectionView;
				}
				throw new KeyNotFoundException();
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000A3AB File Offset: 0x000085AB
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A3B8 File Offset: 0x000085B8
		public IEnumerator<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> GetEnumerator()
		{
			return new MultiValueDictionary<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A3B8 File Offset: 0x000085B8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new MultiValueDictionary<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x04000129 RID: 297
		private Dictionary<TKey, MultiValueDictionary<TKey, TValue>.InnerCollectionView> dictionary;

		// Token: 0x0400012A RID: 298
		private Func<ICollection<TValue>> NewCollectionFactory = () => new List<TValue>();

		// Token: 0x0400012B RID: 299
		private int version;

		// Token: 0x0200006A RID: 106
		private class Enumerator : IEnumerator<KeyValuePair<TKey, IReadOnlyCollection<TValue>>>, IDisposable, IEnumerator
		{
			// Token: 0x0600026C RID: 620 RVA: 0x0000A3C0 File Offset: 0x000085C0
			internal Enumerator(MultiValueDictionary<TKey, TValue> multiValueDictionary)
			{
				this.multiValueDictionary = multiValueDictionary;
				this.version = multiValueDictionary.version;
				this.current = default(KeyValuePair<TKey, IReadOnlyCollection<TValue>>);
				this.enumerator = multiValueDictionary.dictionary.GetEnumerator();
				this.state = MultiValueDictionary<TKey, TValue>.Enumerator.EnumerationState.BeforeFirst;
			}

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x0600026D RID: 621 RVA: 0x0000A3FF File Offset: 0x000085FF
			public KeyValuePair<TKey, IReadOnlyCollection<TValue>> Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x0600026E RID: 622 RVA: 0x0000A408 File Offset: 0x00008608
			object IEnumerator.Current
			{
				get
				{
					MultiValueDictionary<TKey, TValue>.Enumerator.EnumerationState enumerationState = this.state;
					if (enumerationState == MultiValueDictionary<TKey, TValue>.Enumerator.EnumerationState.BeforeFirst)
					{
						throw new InvalidOperationException(Resources.InvalidOperation_EnumNotStarted);
					}
					if (enumerationState != MultiValueDictionary<TKey, TValue>.Enumerator.EnumerationState.AfterLast)
					{
						return this.current;
					}
					throw new InvalidOperationException(Resources.InvalidOperation_EnumEnded);
				}
			}

			// Token: 0x0600026F RID: 623 RVA: 0x0000A448 File Offset: 0x00008648
			public bool MoveNext()
			{
				if (this.version != this.multiValueDictionary.version)
				{
					throw new InvalidOperationException(Resources.InvalidOperation_EnumFailedVersion);
				}
				if (this.enumerator.MoveNext())
				{
					KeyValuePair<TKey, MultiValueDictionary<TKey, TValue>.InnerCollectionView> keyValuePair = this.enumerator.Current;
					TKey key = keyValuePair.Key;
					keyValuePair = this.enumerator.Current;
					this.current = new KeyValuePair<TKey, IReadOnlyCollection<TValue>>(key, keyValuePair.Value);
					this.state = MultiValueDictionary<TKey, TValue>.Enumerator.EnumerationState.During;
					return true;
				}
				this.current = default(KeyValuePair<TKey, IReadOnlyCollection<TValue>>);
				this.state = MultiValueDictionary<TKey, TValue>.Enumerator.EnumerationState.AfterLast;
				return false;
			}

			// Token: 0x06000270 RID: 624 RVA: 0x0000A4D0 File Offset: 0x000086D0
			public void Reset()
			{
				if (this.version != this.multiValueDictionary.version)
				{
					throw new InvalidOperationException(Resources.InvalidOperation_EnumFailedVersion);
				}
				this.enumerator.Dispose();
				this.enumerator = this.multiValueDictionary.dictionary.GetEnumerator();
				this.current = default(KeyValuePair<TKey, IReadOnlyCollection<TValue>>);
				this.state = MultiValueDictionary<TKey, TValue>.Enumerator.EnumerationState.BeforeFirst;
			}

			// Token: 0x06000271 RID: 625 RVA: 0x0000A52F File Offset: 0x0000872F
			public void Dispose()
			{
				this.enumerator.Dispose();
			}

			// Token: 0x0400012C RID: 300
			private MultiValueDictionary<TKey, TValue> multiValueDictionary;

			// Token: 0x0400012D RID: 301
			private int version;

			// Token: 0x0400012E RID: 302
			private KeyValuePair<TKey, IReadOnlyCollection<TValue>> current;

			// Token: 0x0400012F RID: 303
			private Dictionary<TKey, MultiValueDictionary<TKey, TValue>.InnerCollectionView>.Enumerator enumerator;

			// Token: 0x04000130 RID: 304
			private MultiValueDictionary<TKey, TValue>.Enumerator.EnumerationState state;

			// Token: 0x0200006B RID: 107
			private enum EnumerationState
			{
				// Token: 0x04000132 RID: 306
				BeforeFirst,
				// Token: 0x04000133 RID: 307
				During,
				// Token: 0x04000134 RID: 308
				AfterLast
			}
		}

		// Token: 0x0200006C RID: 108
		private class InnerCollectionView : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, IReadOnlyCollection<TValue>
		{
			// Token: 0x06000272 RID: 626 RVA: 0x0000A53C File Offset: 0x0000873C
			public InnerCollectionView(TKey key, ICollection<TValue> collection)
			{
				this.key = key;
				this.collection = collection;
			}

			// Token: 0x06000273 RID: 627 RVA: 0x0000A552 File Offset: 0x00008752
			public void AddValue(TValue item)
			{
				this.collection.Add(item);
			}

			// Token: 0x06000274 RID: 628 RVA: 0x0000A560 File Offset: 0x00008760
			public bool RemoveValue(TValue item)
			{
				return this.collection.Remove(item);
			}

			// Token: 0x06000275 RID: 629 RVA: 0x0000A56E File Offset: 0x0000876E
			public bool Contains(TValue item)
			{
				return this.collection.Contains(item);
			}

			// Token: 0x06000276 RID: 630 RVA: 0x0000A57C File Offset: 0x0000877C
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", Resources.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (arrayIndex > array.Length)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", Resources.ArgumentOutOfRange_Index);
				}
				if (array.Length - arrayIndex < this.collection.Count)
				{
					throw new ArgumentException(Resources.CopyTo_ArgumentsTooSmall, "arrayIndex");
				}
				this.collection.CopyTo(array, arrayIndex);
			}

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x06000277 RID: 631 RVA: 0x0000A5F0 File Offset: 0x000087F0
			public int Count
			{
				get
				{
					return this.collection.Count;
				}
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x06000278 RID: 632 RVA: 0x0000A5FD File Offset: 0x000087FD
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000279 RID: 633 RVA: 0x0000A600 File Offset: 0x00008800
			public IEnumerator<TValue> GetEnumerator()
			{
				return this.collection.GetEnumerator();
			}

			// Token: 0x0600027A RID: 634 RVA: 0x0000A60D File Offset: 0x0000880D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x0600027B RID: 635 RVA: 0x0000A615 File Offset: 0x00008815
			public TKey Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x0600027C RID: 636 RVA: 0x0000A61D File Offset: 0x0000881D
			void ICollection<TValue>.Add(TValue item)
			{
				throw new NotSupportedException(Resources.ReadOnly_Modification);
			}

			// Token: 0x0600027D RID: 637 RVA: 0x0000A61D File Offset: 0x0000881D
			void ICollection<TValue>.Clear()
			{
				throw new NotSupportedException(Resources.ReadOnly_Modification);
			}

			// Token: 0x0600027E RID: 638 RVA: 0x0000A61D File Offset: 0x0000881D
			bool ICollection<TValue>.Remove(TValue item)
			{
				throw new NotSupportedException(Resources.ReadOnly_Modification);
			}

			// Token: 0x04000135 RID: 309
			private TKey key;

			// Token: 0x04000136 RID: 310
			private ICollection<TValue> collection;
		}
	}
}
