using System;
using System.Collections;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000F3 RID: 243
	internal abstract class XmlaPropertyCollectionBase : ICollection, IEnumerable, IList, IDictionary
	{
		// Token: 0x06000D2F RID: 3375 RVA: 0x0002FF3C File Offset: 0x0002E13C
		internal XmlaPropertyCollectionBase()
		{
			XmlaPropertyCollectionBase.HashHelper hashHelper = new XmlaPropertyCollectionBase.HashHelper();
			this.hashStore = new Hashtable(hashHelper);
			this.items = new ArrayList();
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000D30 RID: 3376
		protected abstract Type ItemType { get; }

		// Token: 0x06000D31 RID: 3377
		protected abstract IXmlaProperty CreateBasePropertyObject(IXmlaPropertyKey key, object propertyValue);

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000D32 RID: 3378
		protected abstract object Parent { get; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0002FF6C File Offset: 0x0002E16C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0002FF6F File Offset: 0x0002E16F
		public object SyncRoot
		{
			get
			{
				return this.items.SyncRoot;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0002FF7C File Offset: 0x0002E17C
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0002FF89 File Offset: 0x0002E189
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0002FF98 File Offset: 0x0002E198
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0002FF9B File Offset: 0x0002E19B
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x0002FFA3 File Offset: 0x0002E1A3
		public bool IsReadOnly { get; internal set; }

		// Token: 0x17000517 RID: 1303
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

		// Token: 0x17000518 RID: 1304
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

		// Token: 0x17000519 RID: 1305
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

		// Token: 0x06000D40 RID: 3392 RVA: 0x000300A4 File Offset: 0x0002E2A4
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

		// Token: 0x06000D41 RID: 3393 RVA: 0x000300F8 File Offset: 0x0002E2F8
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

		// Token: 0x06000D42 RID: 3394 RVA: 0x0003012C File Offset: 0x0002E32C
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

		// Token: 0x06000D43 RID: 3395 RVA: 0x00030191 File Offset: 0x0002E391
		public bool Contains(IXmlaProperty property)
		{
			return -1 != this.IndexOf(property);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x000301A0 File Offset: 0x0002E3A0
		public bool Contains(IXmlaPropertyKey key)
		{
			return -1 != this.IndexOf(key);
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x000301AF File Offset: 0x0002E3AF
		bool IList.Contains(object value)
		{
			this.ValidateType(value);
			return this.Contains((IXmlaProperty)value);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000301C4 File Offset: 0x0002E3C4
		public int IndexOf(IXmlaProperty property)
		{
			return this.items.IndexOf(property);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x000301D4 File Offset: 0x0002E3D4
		public int IndexOf(IXmlaPropertyKey key)
		{
			int num = -1;
			if (this.hashStore.ContainsKey(key))
			{
				num = (int)this.hashStore[key];
			}
			return num;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00030204 File Offset: 0x0002E404
		int IList.IndexOf(object value)
		{
			this.ValidateType(value);
			return this.IndexOf((IXmlaProperty)value);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0003021C File Offset: 0x0002E41C
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

		// Token: 0x06000D4A RID: 3402 RVA: 0x000302A9 File Offset: 0x0002E4A9
		void IList.Insert(int index, object value)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			this.ValidateType(value);
			this.Insert(index, (IXmlaProperty)value);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x000302D4 File Offset: 0x0002E4D4
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

		// Token: 0x06000D4C RID: 3404 RVA: 0x00030320 File Offset: 0x0002E520
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

		// Token: 0x06000D4D RID: 3405 RVA: 0x00030363 File Offset: 0x0002E563
		void IList.Remove(object value)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			this.ValidateType(value);
			this.Remove((IXmlaProperty)value);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0003038B File Offset: 0x0002E58B
		public void RemoveAt(int index)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000303AE File Offset: 0x0002E5AE
		void IDictionary.Add(object key, object value)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			this.ValidateKeyType(key);
			this[(IXmlaPropertyKey)key] = value;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x000303D7 File Offset: 0x0002E5D7
		bool IDictionary.Contains(object key)
		{
			this.ValidateKeyType(key);
			return this.Contains((IXmlaPropertyKey)key);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x000303EC File Offset: 0x0002E5EC
		void IDictionary.Remove(object key)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.Collection_IsReadOnly);
			}
			this.ValidateKeyType(key);
			this.Remove((IXmlaPropertyKey)key);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00030414 File Offset: 0x0002E614
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new XmlaPropertyCollectionBase.XmlaPropertyDictionaryEnumerator(this);
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0003041C File Offset: 0x0002E61C
		public ICollection Keys
		{
			get
			{
				return this.hashStore.Keys;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0003042C File Offset: 0x0002E62C
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

		// Token: 0x1700051C RID: 1308
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

		// Token: 0x06000D57 RID: 3415 RVA: 0x000304AE File Offset: 0x0002E6AE
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x000304BC File Offset: 0x0002E6BC
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

		// Token: 0x06000D59 RID: 3417 RVA: 0x0003050C File Offset: 0x0002E70C
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

		// Token: 0x06000D5A RID: 3418 RVA: 0x0003055C File Offset: 0x0002E75C
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

		// Token: 0x06000D5B RID: 3419 RVA: 0x000305D6 File Offset: 0x0002E7D6
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x000305F0 File Offset: 0x0002E7F0
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

		// Token: 0x06000D5D RID: 3421 RVA: 0x00030668 File Offset: 0x0002E868
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

		// Token: 0x06000D5E RID: 3422 RVA: 0x00030696 File Offset: 0x0002E896
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

		// Token: 0x06000D5F RID: 3423 RVA: 0x000306C8 File Offset: 0x0002E8C8
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

		// Token: 0x06000D60 RID: 3424 RVA: 0x00030754 File Offset: 0x0002E954
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

		// Token: 0x04000849 RID: 2121
		private Hashtable hashStore;

		// Token: 0x0400084A RID: 2122
		private ArrayList items;

		// Token: 0x020001C9 RID: 457
		private class HashHelper : IEqualityComparer, IComparer
		{
			// Token: 0x060013A7 RID: 5031 RVA: 0x00044A9C File Offset: 0x00042C9C
			internal HashHelper()
			{
			}

			// Token: 0x060013A8 RID: 5032 RVA: 0x00044AA4 File Offset: 0x00042CA4
			bool IEqualityComparer.Equals(object x, object y)
			{
				return ((IComparer)this).Compare(x, y) == 0;
			}

			// Token: 0x060013A9 RID: 5033 RVA: 0x00044AB4 File Offset: 0x00042CB4
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

			// Token: 0x060013AA RID: 5034 RVA: 0x00044AFC File Offset: 0x00042CFC
			int IEqualityComparer.GetHashCode(object obj)
			{
				IXmlaPropertyKey xmlaPropertyKey = (IXmlaPropertyKey)obj;
				return string.Format(CultureInfo.InvariantCulture, "{0}#{1}", (xmlaPropertyKey.Name != null) ? xmlaPropertyKey.Name.GetHashCode().ToString(CultureInfo.InvariantCulture) : string.Empty, (xmlaPropertyKey.Namespace != null) ? xmlaPropertyKey.Namespace.GetHashCode().ToString(CultureInfo.InvariantCulture) : string.Empty).GetHashCode();
			}
		}

		// Token: 0x020001CA RID: 458
		private class XmlaPropertyDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060013AB RID: 5035 RVA: 0x00044B72 File Offset: 0x00042D72
			internal XmlaPropertyDictionaryEnumerator(XmlaPropertyCollectionBase collection)
			{
				this.collection = collection;
				this.current = -1;
			}

			// Token: 0x170006DD RID: 1757
			// (get) Token: 0x060013AC RID: 5036 RVA: 0x00044B90 File Offset: 0x00042D90
			public DictionaryEntry Entry
			{
				get
				{
					return new DictionaryEntry(((IDictionaryEnumerator)this).Key, ((IDictionaryEnumerator)this).Value);
				}
			}

			// Token: 0x170006DE RID: 1758
			// (get) Token: 0x060013AD RID: 5037 RVA: 0x00044BB0 File Offset: 0x00042DB0
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

			// Token: 0x170006DF RID: 1759
			// (get) Token: 0x060013AE RID: 5038 RVA: 0x00044BEC File Offset: 0x00042DEC
			object IDictionaryEnumerator.Key
			{
				get
				{
					return this.Key;
				}
			}

			// Token: 0x170006E0 RID: 1760
			// (get) Token: 0x060013AF RID: 5039 RVA: 0x00044BF4 File Offset: 0x00042DF4
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

			// Token: 0x170006E1 RID: 1761
			// (get) Token: 0x060013B0 RID: 5040 RVA: 0x00044C34 File Offset: 0x00042E34
			public DictionaryEntry Current
			{
				get
				{
					return ((IDictionaryEnumerator)this).Entry;
				}
			}

			// Token: 0x170006E2 RID: 1762
			// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00044C3C File Offset: 0x00042E3C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060013B2 RID: 5042 RVA: 0x00044C49 File Offset: 0x00042E49
			public void Reset()
			{
				this.current = -1;
			}

			// Token: 0x060013B3 RID: 5043 RVA: 0x00044C54 File Offset: 0x00042E54
			public bool MoveNext()
			{
				int num = this.current + 1;
				this.current = num;
				return num < this.collection.Count;
			}

			// Token: 0x04000CEF RID: 3311
			private XmlaPropertyCollectionBase collection;

			// Token: 0x04000CF0 RID: 3312
			private int current = -1;
		}
	}
}
