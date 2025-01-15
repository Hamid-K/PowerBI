using System;
using System.Collections;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000F3 RID: 243
	internal abstract class XmlaPropertyCollectionBase : ICollection, IEnumerable, IList, IDictionary
	{
		// Token: 0x06000D3C RID: 3388 RVA: 0x0003026C File Offset: 0x0002E46C
		internal XmlaPropertyCollectionBase()
		{
			XmlaPropertyCollectionBase.HashHelper hashHelper = new XmlaPropertyCollectionBase.HashHelper();
			this.hashStore = new Hashtable(hashHelper);
			this.items = new ArrayList();
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000D3D RID: 3389
		protected abstract Type ItemType { get; }

		// Token: 0x06000D3E RID: 3390
		protected abstract IXmlaProperty CreateBasePropertyObject(IXmlaPropertyKey key, object propertyValue);

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000D3F RID: 3391
		protected abstract object Parent { get; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0003029C File Offset: 0x0002E49C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0003029F File Offset: 0x0002E49F
		public object SyncRoot
		{
			get
			{
				return this.items.SyncRoot;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x000302AC File Offset: 0x0002E4AC
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x000302B9 File Offset: 0x0002E4B9
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x000302C8 File Offset: 0x0002E4C8
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x000302CB File Offset: 0x0002E4CB
		// (set) Token: 0x06000D46 RID: 3398 RVA: 0x000302D3 File Offset: 0x0002E4D3
		public bool IsReadOnly { get; internal set; }

		// Token: 0x1700051D RID: 1309
		public IXmlaProperty this[int index]
		{
			get
			{
				this.RangeCheck(index);
				return (IXmlaProperty)this.items[index];
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.Collection_IsReadOnly);
				}
				this.RangeCheck(index);
				this.Replace(index, value);
			}
		}

		// Token: 0x1700051E RID: 1310
		public object this[IXmlaPropertyKey key]
		{
			get
			{
				int num = this.IndexOf(key);
				if (num == -1)
				{
					throw new ArgumentException(SR.Property_DoesNotExist, "key");
				}
				return this[num];
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.Collection_IsReadOnly);
				}
				int num = this.IndexOf(key);
				if (num == -1)
				{
					this.Add(this.CreateBasePropertyObject(key, value));
					return;
				}
				((IXmlaProperty)this.items[num]).Value = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.Collection_IsReadOnly);
				}
				this.ValidateType(value);
				this[index] = (IXmlaProperty)value;
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x000303D4 File Offset: 0x0002E5D4
		public IXmlaProperty Add(IXmlaProperty value)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			this.Validate(-1, value);
			value.Parent = this.Parent;
			int num = this.items.Add(value);
			this.hashStore[value] = num;
			return value;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00030428 File Offset: 0x0002E628
		int IList.Add(object value)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			this.ValidateType(value);
			this.Add((IXmlaProperty)value);
			return this.Count - 1;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0003045C File Offset: 0x0002E65C
		public void Clear()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				((IXmlaProperty)this.items[i]).Parent = null;
			}
			this.items.Clear();
			this.hashStore.Clear();
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x000304C1 File Offset: 0x0002E6C1
		public bool Contains(IXmlaProperty property)
		{
			return -1 != this.IndexOf(property);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x000304D0 File Offset: 0x0002E6D0
		public bool Contains(IXmlaPropertyKey key)
		{
			return -1 != this.IndexOf(key);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x000304DF File Offset: 0x0002E6DF
		bool IList.Contains(object value)
		{
			this.ValidateType(value);
			return this.Contains((IXmlaProperty)value);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x000304F4 File Offset: 0x0002E6F4
		public int IndexOf(IXmlaProperty property)
		{
			return this.items.IndexOf(property);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00030504 File Offset: 0x0002E704
		public int IndexOf(IXmlaPropertyKey key)
		{
			int num = -1;
			if (this.hashStore.ContainsKey(key))
			{
				num = (int)this.hashStore[key];
			}
			return num;
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00030534 File Offset: 0x0002E734
		int IList.IndexOf(object value)
		{
			this.ValidateType(value);
			return this.IndexOf((IXmlaProperty)value);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0003054C File Offset: 0x0002E74C
		public void Insert(int index, IXmlaProperty value)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			this.Validate(-1, value);
			value.Parent = this.Parent;
			this.items.Insert(index, value);
			this.hashStore[value] = index;
			for (int i = this.items.Count - 1; i > index; i--)
			{
				this.hashStore[(IXmlaProperty)this.items[i]] = i;
			}
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x000305D9 File Offset: 0x0002E7D9
		void IList.Insert(int index, object value)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			this.ValidateType(value);
			this.Insert(index, (IXmlaProperty)value);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00030604 File Offset: 0x0002E804
		public void Remove(IXmlaProperty value)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			this.ValidateType(value);
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveIndex(num);
				return;
			}
			throw new ArgumentException(SR.Property_DoesNotExist, "value");
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00030650 File Offset: 0x0002E850
		public void Remove(IXmlaPropertyKey value)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveIndex(num);
				return;
			}
			throw new ArgumentException(SR.Property_DoesNotExist, "value");
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00030693 File Offset: 0x0002E893
		void IList.Remove(object value)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			this.ValidateType(value);
			this.Remove((IXmlaProperty)value);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x000306BB File Offset: 0x0002E8BB
		public void RemoveAt(int index)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x000306DE File Offset: 0x0002E8DE
		void IDictionary.Add(object key, object value)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			this.ValidateKeyType(key);
			this[(IXmlaPropertyKey)key] = value;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00030707 File Offset: 0x0002E907
		bool IDictionary.Contains(object key)
		{
			this.ValidateKeyType(key);
			return this.Contains((IXmlaPropertyKey)key);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0003071C File Offset: 0x0002E91C
		void IDictionary.Remove(object key)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			this.ValidateKeyType(key);
			this.Remove((IXmlaPropertyKey)key);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00030744 File Offset: 0x0002E944
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new XmlaPropertyCollectionBase.XmlaPropertyDictionaryEnumerator(this);
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0003074C File Offset: 0x0002E94C
		public ICollection Keys
		{
			get
			{
				return this.hashStore.Keys;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x0003075C File Offset: 0x0002E95C
		public ICollection Values
		{
			get
			{
				object[] array = new object[this.Count];
				for (int i = 0; i < this.Count; i++)
				{
					array[i] = ((IXmlaProperty)this.items[i]).Value;
				}
				return array;
			}
		}

		// Token: 0x17000522 RID: 1314
		object IDictionary.this[object key]
		{
			get
			{
				this.ValidateKeyType(key);
				return this[(IXmlaPropertyKey)key];
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.Collection_IsReadOnly);
				}
				this.ValidateKeyType(key);
				this[(IXmlaPropertyKey)key] = value;
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000307DE File Offset: 0x0002E9DE
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x000307EC File Offset: 0x0002E9EC
		internal void ChangeName(IXmlaProperty property, string newName)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			XmlaPropertyKey xmlaPropertyKey = new XmlaPropertyKey(newName, property.Namespace);
			int num = this.RemoveKey(xmlaPropertyKey, property);
			property.Name = newName;
			this.hashStore[property] = num;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0003083C File Offset: 0x0002EA3C
		internal void ChangeNamespace(IXmlaProperty property, string newNamespace)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			XmlaPropertyKey xmlaPropertyKey = new XmlaPropertyKey(property.Name, newNamespace);
			int num = this.RemoveKey(xmlaPropertyKey, property);
			property.Namespace = newNamespace;
			this.hashStore[property] = num;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0003088C File Offset: 0x0002EA8C
		internal void Validate(int index, IXmlaProperty value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Parent != null && this.Parent != value.Parent)
			{
				throw new ArgumentException(SR.Property_Parent_Mismatch, "value");
			}
			if (index != this.IndexOf(value))
			{
				throw new ArgumentException(SR.Property_Already_Exists, "value");
			}
			string name = value.Name;
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.Connection_PropertyNameEmpty, "Name");
			}
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00030906 File Offset: 0x0002EB06
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00030920 File Offset: 0x0002EB20
		private void Replace(int index, IXmlaProperty newValue)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			this.Validate(index, newValue);
			IXmlaProperty xmlaProperty = (IXmlaProperty)this.items[index];
			xmlaProperty.Parent = null;
			newValue.Parent = this.Parent;
			this.items[index] = newValue;
			this.hashStore.Remove(xmlaProperty);
			this.hashStore.Add(newValue, index);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00030998 File Offset: 0x0002EB98
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.ItemType.IsInstanceOfType(value))
			{
				throw new ArgumentException(SR.Property_Value_Wrong_Type, "value");
			}
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x000309C6 File Offset: 0x0002EBC6
		private void ValidateKeyType(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!typeof(IXmlaPropertyKey).IsInstanceOfType(value))
			{
				throw new ArgumentException(SR.Property_Key_Wrong_Type, "value");
			}
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x000309F8 File Offset: 0x0002EBF8
		private void RemoveIndex(int index)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			IXmlaProperty xmlaProperty = (IXmlaProperty)this.items[index];
			xmlaProperty.Parent = null;
			this.items.RemoveAt(index);
			this.hashStore.Remove(xmlaProperty);
			for (int i = index; i < this.items.Count; i++)
			{
				this.hashStore[(IXmlaProperty)this.items[i]] = i;
			}
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00030A84 File Offset: 0x0002EC84
		private int RemoveKey(XmlaPropertyKey key, IXmlaProperty property)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			if (this.hashStore.ContainsKey(key))
			{
				throw new ArgumentException(SR.Property_Already_Exists, "key");
			}
			int num = this.IndexOf(property);
			this.hashStore.Remove(property);
			return num;
		}

		// Token: 0x04000856 RID: 2134
		private Hashtable hashStore;

		// Token: 0x04000857 RID: 2135
		private ArrayList items;

		// Token: 0x020001C9 RID: 457
		private class HashHelper : IEqualityComparer, IComparer
		{
			// Token: 0x060013B4 RID: 5044 RVA: 0x00044FD8 File Offset: 0x000431D8
			internal HashHelper()
			{
			}

			// Token: 0x060013B5 RID: 5045 RVA: 0x00044FE0 File Offset: 0x000431E0
			bool IEqualityComparer.Equals(object x, object y)
			{
				return ((IComparer)this).Compare(x, y) == 0;
			}

			// Token: 0x060013B6 RID: 5046 RVA: 0x00044FF0 File Offset: 0x000431F0
			int IComparer.Compare(object x, object y)
			{
				IXmlaPropertyKey xmlaPropertyKey = (IXmlaPropertyKey)x;
				IXmlaPropertyKey xmlaPropertyKey2 = (IXmlaPropertyKey)y;
				int num = string.Compare(xmlaPropertyKey.Name, xmlaPropertyKey2.Name, StringComparison.Ordinal);
				if (num == 0)
				{
					num = string.Compare(xmlaPropertyKey.Name, xmlaPropertyKey2.Name, StringComparison.Ordinal);
				}
				return num;
			}

			// Token: 0x060013B7 RID: 5047 RVA: 0x00045038 File Offset: 0x00043238
			int IEqualityComparer.GetHashCode(object obj)
			{
				IXmlaPropertyKey xmlaPropertyKey = (IXmlaPropertyKey)obj;
				return string.Format(CultureInfo.InvariantCulture, "{0}#{1}", (xmlaPropertyKey.Name != null) ? xmlaPropertyKey.Name.GetHashCode().ToString(CultureInfo.InvariantCulture) : string.Empty, (xmlaPropertyKey.Namespace != null) ? xmlaPropertyKey.Namespace.GetHashCode().ToString(CultureInfo.InvariantCulture) : string.Empty).GetHashCode();
			}
		}

		// Token: 0x020001CA RID: 458
		private class XmlaPropertyDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060013B8 RID: 5048 RVA: 0x000450AE File Offset: 0x000432AE
			internal XmlaPropertyDictionaryEnumerator(XmlaPropertyCollectionBase collection)
			{
				this.collection = collection;
				this.current = -1;
			}

			// Token: 0x170006E3 RID: 1763
			// (get) Token: 0x060013B9 RID: 5049 RVA: 0x000450CC File Offset: 0x000432CC
			public DictionaryEntry Entry
			{
				get
				{
					return new DictionaryEntry(((IDictionaryEnumerator)this).Key, ((IDictionaryEnumerator)this).Value);
				}
			}

			// Token: 0x170006E4 RID: 1764
			// (get) Token: 0x060013BA RID: 5050 RVA: 0x000450EC File Offset: 0x000432EC
			public IXmlaPropertyKey Key
			{
				get
				{
					IXmlaPropertyKey xmlaPropertyKey;
					try
					{
						xmlaPropertyKey = this.collection[this.current];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return xmlaPropertyKey;
				}
			}

			// Token: 0x170006E5 RID: 1765
			// (get) Token: 0x060013BB RID: 5051 RVA: 0x00045128 File Offset: 0x00043328
			object IDictionaryEnumerator.Key
			{
				get
				{
					return this.Key;
				}
			}

			// Token: 0x170006E6 RID: 1766
			// (get) Token: 0x060013BC RID: 5052 RVA: 0x00045130 File Offset: 0x00043330
			public object Value
			{
				get
				{
					object value;
					try
					{
						value = this.collection[this.current].Value;
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return value;
				}
			}

			// Token: 0x170006E7 RID: 1767
			// (get) Token: 0x060013BD RID: 5053 RVA: 0x00045170 File Offset: 0x00043370
			public DictionaryEntry Current
			{
				get
				{
					return ((IDictionaryEnumerator)this).Entry;
				}
			}

			// Token: 0x170006E8 RID: 1768
			// (get) Token: 0x060013BE RID: 5054 RVA: 0x00045178 File Offset: 0x00043378
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060013BF RID: 5055 RVA: 0x00045185 File Offset: 0x00043385
			public void Reset()
			{
				this.current = -1;
			}

			// Token: 0x060013C0 RID: 5056 RVA: 0x00045190 File Offset: 0x00043390
			public bool MoveNext()
			{
				int num = this.current + 1;
				this.current = num;
				return num < this.collection.Count;
			}

			// Token: 0x04000D00 RID: 3328
			private XmlaPropertyCollectionBase collection;

			// Token: 0x04000D01 RID: 3329
			private int current = -1;
		}
	}
}
