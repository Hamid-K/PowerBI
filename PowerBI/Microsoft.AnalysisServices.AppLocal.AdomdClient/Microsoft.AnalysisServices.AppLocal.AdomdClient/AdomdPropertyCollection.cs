using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000064 RID: 100
	public sealed class AdomdPropertyCollection : MarshalByRefObject, IList, ICollection, IEnumerable
	{
		// Token: 0x060006A0 RID: 1696 RVA: 0x00022C0C File Offset: 0x00020E0C
		public AdomdPropertyCollection()
		{
			this.collectionInternal = new AdomdPropertyCollectionInternal(this);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00022C20 File Offset: 0x00020E20
		internal AdomdPropertyCollection(bool readOnly)
			: this()
		{
			this.IsReadOnly = readOnly;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00022C2F File Offset: 0x00020E2F
		public AdomdProperty Find(string propertyName)
		{
			return this.Find(propertyName, null);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00022C3C File Offset: 0x00020E3C
		public AdomdProperty Find(string propertyName, string propertyNamespace)
		{
			XmlaPropertyKey xmlaPropertyKey = new XmlaPropertyKey(propertyName, propertyNamespace);
			int num = this.collectionInternal.IndexOf(xmlaPropertyKey);
			if (num != -1)
			{
				return this[num];
			}
			return null;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00022C6B File Offset: 0x00020E6B
		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.collectionInternal).IsSynchronized;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00022C78 File Offset: 0x00020E78
		public object SyncRoot
		{
			get
			{
				return ((ICollection)this.collectionInternal).SyncRoot;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00022C85 File Offset: 0x00020E85
		public int Count
		{
			get
			{
				return this.collectionInternal.Count;
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00022C92 File Offset: 0x00020E92
		public void CopyTo(AdomdProperty[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00022C9C File Offset: 0x00020E9C
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.collectionInternal).CopyTo(array, index);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00022CAB File Offset: 0x00020EAB
		public AdomdPropertyCollection.Enumerator GetEnumerator()
		{
			return new AdomdPropertyCollection.Enumerator(this);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00022CB3 File Offset: 0x00020EB3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00022CC0 File Offset: 0x00020EC0
		public bool IsFixedSize
		{
			get
			{
				return ((IList)this.collectionInternal).IsFixedSize;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x00022CCD File Offset: 0x00020ECD
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x00022CDA File Offset: 0x00020EDA
		public bool IsReadOnly
		{
			get
			{
				return ((IList)this.collectionInternal).IsReadOnly;
			}
			internal set
			{
				this.collectionInternal.IsReadOnly = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		public AdomdProperty this[int index]
		{
			get
			{
				return (AdomdProperty)this.collectionInternal[index];
			}
			set
			{
				this.collectionInternal[index] = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		object IList.this[int index]
		{
			get
			{
				return ((IList)this.collectionInternal)[index];
			}
			set
			{
				((IList)this.collectionInternal)[index] = value;
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00022D27 File Offset: 0x00020F27
		public AdomdProperty Add(AdomdProperty value)
		{
			return (AdomdProperty)this.collectionInternal.Add(value);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00022D3A File Offset: 0x00020F3A
		public AdomdProperty Add(string propertyName, object value)
		{
			return (AdomdProperty)this.collectionInternal.Add(new AdomdProperty(propertyName, value));
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00022D53 File Offset: 0x00020F53
		public AdomdProperty Add(string propertyName, string propertyNamespace, object value)
		{
			return (AdomdProperty)this.collectionInternal.Add(new AdomdProperty(propertyName, propertyNamespace, value));
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00022D6D File Offset: 0x00020F6D
		int IList.Add(object value)
		{
			return ((IList)this.collectionInternal).Add(value);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00022D7B File Offset: 0x00020F7B
		public void Clear()
		{
			this.collectionInternal.Clear();
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00022D88 File Offset: 0x00020F88
		public bool Contains(AdomdProperty value)
		{
			return this.collectionInternal.Contains(value);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00022D96 File Offset: 0x00020F96
		bool IList.Contains(object value)
		{
			return ((IList)this.collectionInternal).Contains(value);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00022DA4 File Offset: 0x00020FA4
		public int IndexOf(AdomdProperty value)
		{
			return this.collectionInternal.IndexOf(value);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00022DB2 File Offset: 0x00020FB2
		int IList.IndexOf(object value)
		{
			return ((IList)this.collectionInternal).IndexOf(value);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00022DC0 File Offset: 0x00020FC0
		public void Insert(int index, AdomdProperty value)
		{
			this.collectionInternal.Insert(index, value);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00022DCF File Offset: 0x00020FCF
		void IList.Insert(int index, object value)
		{
			((IList)this.collectionInternal).Insert(index, value);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00022DDE File Offset: 0x00020FDE
		public void Remove(AdomdProperty value)
		{
			this.collectionInternal.Remove(value);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00022DEC File Offset: 0x00020FEC
		void IList.Remove(object value)
		{
			((IList)this.collectionInternal).Remove(value);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00022DFA File Offset: 0x00020FFA
		public void RemoveAt(int index)
		{
			this.collectionInternal.RemoveAt(index);
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00022E08 File Offset: 0x00021008
		internal AdomdPropertyCollectionInternal InternalCollection
		{
			get
			{
				return this.collectionInternal;
			}
		}

		// Token: 0x0400045B RID: 1115
		private AdomdPropertyCollectionInternal collectionInternal;

		// Token: 0x020001A7 RID: 423
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060012F9 RID: 4857 RVA: 0x00043B61 File Offset: 0x00041D61
			internal Enumerator(AdomdPropertyCollection properties)
			{
				this.properties = properties;
				this.currentIndex = -1;
			}

			// Token: 0x170006A1 RID: 1697
			// (get) Token: 0x060012FA RID: 4858 RVA: 0x00043B74 File Offset: 0x00041D74
			public AdomdProperty Current
			{
				get
				{
					AdomdProperty adomdProperty;
					try
					{
						adomdProperty = this.properties[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return adomdProperty;
				}
			}

			// Token: 0x170006A2 RID: 1698
			// (get) Token: 0x060012FB RID: 4859 RVA: 0x00043BB0 File Offset: 0x00041DB0
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060012FC RID: 4860 RVA: 0x00043BB8 File Offset: 0x00041DB8
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.properties.Count;
			}

			// Token: 0x060012FD RID: 4861 RVA: 0x00043BE3 File Offset: 0x00041DE3
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CBA RID: 3258
			private int currentIndex;

			// Token: 0x04000CBB RID: 3259
			private AdomdPropertyCollection properties;
		}
	}
}
