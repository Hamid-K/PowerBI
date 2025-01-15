using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000AC RID: 172
	public sealed class MemberPropertyCollection : ICollection, IEnumerable
	{
		// Token: 0x060009DA RID: 2522 RVA: 0x00029C9C File Offset: 0x00027E9C
		internal MemberPropertyCollection(DataRow memberAxisRow, Member parentMember, int internallyAddedDimensionPropertyCount)
		{
			if (parentMember == null)
			{
				throw new ArgumentNullException("parentMember");
			}
			if (memberAxisRow == null)
			{
				this.indexMap = new Collection<int>();
			}
			else
			{
				this.indexMap = memberAxisRow.Table.ExtendedProperties["MemberProperties"] as Collection<int>;
			}
			this.memberAxisRow = memberAxisRow;
			this.parentMember = parentMember;
			this.firstMemberPropertyOffset = 5 + internallyAddedDimensionPropertyCount;
			if (this.memberAxisRow == null)
			{
				this.namesHash = new Hashtable();
				return;
			}
			this.namesHash = MemberPropertyCollection.GetOrCreateNamesHashtable(this.memberAxisRow.Table, internallyAddedDimensionPropertyCount);
		}

		// Token: 0x1700033F RID: 831
		public MemberProperty this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				int num = index + this.firstMemberPropertyOffset;
				return new MemberProperty(this.memberAxisRow, this.indexMap[num], this.parentMember);
			}
		}

		// Token: 0x17000340 RID: 832
		public MemberProperty this[string index]
		{
			get
			{
				MemberProperty memberProperty = this.Find(index);
				if (null == memberProperty)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return memberProperty;
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00029DB4 File Offset: 0x00027FB4
		public MemberProperty Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			int propertyColumnIndex = this.GetPropertyColumnIndex(index);
			if (-1 == propertyColumnIndex)
			{
				return null;
			}
			return new MemberProperty(this.memberAxisRow, propertyColumnIndex, this.parentMember);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00029DEF File Offset: 0x00027FEF
		private int GetPropertyColumnIndex(string propName)
		{
			if (this.namesHash[propName] is int)
			{
				return (int)this.namesHash[propName];
			}
			return -1;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00029E18 File Offset: 0x00028018
		private static Hashtable GetNamesHash(DataTable table, int firstPropertyOffSet)
		{
			Hashtable hashtable = new Hashtable();
			if (table == null)
			{
				return hashtable;
			}
			AdomdUtils.FillPropertiesNamesHashTable(table, hashtable, firstPropertyOffSet);
			return hashtable;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00029E3C File Offset: 0x0002803C
		internal static Hashtable GetOrCreateNamesHashtable(DataTable table, int internallyAddedPropCount)
		{
			if (table.ExtendedProperties["MemberPropertiesNamesHash"] is Hashtable)
			{
				return table.ExtendedProperties["MemberPropertiesNamesHash"] as Hashtable;
			}
			Hashtable hashtable = MemberPropertyCollection.GetNamesHash(table, 5 + internallyAddedPropCount);
			table.ExtendedProperties["MemberPropertiesNamesHash"] = hashtable;
			return hashtable;
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00029E92 File Offset: 0x00028092
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00029E95 File Offset: 0x00028095
		public object SyncRoot
		{
			get
			{
				if (this.memberAxisRow == null)
				{
					return this;
				}
				return this.memberAxisRow.Table.Columns.SyncRoot;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00029EB6 File Offset: 0x000280B6
		public int Count
		{
			get
			{
				if (this.memberAxisRow == null)
				{
					return 0;
				}
				return Math.Max(this.indexMap.Count - this.firstMemberPropertyOffset, 0);
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00029EDA File Offset: 0x000280DA
		public void CopyTo(MemberProperty[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00029EE4 File Offset: 0x000280E4
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00029F1F File Offset: 0x0002811F
		public MemberPropertyCollection.Enumerator GetEnumerator()
		{
			return new MemberPropertyCollection.Enumerator(this);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00029F27 File Offset: 0x00028127
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400067B RID: 1659
		private const int STANDARD_PROP_OFFSET = 5;

		// Token: 0x0400067C RID: 1660
		private int firstMemberPropertyOffset;

		// Token: 0x0400067D RID: 1661
		private DataRow memberAxisRow;

		// Token: 0x0400067E RID: 1662
		private Member parentMember;

		// Token: 0x0400067F RID: 1663
		private Collection<int> indexMap;

		// Token: 0x04000680 RID: 1664
		private Hashtable namesHash;

		// Token: 0x04000681 RID: 1665
		private const string namesHashtablePropertyName = "MemberPropertiesNamesHash";

		// Token: 0x020001B5 RID: 437
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001345 RID: 4933 RVA: 0x00044424 File Offset: 0x00042624
			internal Enumerator(MemberPropertyCollection memberProperties)
			{
				this.memberProperties = memberProperties;
				this.currentIndex = -1;
			}

			// Token: 0x170006BB RID: 1723
			// (get) Token: 0x06001346 RID: 4934 RVA: 0x00044434 File Offset: 0x00042634
			public MemberProperty Current
			{
				get
				{
					MemberProperty memberProperty;
					try
					{
						memberProperty = this.memberProperties[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return memberProperty;
				}
			}

			// Token: 0x170006BC RID: 1724
			// (get) Token: 0x06001347 RID: 4935 RVA: 0x00044470 File Offset: 0x00042670
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001348 RID: 4936 RVA: 0x00044478 File Offset: 0x00042678
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.memberProperties.Count;
			}

			// Token: 0x06001349 RID: 4937 RVA: 0x000444A3 File Offset: 0x000426A3
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CD8 RID: 3288
			private int currentIndex;

			// Token: 0x04000CD9 RID: 3289
			private MemberPropertyCollection memberProperties;
		}
	}
}
