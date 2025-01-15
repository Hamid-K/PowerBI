using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000067 RID: 103
	public sealed class AdomdRestrictionCollection : MarshalByRefObject, IList, ICollection, IEnumerable
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x00022F39 File Offset: 0x00021139
		public AdomdRestrictionCollection()
		{
			this.collectionInternal = new AdomdRestrictionCollectionInternal(this);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00022F4D File Offset: 0x0002114D
		public AdomdRestriction Find(string propertyName)
		{
			return this.Find(propertyName, null);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00022F58 File Offset: 0x00021158
		public AdomdRestriction Find(string propertyName, string propertyNamespace)
		{
			XmlaPropertyKey xmlaPropertyKey = new XmlaPropertyKey(propertyName, propertyNamespace);
			int num = this.collectionInternal.IndexOf(xmlaPropertyKey);
			if (num != -1)
			{
				return this[num];
			}
			return null;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00022F87 File Offset: 0x00021187
		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.collectionInternal).IsSynchronized;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00022F94 File Offset: 0x00021194
		public object SyncRoot
		{
			get
			{
				return ((ICollection)this.collectionInternal).SyncRoot;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00022FA1 File Offset: 0x000211A1
		public int Count
		{
			get
			{
				return this.collectionInternal.Count;
			}
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00022FAE File Offset: 0x000211AE
		public void CopyTo(AdomdRestriction[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00022FB8 File Offset: 0x000211B8
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.collectionInternal).CopyTo(array, index);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00022FC7 File Offset: 0x000211C7
		public AdomdRestrictionCollection.Enumerator GetEnumerator()
		{
			return new AdomdRestrictionCollection.Enumerator(this);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00022FCF File Offset: 0x000211CF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00022FDC File Offset: 0x000211DC
		public bool IsFixedSize
		{
			get
			{
				return ((IList)this.collectionInternal).IsFixedSize;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00022FE9 File Offset: 0x000211E9
		public bool IsReadOnly
		{
			get
			{
				return ((IList)this.collectionInternal).IsReadOnly;
			}
		}

		// Token: 0x170001C7 RID: 455
		public AdomdRestriction this[int index]
		{
			get
			{
				return (AdomdRestriction)this.collectionInternal[index];
			}
			set
			{
				this.collectionInternal[index] = value;
			}
		}

		// Token: 0x170001C8 RID: 456
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

		// Token: 0x060006E4 RID: 1764 RVA: 0x00023035 File Offset: 0x00021235
		public AdomdRestriction Add(AdomdRestriction value)
		{
			return (AdomdRestriction)this.collectionInternal.Add(value);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00023048 File Offset: 0x00021248
		public AdomdRestriction Add(string propertyName, object value)
		{
			return (AdomdRestriction)this.collectionInternal.Add(new AdomdRestriction(propertyName, value));
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00023061 File Offset: 0x00021261
		public AdomdRestriction Add(string propertyName, string propertyNamespace, object value)
		{
			return (AdomdRestriction)this.collectionInternal.Add(new AdomdRestriction(propertyName, propertyNamespace, value));
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0002307B File Offset: 0x0002127B
		int IList.Add(object value)
		{
			return ((IList)this.collectionInternal).Add(value);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00023089 File Offset: 0x00021289
		public void Clear()
		{
			this.collectionInternal.Clear();
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00023096 File Offset: 0x00021296
		public bool Contains(AdomdRestriction value)
		{
			return this.collectionInternal.Contains(value);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000230A4 File Offset: 0x000212A4
		bool IList.Contains(object value)
		{
			return ((IList)this.collectionInternal).Contains(value);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000230B2 File Offset: 0x000212B2
		public int IndexOf(AdomdRestriction value)
		{
			return this.collectionInternal.IndexOf(value);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000230C0 File Offset: 0x000212C0
		int IList.IndexOf(object value)
		{
			return ((IList)this.collectionInternal).IndexOf(value);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000230CE File Offset: 0x000212CE
		public void Insert(int index, AdomdRestriction value)
		{
			this.collectionInternal.Insert(index, value);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000230DD File Offset: 0x000212DD
		void IList.Insert(int index, object value)
		{
			((IList)this.collectionInternal).Insert(index, value);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x000230EC File Offset: 0x000212EC
		public void Remove(AdomdRestriction value)
		{
			this.collectionInternal.Remove(value);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000230FA File Offset: 0x000212FA
		void IList.Remove(object value)
		{
			((IList)this.collectionInternal).Remove(value);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00023108 File Offset: 0x00021308
		public void RemoveAt(int index)
		{
			this.collectionInternal.RemoveAt(index);
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00023116 File Offset: 0x00021316
		internal AdomdRestrictionCollectionInternal InternalCollection
		{
			get
			{
				return this.collectionInternal;
			}
		}

		// Token: 0x04000461 RID: 1121
		private AdomdRestrictionCollectionInternal collectionInternal;

		// Token: 0x020001A8 RID: 424
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060012FE RID: 4862 RVA: 0x00043BEC File Offset: 0x00041DEC
			internal Enumerator(AdomdRestrictionCollection restrictions)
			{
				this.restrictions = restrictions;
				this.currentIndex = -1;
			}

			// Token: 0x170006A3 RID: 1699
			// (get) Token: 0x060012FF RID: 4863 RVA: 0x00043BFC File Offset: 0x00041DFC
			public AdomdRestriction Current
			{
				get
				{
					AdomdRestriction adomdRestriction;
					try
					{
						adomdRestriction = this.restrictions[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return adomdRestriction;
				}
			}

			// Token: 0x170006A4 RID: 1700
			// (get) Token: 0x06001300 RID: 4864 RVA: 0x00043C38 File Offset: 0x00041E38
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001301 RID: 4865 RVA: 0x00043C40 File Offset: 0x00041E40
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.restrictions.Count;
			}

			// Token: 0x06001302 RID: 4866 RVA: 0x00043C6B File Offset: 0x00041E6B
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CBC RID: 3260
			private int currentIndex;

			// Token: 0x04000CBD RID: 3261
			private AdomdRestrictionCollection restrictions;
		}
	}
}
