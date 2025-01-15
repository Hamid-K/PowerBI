using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000AC RID: 172
	public sealed class MemberPropertyCollection : ICollection, IEnumerable
	{
		// Token: 0x060009CD RID: 2509 RVA: 0x0002996C File Offset: 0x00027B6C
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

		// Token: 0x17000339 RID: 825
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

		// Token: 0x1700033A RID: 826
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

		// Token: 0x060009D0 RID: 2512 RVA: 0x00029A84 File Offset: 0x00027C84
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

		// Token: 0x060009D1 RID: 2513 RVA: 0x00029ABF File Offset: 0x00027CBF
		private int GetPropertyColumnIndex(string propName)
		{
			if (this.namesHash[propName] is int)
			{
				return (int)this.namesHash[propName];
			}
			return -1;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00029AE8 File Offset: 0x00027CE8
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

		// Token: 0x060009D3 RID: 2515 RVA: 0x00029B0C File Offset: 0x00027D0C
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

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00029B62 File Offset: 0x00027D62
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00029B65 File Offset: 0x00027D65
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

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00029B86 File Offset: 0x00027D86
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

		// Token: 0x060009D7 RID: 2519 RVA: 0x00029BAA File Offset: 0x00027DAA
		public void CopyTo(MemberProperty[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00029BB4 File Offset: 0x00027DB4
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00029BEF File Offset: 0x00027DEF
		public MemberPropertyCollection.Enumerator GetEnumerator()
		{
			return new MemberPropertyCollection.Enumerator(this);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00029BF7 File Offset: 0x00027DF7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400066E RID: 1646
		private const int STANDARD_PROP_OFFSET = 5;

		// Token: 0x0400066F RID: 1647
		private int firstMemberPropertyOffset;

		// Token: 0x04000670 RID: 1648
		private DataRow memberAxisRow;

		// Token: 0x04000671 RID: 1649
		private Member parentMember;

		// Token: 0x04000672 RID: 1650
		private Collection<int> indexMap;

		// Token: 0x04000673 RID: 1651
		private Hashtable namesHash;

		// Token: 0x04000674 RID: 1652
		private const string namesHashtablePropertyName = "MemberPropertiesNamesHash";

		// Token: 0x020001B5 RID: 437
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001338 RID: 4920 RVA: 0x00043EE8 File Offset: 0x000420E8
			internal Enumerator(MemberPropertyCollection memberProperties)
			{
				this.memberProperties = memberProperties;
				this.currentIndex = -1;
			}

			// Token: 0x170006B5 RID: 1717
			// (get) Token: 0x06001339 RID: 4921 RVA: 0x00043EF8 File Offset: 0x000420F8
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

			// Token: 0x170006B6 RID: 1718
			// (get) Token: 0x0600133A RID: 4922 RVA: 0x00043F34 File Offset: 0x00042134
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600133B RID: 4923 RVA: 0x00043F3C File Offset: 0x0004213C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.memberProperties.Count;
			}

			// Token: 0x0600133C RID: 4924 RVA: 0x00043F67 File Offset: 0x00042167
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CC7 RID: 3271
			private int currentIndex;

			// Token: 0x04000CC8 RID: 3272
			private MemberPropertyCollection memberProperties;
		}
	}
}
