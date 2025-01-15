using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000064 RID: 100
	public sealed class AdomdPropertyCollection : MarshalByRefObject, IList, ICollection, IEnumerable
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x000228DC File Offset: 0x00020ADC
		public AdomdPropertyCollection()
		{
			this.collectionInternal = new AdomdPropertyCollectionInternal(this);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000228F0 File Offset: 0x00020AF0
		internal AdomdPropertyCollection(bool readOnly)
			: this()
		{
			this.IsReadOnly = readOnly;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000228FF File Offset: 0x00020AFF
		public AdomdProperty Find(string propertyName)
		{
			return this.Find(propertyName, null);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0002290C File Offset: 0x00020B0C
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

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0002293B File Offset: 0x00020B3B
		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.collectionInternal).IsSynchronized;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00022948 File Offset: 0x00020B48
		public object SyncRoot
		{
			get
			{
				return ((ICollection)this.collectionInternal).SyncRoot;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00022955 File Offset: 0x00020B55
		public int Count
		{
			get
			{
				return this.collectionInternal.Count;
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00022962 File Offset: 0x00020B62
		public void CopyTo(AdomdProperty[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0002296C File Offset: 0x00020B6C
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.collectionInternal).CopyTo(array, index);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0002297B File Offset: 0x00020B7B
		public AdomdPropertyCollection.Enumerator GetEnumerator()
		{
			return new AdomdPropertyCollection.Enumerator(this);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00022983 File Offset: 0x00020B83
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00022990 File Offset: 0x00020B90
		public bool IsFixedSize
		{
			get
			{
				return ((IList)this.collectionInternal).IsFixedSize;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0002299D File Offset: 0x00020B9D
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x000229AA File Offset: 0x00020BAA
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

		// Token: 0x170001B0 RID: 432
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

		// Token: 0x170001B1 RID: 433
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

		// Token: 0x060006A5 RID: 1701 RVA: 0x000229F7 File Offset: 0x00020BF7
		public AdomdProperty Add(AdomdProperty value)
		{
			return (AdomdProperty)this.collectionInternal.Add(value);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00022A0A File Offset: 0x00020C0A
		public AdomdProperty Add(string propertyName, object value)
		{
			return (AdomdProperty)this.collectionInternal.Add(new AdomdProperty(propertyName, value));
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00022A23 File Offset: 0x00020C23
		public AdomdProperty Add(string propertyName, string propertyNamespace, object value)
		{
			return (AdomdProperty)this.collectionInternal.Add(new AdomdProperty(propertyName, propertyNamespace, value));
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00022A3D File Offset: 0x00020C3D
		int IList.Add(object value)
		{
			return ((IList)this.collectionInternal).Add(value);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00022A4B File Offset: 0x00020C4B
		public void Clear()
		{
			this.collectionInternal.Clear();
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00022A58 File Offset: 0x00020C58
		public bool Contains(AdomdProperty value)
		{
			return this.collectionInternal.Contains(value);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00022A66 File Offset: 0x00020C66
		bool IList.Contains(object value)
		{
			return ((IList)this.collectionInternal).Contains(value);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00022A74 File Offset: 0x00020C74
		public int IndexOf(AdomdProperty value)
		{
			return this.collectionInternal.IndexOf(value);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00022A82 File Offset: 0x00020C82
		int IList.IndexOf(object value)
		{
			return ((IList)this.collectionInternal).IndexOf(value);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00022A90 File Offset: 0x00020C90
		public void Insert(int index, AdomdProperty value)
		{
			this.collectionInternal.Insert(index, value);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00022A9F File Offset: 0x00020C9F
		void IList.Insert(int index, object value)
		{
			((IList)this.collectionInternal).Insert(index, value);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00022AAE File Offset: 0x00020CAE
		public void Remove(AdomdProperty value)
		{
			this.collectionInternal.Remove(value);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00022ABC File Offset: 0x00020CBC
		void IList.Remove(object value)
		{
			((IList)this.collectionInternal).Remove(value);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00022ACA File Offset: 0x00020CCA
		public void RemoveAt(int index)
		{
			this.collectionInternal.RemoveAt(index);
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00022AD8 File Offset: 0x00020CD8
		internal AdomdPropertyCollectionInternal InternalCollection
		{
			get
			{
				return this.collectionInternal;
			}
		}

		// Token: 0x0400044E RID: 1102
		private AdomdPropertyCollectionInternal collectionInternal;

		// Token: 0x020001A7 RID: 423
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060012EC RID: 4844 RVA: 0x00043625 File Offset: 0x00041825
			internal Enumerator(AdomdPropertyCollection properties)
			{
				this.properties = properties;
				this.currentIndex = -1;
			}

			// Token: 0x1700069B RID: 1691
			// (get) Token: 0x060012ED RID: 4845 RVA: 0x00043638 File Offset: 0x00041838
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

			// Token: 0x1700069C RID: 1692
			// (get) Token: 0x060012EE RID: 4846 RVA: 0x00043674 File Offset: 0x00041874
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060012EF RID: 4847 RVA: 0x0004367C File Offset: 0x0004187C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.properties.Count;
			}

			// Token: 0x060012F0 RID: 4848 RVA: 0x000436A7 File Offset: 0x000418A7
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CA9 RID: 3241
			private int currentIndex;

			// Token: 0x04000CAA RID: 3242
			private AdomdPropertyCollection properties;
		}
	}
}
