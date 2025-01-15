using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200007A RID: 122
	public sealed class CellPropertyCollection : ICollection, IEnumerable
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x000251D0 File Offset: 0x000233D0
		internal CellPropertyCollection(DataTable cellTable, DataRow cellRow, Cell parentCell)
		{
			this.parentCell = parentCell;
			this.cellTable = cellTable;
			this.cellRow = cellRow;
			this.internalCollection = cellTable.Columns;
			this.indexMap = cellTable.ExtendedProperties["MemberProperties"] as Collection<int>;
			if (this.cellTable.ExtendedProperties["CellPropertiesNamesHash"] is Hashtable)
			{
				this.namesHash = this.cellTable.ExtendedProperties["CellPropertiesNamesHash"] as Hashtable;
				return;
			}
			this.namesHash = CellPropertyCollection.GetNamesHash(this.cellTable);
			this.cellTable.ExtendedProperties["CellPropertiesNamesHash"] = this.namesHash;
		}

		// Token: 0x1700020B RID: 523
		public CellProperty this[int index]
		{
			get
			{
				if (index < 0 && index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return new CellProperty(this.cellTable, this.cellRow, this.indexMap[index], this.parentCell);
			}
		}

		// Token: 0x1700020C RID: 524
		public CellProperty this[string propertyName]
		{
			get
			{
				CellProperty cellProperty = this.Find(propertyName);
				if (null == cellProperty)
				{
					throw new ArgumentException(SR.Cellset_propertyIsUnknown(propertyName), "propertyName");
				}
				return cellProperty;
			}
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000252F8 File Offset: 0x000234F8
		public CellProperty Find(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (!this.namesHash.ContainsKey(name))
			{
				return null;
			}
			int num = (int)this.namesHash[name];
			return new CellProperty(this.cellTable, this.cellRow, num, this.parentCell);
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0002534D File Offset: 0x0002354D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00025350 File Offset: 0x00023550
		public object SyncRoot
		{
			get
			{
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0002535D File Offset: 0x0002355D
		public int Count
		{
			get
			{
				if (this.internalCollection != null)
				{
					return this.indexMap.Count;
				}
				return 0;
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00025374 File Offset: 0x00023574
		public void CopyTo(CellProperty[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00025380 File Offset: 0x00023580
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000253BB File Offset: 0x000235BB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x000253C8 File Offset: 0x000235C8
		public CellPropertyCollection.Enumerator GetEnumerator()
		{
			return new CellPropertyCollection.Enumerator(this);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000253D0 File Offset: 0x000235D0
		private static Hashtable GetNamesHash(DataTable table)
		{
			Hashtable hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
			if (table == null)
			{
				return hashtable;
			}
			AdomdUtils.FillNamesHashTable(table, hashtable);
			return hashtable;
		}

		// Token: 0x0400053B RID: 1339
		private DataColumnCollection internalCollection;

		// Token: 0x0400053C RID: 1340
		private Hashtable namesHash;

		// Token: 0x0400053D RID: 1341
		private DataTable cellTable;

		// Token: 0x0400053E RID: 1342
		private DataRow cellRow;

		// Token: 0x0400053F RID: 1343
		private Cell parentCell;

		// Token: 0x04000540 RID: 1344
		private Collection<int> indexMap;

		// Token: 0x04000541 RID: 1345
		private const string namesHashtablePropertyName = "CellPropertiesNamesHash";

		// Token: 0x020001AC RID: 428
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001304 RID: 4868 RVA: 0x0004396A File Offset: 0x00041B6A
			internal Enumerator(CellPropertyCollection cellProps)
			{
				this.cellProps = cellProps;
				this.currentIndex = -1;
			}

			// Token: 0x170006A3 RID: 1699
			// (get) Token: 0x06001305 RID: 4869 RVA: 0x0004397C File Offset: 0x00041B7C
			public CellProperty Current
			{
				get
				{
					CellProperty cellProperty;
					try
					{
						cellProperty = this.cellProps[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return cellProperty;
				}
			}

			// Token: 0x170006A4 RID: 1700
			// (get) Token: 0x06001306 RID: 4870 RVA: 0x000439B8 File Offset: 0x00041BB8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001307 RID: 4871 RVA: 0x000439C0 File Offset: 0x00041BC0
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.cellProps.Count;
			}

			// Token: 0x06001308 RID: 4872 RVA: 0x000439EB File Offset: 0x00041BEB
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CB5 RID: 3253
			private int currentIndex;

			// Token: 0x04000CB6 RID: 3254
			private CellPropertyCollection cellProps;
		}
	}
}
