using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200007A RID: 122
	public sealed class CellPropertyCollection : ICollection, IEnumerable
	{
		// Token: 0x060007A4 RID: 1956 RVA: 0x00025500 File Offset: 0x00023700
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

		// Token: 0x17000211 RID: 529
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

		// Token: 0x17000212 RID: 530
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

		// Token: 0x060007A7 RID: 1959 RVA: 0x00025628 File Offset: 0x00023828
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

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0002567D File Offset: 0x0002387D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00025680 File Offset: 0x00023880
		public object SyncRoot
		{
			get
			{
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0002568D File Offset: 0x0002388D
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

		// Token: 0x060007AB RID: 1963 RVA: 0x000256A4 File Offset: 0x000238A4
		public void CopyTo(CellProperty[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000256B0 File Offset: 0x000238B0
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000256EB File Offset: 0x000238EB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000256F8 File Offset: 0x000238F8
		public CellPropertyCollection.Enumerator GetEnumerator()
		{
			return new CellPropertyCollection.Enumerator(this);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00025700 File Offset: 0x00023900
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

		// Token: 0x04000548 RID: 1352
		private DataColumnCollection internalCollection;

		// Token: 0x04000549 RID: 1353
		private Hashtable namesHash;

		// Token: 0x0400054A RID: 1354
		private DataTable cellTable;

		// Token: 0x0400054B RID: 1355
		private DataRow cellRow;

		// Token: 0x0400054C RID: 1356
		private Cell parentCell;

		// Token: 0x0400054D RID: 1357
		private Collection<int> indexMap;

		// Token: 0x0400054E RID: 1358
		private const string namesHashtablePropertyName = "CellPropertiesNamesHash";

		// Token: 0x020001AC RID: 428
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001311 RID: 4881 RVA: 0x00043EA6 File Offset: 0x000420A6
			internal Enumerator(CellPropertyCollection cellProps)
			{
				this.cellProps = cellProps;
				this.currentIndex = -1;
			}

			// Token: 0x170006A9 RID: 1705
			// (get) Token: 0x06001312 RID: 4882 RVA: 0x00043EB8 File Offset: 0x000420B8
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

			// Token: 0x170006AA RID: 1706
			// (get) Token: 0x06001313 RID: 4883 RVA: 0x00043EF4 File Offset: 0x000420F4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001314 RID: 4884 RVA: 0x00043EFC File Offset: 0x000420FC
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.cellProps.Count;
			}

			// Token: 0x06001315 RID: 4885 RVA: 0x00043F27 File Offset: 0x00042127
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CC6 RID: 3270
			private int currentIndex;

			// Token: 0x04000CC7 RID: 3271
			private CellPropertyCollection cellProps;
		}
	}
}
