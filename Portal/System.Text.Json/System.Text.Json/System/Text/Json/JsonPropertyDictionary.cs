using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Text.Json
{
	// Token: 0x02000040 RID: 64
	internal sealed class JsonPropertyDictionary<T> where T : class
	{
		// Token: 0x0600031E RID: 798 RVA: 0x000094BD File Offset: 0x000076BD
		public JsonPropertyDictionary(bool caseInsensitive)
		{
			this._stringComparer = (caseInsensitive ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal);
			this._propertyList = new List<KeyValuePair<string, T>>();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000094E5 File Offset: 0x000076E5
		public JsonPropertyDictionary(bool caseInsensitive, int capacity)
		{
			this._stringComparer = (caseInsensitive ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal);
			this._propertyList = new List<KeyValuePair<string, T>>(capacity);
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000950E File Offset: 0x0000770E
		public List<KeyValuePair<string, T>> List
		{
			get
			{
				return this._propertyList;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00009516 File Offset: 0x00007716
		public void Add(string propertyName, T value)
		{
			if (this.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException_CollectionIsReadOnly();
			}
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.AddValue(propertyName, value);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000953A File Offset: 0x0000773A
		public void Add(KeyValuePair<string, T> property)
		{
			if (this.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException_CollectionIsReadOnly();
			}
			this.Add(property.Key, property.Value);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000955D File Offset: 0x0000775D
		public bool TryAdd(string propertyName, T value)
		{
			if (this.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException_CollectionIsReadOnly();
			}
			return this.TryAddValue(propertyName, value);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00009574 File Offset: 0x00007774
		public void Clear()
		{
			if (this.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException_CollectionIsReadOnly();
			}
			this._propertyList.Clear();
			Dictionary<string, T> propertyDictionary = this._propertyDictionary;
			if (propertyDictionary == null)
			{
				return;
			}
			propertyDictionary.Clear();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000959E File Offset: 0x0000779E
		public bool ContainsKey(string propertyName)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			return this.ContainsProperty(propertyName);
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000326 RID: 806 RVA: 0x000095B4 File Offset: 0x000077B4
		public int Count
		{
			get
			{
				return this._propertyList.Count;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000095C4 File Offset: 0x000077C4
		public bool Remove(string propertyName)
		{
			if (this.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException_CollectionIsReadOnly();
			}
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			T t;
			return this.TryRemoveProperty(propertyName, out t);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000095F4 File Offset: 0x000077F4
		public bool Contains(KeyValuePair<string, T> item)
		{
			foreach (KeyValuePair<string, T> keyValuePair in this)
			{
				if (item.Value == keyValuePair.Value && this._stringComparer.Equals(item.Key, keyValuePair.Key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00009678 File Offset: 0x00007878
		public void CopyTo(KeyValuePair<string, T>[] array, int index)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_ArrayIndexNegative("index");
			}
			foreach (KeyValuePair<string, T> keyValuePair in this._propertyList)
			{
				if (index >= array.Length)
				{
					ThrowHelper.ThrowArgumentException_ArrayTooSmall("array");
				}
				array[index++] = keyValuePair;
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000096F0 File Offset: 0x000078F0
		public List<KeyValuePair<string, T>>.Enumerator GetEnumerator()
		{
			return this._propertyList.GetEnumerator();
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600032B RID: 811 RVA: 0x000096FD File Offset: 0x000078FD
		public IList<string> Keys
		{
			get
			{
				return this.GetKeyCollection();
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00009705 File Offset: 0x00007905
		public IList<T> Values
		{
			get
			{
				return this.GetValueCollection();
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00009710 File Offset: 0x00007910
		public bool TryGetValue(string propertyName, [MaybeNullWhen(false)] out T value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			if (this._propertyDictionary != null)
			{
				return this._propertyDictionary.TryGetValue(propertyName, out value);
			}
			foreach (KeyValuePair<string, T> keyValuePair in this._propertyList)
			{
				if (this._stringComparer.Equals(propertyName, keyValuePair.Key))
				{
					value = keyValuePair.Value;
					return true;
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600032E RID: 814 RVA: 0x000097B0 File Offset: 0x000079B0
		// (set) Token: 0x0600032F RID: 815 RVA: 0x000097B8 File Offset: 0x000079B8
		public bool IsReadOnly { get; set; }

		// Token: 0x17000125 RID: 293
		public T this[string propertyName]
		{
			get
			{
				T t;
				if (this.TryGetPropertyValue(propertyName, out t))
				{
					return t;
				}
				return default(T);
			}
			[param: DisallowNull]
			set
			{
				bool flag;
				this.SetValue(propertyName, value, out flag);
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00009800 File Offset: 0x00007A00
		public T SetValue(string propertyName, T value, out bool valueAlreadyInDictionary)
		{
			if (this.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException_CollectionIsReadOnly();
			}
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.CreateDictionaryIfThresholdMet();
			valueAlreadyInDictionary = false;
			T t = default(T);
			if (this._propertyDictionary != null)
			{
				if (this._propertyDictionary.TryAdd(propertyName, value))
				{
					this._propertyList.Add(new KeyValuePair<string, T>(propertyName, value));
					return default(T);
				}
				t = this._propertyDictionary[propertyName];
				if (t == value)
				{
					valueAlreadyInDictionary = true;
					return default(T);
				}
			}
			int num = this.FindValueIndex(propertyName);
			if (num >= 0)
			{
				if (this._propertyDictionary != null)
				{
					this._propertyDictionary[propertyName] = value;
				}
				else
				{
					KeyValuePair<string, T> keyValuePair = this._propertyList[num];
					if (keyValuePair.Value == value)
					{
						valueAlreadyInDictionary = true;
						return default(T);
					}
					t = keyValuePair.Value;
				}
				this._propertyList[num] = new KeyValuePair<string, T>(propertyName, value);
			}
			else
			{
				Dictionary<string, T> propertyDictionary = this._propertyDictionary;
				if (propertyDictionary != null)
				{
					propertyDictionary.Add(propertyName, value);
				}
				this._propertyList.Add(new KeyValuePair<string, T>(propertyName, value));
			}
			return t;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00009927 File Offset: 0x00007B27
		private void AddValue(string propertyName, T value)
		{
			if (!this.TryAddValue(propertyName, value))
			{
				ThrowHelper.ThrowArgumentException_DuplicateKey("propertyName", propertyName);
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00009940 File Offset: 0x00007B40
		internal bool TryAddValue(string propertyName, T value)
		{
			if (this.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException_CollectionIsReadOnly();
			}
			this.CreateDictionaryIfThresholdMet();
			if (this._propertyDictionary == null)
			{
				if (this.ContainsProperty(propertyName))
				{
					return false;
				}
			}
			else if (!this._propertyDictionary.TryAdd(propertyName, value))
			{
				return false;
			}
			this._propertyList.Add(new KeyValuePair<string, T>(propertyName, value));
			return true;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00009997 File Offset: 0x00007B97
		private void CreateDictionaryIfThresholdMet()
		{
			if (this._propertyDictionary == null && this._propertyList.Count > 9)
			{
				this._propertyDictionary = JsonHelpers.CreateDictionaryFromCollection<string, T>(this._propertyList, this._stringComparer);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000099C8 File Offset: 0x00007BC8
		internal bool ContainsValue(T value)
		{
			foreach (T t in this.GetValueCollection())
			{
				if (t == value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00009A24 File Offset: 0x00007C24
		public KeyValuePair<string, T>? FindValue(T value)
		{
			foreach (KeyValuePair<string, T> keyValuePair in this)
			{
				if (keyValuePair.Value == value)
				{
					return new KeyValuePair<string, T>?(keyValuePair);
				}
			}
			return null;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00009A94 File Offset: 0x00007C94
		private bool ContainsProperty(string propertyName)
		{
			if (this._propertyDictionary != null)
			{
				return this._propertyDictionary.ContainsKey(propertyName);
			}
			foreach (KeyValuePair<string, T> keyValuePair in this._propertyList)
			{
				if (this._stringComparer.Equals(propertyName, keyValuePair.Key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00009B14 File Offset: 0x00007D14
		private int FindValueIndex(string propertyName)
		{
			for (int i = 0; i < this._propertyList.Count; i++)
			{
				KeyValuePair<string, T> keyValuePair = this._propertyList[i];
				if (this._stringComparer.Equals(propertyName, keyValuePair.Key))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00009B5C File Offset: 0x00007D5C
		public bool TryGetPropertyValue(string propertyName, [MaybeNullWhen(false)] out T value)
		{
			return this.TryGetValue(propertyName, out value);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00009B68 File Offset: 0x00007D68
		public bool TryRemoveProperty(string propertyName, [MaybeNullWhen(false)] out T existing)
		{
			if (this.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException_CollectionIsReadOnly();
			}
			if (this._propertyDictionary != null)
			{
				if (!this._propertyDictionary.TryGetValue(propertyName, out existing))
				{
					return false;
				}
				bool flag = this._propertyDictionary.Remove(propertyName);
			}
			for (int i = 0; i < this._propertyList.Count; i++)
			{
				KeyValuePair<string, T> keyValuePair = this._propertyList[i];
				if (this._stringComparer.Equals(keyValuePair.Key, propertyName))
				{
					this._propertyList.RemoveAt(i);
					existing = keyValuePair.Value;
					return true;
				}
			}
			existing = default(T);
			return false;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00009C04 File Offset: 0x00007E04
		public IList<string> GetKeyCollection()
		{
			JsonPropertyDictionary<T>.KeyCollection keyCollection;
			if ((keyCollection = this._keyCollection) == null)
			{
				keyCollection = (this._keyCollection = new JsonPropertyDictionary<T>.KeyCollection(this));
			}
			return keyCollection;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00009C2C File Offset: 0x00007E2C
		public IList<T> GetValueCollection()
		{
			JsonPropertyDictionary<T>.ValueCollection valueCollection;
			if ((valueCollection = this._valueCollection) == null)
			{
				valueCollection = (this._valueCollection = new JsonPropertyDictionary<T>.ValueCollection(this));
			}
			return valueCollection;
		}

		// Token: 0x0400013E RID: 318
		private const int ListToDictionaryThreshold = 9;

		// Token: 0x0400013F RID: 319
		private Dictionary<string, T> _propertyDictionary;

		// Token: 0x04000140 RID: 320
		private readonly List<KeyValuePair<string, T>> _propertyList;

		// Token: 0x04000141 RID: 321
		private readonly StringComparer _stringComparer;

		// Token: 0x04000143 RID: 323
		private JsonPropertyDictionary<T>.KeyCollection _keyCollection;

		// Token: 0x04000144 RID: 324
		private JsonPropertyDictionary<T>.ValueCollection _valueCollection;

		// Token: 0x0200011B RID: 283
		private sealed class KeyCollection : IList<string>, ICollection<string>, IEnumerable<string>, IEnumerable
		{
			// Token: 0x06000D64 RID: 3428 RVA: 0x000341DA File Offset: 0x000323DA
			public KeyCollection(JsonPropertyDictionary<T> jsonObject)
			{
				this._parent = jsonObject;
			}

			// Token: 0x170002DC RID: 732
			// (get) Token: 0x06000D65 RID: 3429 RVA: 0x000341E9 File Offset: 0x000323E9
			public int Count
			{
				get
				{
					return this._parent.Count;
				}
			}

			// Token: 0x170002DD RID: 733
			// (get) Token: 0x06000D66 RID: 3430 RVA: 0x000341F6 File Offset: 0x000323F6
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170002DE RID: 734
			public string this[int index]
			{
				get
				{
					return this._parent.List[index].Key;
				}
				set
				{
					throw ThrowHelper.GetNotSupportedException_CollectionIsReadOnly();
				}
			}

			// Token: 0x06000D69 RID: 3433 RVA: 0x00034229 File Offset: 0x00032429
			IEnumerator IEnumerable.GetEnumerator()
			{
				foreach (KeyValuePair<string, T> keyValuePair in this._parent)
				{
					yield return keyValuePair.Key;
				}
				List<KeyValuePair<string, T>>.Enumerator enumerator = default(List<KeyValuePair<string, T>>.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x06000D6A RID: 3434 RVA: 0x00034238 File Offset: 0x00032438
			public void Add(string propertyName)
			{
				ThrowHelper.ThrowNotSupportedException_CollectionIsReadOnly();
			}

			// Token: 0x06000D6B RID: 3435 RVA: 0x0003423F File Offset: 0x0003243F
			public void Clear()
			{
				ThrowHelper.ThrowNotSupportedException_CollectionIsReadOnly();
			}

			// Token: 0x06000D6C RID: 3436 RVA: 0x00034246 File Offset: 0x00032446
			public bool Contains(string propertyName)
			{
				return this._parent.ContainsProperty(propertyName);
			}

			// Token: 0x06000D6D RID: 3437 RVA: 0x00034254 File Offset: 0x00032454
			public void CopyTo(string[] propertyNameArray, int index)
			{
				if (index < 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_ArrayIndexNegative("index");
				}
				foreach (KeyValuePair<string, T> keyValuePair in this._parent)
				{
					if (index >= propertyNameArray.Length)
					{
						ThrowHelper.ThrowArgumentException_ArrayTooSmall("propertyNameArray");
					}
					propertyNameArray[index++] = keyValuePair.Key;
				}
			}

			// Token: 0x06000D6E RID: 3438 RVA: 0x000342D0 File Offset: 0x000324D0
			public IEnumerator<string> GetEnumerator()
			{
				foreach (KeyValuePair<string, T> keyValuePair in this._parent)
				{
					yield return keyValuePair.Key;
				}
				List<KeyValuePair<string, T>>.Enumerator enumerator = default(List<KeyValuePair<string, T>>.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x06000D6F RID: 3439 RVA: 0x000342DF File Offset: 0x000324DF
			bool ICollection<string>.Remove(string propertyName)
			{
				throw ThrowHelper.GetNotSupportedException_CollectionIsReadOnly();
			}

			// Token: 0x06000D70 RID: 3440 RVA: 0x000342E6 File Offset: 0x000324E6
			public int IndexOf(string item)
			{
				throw ThrowHelper.GetNotSupportedException_CollectionIsReadOnly();
			}

			// Token: 0x06000D71 RID: 3441 RVA: 0x000342ED File Offset: 0x000324ED
			public void Insert(int index, string item)
			{
				throw ThrowHelper.GetNotSupportedException_CollectionIsReadOnly();
			}

			// Token: 0x06000D72 RID: 3442 RVA: 0x000342F4 File Offset: 0x000324F4
			public void RemoveAt(int index)
			{
				throw ThrowHelper.GetNotSupportedException_CollectionIsReadOnly();
			}

			// Token: 0x0400046F RID: 1135
			private readonly JsonPropertyDictionary<T> _parent;
		}

		// Token: 0x0200011C RID: 284
		private sealed class ValueCollection : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x06000D73 RID: 3443 RVA: 0x000342FB File Offset: 0x000324FB
			public ValueCollection(JsonPropertyDictionary<T> jsonObject)
			{
				this._parent = jsonObject;
			}

			// Token: 0x170002DF RID: 735
			// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0003430A File Offset: 0x0003250A
			public int Count
			{
				get
				{
					return this._parent.Count;
				}
			}

			// Token: 0x170002E0 RID: 736
			// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00034317 File Offset: 0x00032517
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170002E1 RID: 737
			public T this[int index]
			{
				get
				{
					return this._parent.List[index].Value;
				}
				set
				{
					throw ThrowHelper.GetNotSupportedException_CollectionIsReadOnly();
				}
			}

			// Token: 0x06000D78 RID: 3448 RVA: 0x00034349 File Offset: 0x00032549
			IEnumerator IEnumerable.GetEnumerator()
			{
				foreach (KeyValuePair<string, T> keyValuePair in this._parent)
				{
					yield return keyValuePair.Value;
				}
				List<KeyValuePair<string, T>>.Enumerator enumerator = default(List<KeyValuePair<string, T>>.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x06000D79 RID: 3449 RVA: 0x00034358 File Offset: 0x00032558
			public void Add(T jsonNode)
			{
				ThrowHelper.ThrowNotSupportedException_CollectionIsReadOnly();
			}

			// Token: 0x06000D7A RID: 3450 RVA: 0x0003435F File Offset: 0x0003255F
			public void Clear()
			{
				ThrowHelper.ThrowNotSupportedException_CollectionIsReadOnly();
			}

			// Token: 0x06000D7B RID: 3451 RVA: 0x00034366 File Offset: 0x00032566
			public bool Contains(T jsonNode)
			{
				return this._parent.ContainsValue(jsonNode);
			}

			// Token: 0x06000D7C RID: 3452 RVA: 0x00034374 File Offset: 0x00032574
			public void CopyTo(T[] nodeArray, int index)
			{
				if (index < 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_ArrayIndexNegative("index");
				}
				foreach (KeyValuePair<string, T> keyValuePair in this._parent)
				{
					if (index >= nodeArray.Length)
					{
						ThrowHelper.ThrowArgumentException_ArrayTooSmall("nodeArray");
					}
					nodeArray[index++] = keyValuePair.Value;
				}
			}

			// Token: 0x06000D7D RID: 3453 RVA: 0x000343F4 File Offset: 0x000325F4
			public IEnumerator<T> GetEnumerator()
			{
				foreach (KeyValuePair<string, T> keyValuePair in this._parent)
				{
					yield return keyValuePair.Value;
				}
				List<KeyValuePair<string, T>>.Enumerator enumerator = default(List<KeyValuePair<string, T>>.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x06000D7E RID: 3454 RVA: 0x00034403 File Offset: 0x00032603
			bool ICollection<T>.Remove(T node)
			{
				throw ThrowHelper.GetNotSupportedException_CollectionIsReadOnly();
			}

			// Token: 0x06000D7F RID: 3455 RVA: 0x0003440A File Offset: 0x0003260A
			public int IndexOf(T item)
			{
				throw ThrowHelper.GetNotSupportedException_CollectionIsReadOnly();
			}

			// Token: 0x06000D80 RID: 3456 RVA: 0x00034411 File Offset: 0x00032611
			public void Insert(int index, T item)
			{
				throw ThrowHelper.GetNotSupportedException_CollectionIsReadOnly();
			}

			// Token: 0x06000D81 RID: 3457 RVA: 0x00034418 File Offset: 0x00032618
			public void RemoveAt(int index)
			{
				throw ThrowHelper.GetNotSupportedException_CollectionIsReadOnly();
			}

			// Token: 0x04000470 RID: 1136
			private readonly JsonPropertyDictionary<T> _parent;
		}
	}
}
