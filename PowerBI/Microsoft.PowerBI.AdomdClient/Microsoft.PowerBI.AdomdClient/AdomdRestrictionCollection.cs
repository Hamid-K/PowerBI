using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000067 RID: 103
	public sealed class AdomdRestrictionCollection : MarshalByRefObject, IList, ICollection, IEnumerable
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x00022C09 File Offset: 0x00020E09
		public AdomdRestrictionCollection()
		{
			this.collectionInternal = new AdomdRestrictionCollectionInternal(this);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00022C1D File Offset: 0x00020E1D
		public AdomdRestriction Find(string propertyName)
		{
			return this.Find(propertyName, null);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00022C28 File Offset: 0x00020E28
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

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00022C57 File Offset: 0x00020E57
		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.collectionInternal).IsSynchronized;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x00022C64 File Offset: 0x00020E64
		public object SyncRoot
		{
			get
			{
				return ((ICollection)this.collectionInternal).SyncRoot;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00022C71 File Offset: 0x00020E71
		public int Count
		{
			get
			{
				return this.collectionInternal.Count;
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00022C7E File Offset: 0x00020E7E
		public void CopyTo(AdomdRestriction[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00022C88 File Offset: 0x00020E88
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.collectionInternal).CopyTo(array, index);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00022C97 File Offset: 0x00020E97
		public AdomdRestrictionCollection.Enumerator GetEnumerator()
		{
			return new AdomdRestrictionCollection.Enumerator(this);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00022C9F File Offset: 0x00020E9F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00022CAC File Offset: 0x00020EAC
		public bool IsFixedSize
		{
			get
			{
				return ((IList)this.collectionInternal).IsFixedSize;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00022CB9 File Offset: 0x00020EB9
		public bool IsReadOnly
		{
			get
			{
				return ((IList)this.collectionInternal).IsReadOnly;
			}
		}

		// Token: 0x170001C1 RID: 449
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

		// Token: 0x170001C2 RID: 450
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

		// Token: 0x060006D7 RID: 1751 RVA: 0x00022D05 File Offset: 0x00020F05
		public AdomdRestriction Add(AdomdRestriction value)
		{
			return (AdomdRestriction)this.collectionInternal.Add(value);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00022D18 File Offset: 0x00020F18
		public AdomdRestriction Add(string propertyName, object value)
		{
			return (AdomdRestriction)this.collectionInternal.Add(new AdomdRestriction(propertyName, value));
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00022D31 File Offset: 0x00020F31
		public AdomdRestriction Add(string propertyName, string propertyNamespace, object value)
		{
			return (AdomdRestriction)this.collectionInternal.Add(new AdomdRestriction(propertyName, propertyNamespace, value));
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00022D4B File Offset: 0x00020F4B
		int IList.Add(object value)
		{
			return ((IList)this.collectionInternal).Add(value);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00022D59 File Offset: 0x00020F59
		public void Clear()
		{
			this.collectionInternal.Clear();
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00022D66 File Offset: 0x00020F66
		public bool Contains(AdomdRestriction value)
		{
			return this.collectionInternal.Contains(value);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00022D74 File Offset: 0x00020F74
		bool IList.Contains(object value)
		{
			return ((IList)this.collectionInternal).Contains(value);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00022D82 File Offset: 0x00020F82
		public int IndexOf(AdomdRestriction value)
		{
			return this.collectionInternal.IndexOf(value);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00022D90 File Offset: 0x00020F90
		int IList.IndexOf(object value)
		{
			return ((IList)this.collectionInternal).IndexOf(value);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00022D9E File Offset: 0x00020F9E
		public void Insert(int index, AdomdRestriction value)
		{
			this.collectionInternal.Insert(index, value);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00022DAD File Offset: 0x00020FAD
		void IList.Insert(int index, object value)
		{
			((IList)this.collectionInternal).Insert(index, value);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00022DBC File Offset: 0x00020FBC
		public void Remove(AdomdRestriction value)
		{
			this.collectionInternal.Remove(value);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00022DCA File Offset: 0x00020FCA
		void IList.Remove(object value)
		{
			((IList)this.collectionInternal).Remove(value);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00022DD8 File Offset: 0x00020FD8
		public void RemoveAt(int index)
		{
			this.collectionInternal.RemoveAt(index);
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00022DE6 File Offset: 0x00020FE6
		internal AdomdRestrictionCollectionInternal InternalCollection
		{
			get
			{
				return this.collectionInternal;
			}
		}

		// Token: 0x04000454 RID: 1108
		private AdomdRestrictionCollectionInternal collectionInternal;

		// Token: 0x020001A8 RID: 424
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060012F1 RID: 4849 RVA: 0x000436B0 File Offset: 0x000418B0
			internal Enumerator(AdomdRestrictionCollection restrictions)
			{
				this.restrictions = restrictions;
				this.currentIndex = -1;
			}

			// Token: 0x1700069D RID: 1693
			// (get) Token: 0x060012F2 RID: 4850 RVA: 0x000436C0 File Offset: 0x000418C0
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

			// Token: 0x1700069E RID: 1694
			// (get) Token: 0x060012F3 RID: 4851 RVA: 0x000436FC File Offset: 0x000418FC
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060012F4 RID: 4852 RVA: 0x00043704 File Offset: 0x00041904
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.restrictions.Count;
			}

			// Token: 0x060012F5 RID: 4853 RVA: 0x0004372F File Offset: 0x0004192F
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CAB RID: 3243
			private int currentIndex;

			// Token: 0x04000CAC RID: 3244
			private AdomdRestrictionCollection restrictions;
		}
	}
}
