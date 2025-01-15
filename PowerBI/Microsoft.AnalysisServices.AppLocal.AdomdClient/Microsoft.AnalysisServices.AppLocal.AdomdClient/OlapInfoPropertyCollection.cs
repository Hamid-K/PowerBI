using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E7 RID: 231
	public sealed class OlapInfoPropertyCollection : ICollection, IEnumerable
	{
		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002F504 File Offset: 0x0002D704
		internal OlapInfoPropertyCollection(DataTable propertiesDataTable)
		{
			this.propertiesDataTable = propertiesDataTable;
			this.indexMap = propertiesDataTable.ExtendedProperties["MemberProperties"] as Collection<int>;
			this.propertiesCach = new OlapInfoProperty[this.indexMap.Count];
			for (int i = 0; i < this.propertiesCach.Length; i++)
			{
				this.propertiesCach[i] = null;
			}
			if (this.propertiesDataTable.ExtendedProperties["MemberPropertiesNamesHash"] is Hashtable)
			{
				this.namesHash = this.propertiesDataTable.ExtendedProperties["MemberPropertiesNamesHash"] as Hashtable;
				return;
			}
			this.namesHash = OlapInfoPropertyCollection.GetNamesHash(this.propertiesDataTable);
			this.propertiesDataTable.ExtendedProperties["MemberPropertiesNamesHash"] = this.namesHash;
		}

		// Token: 0x170004CE RID: 1230
		public OlapInfoProperty this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.propertiesCach[index] == null)
				{
					this.propertiesCach[index] = new OlapInfoProperty(this.propertiesDataTable.Columns[this.indexMap[index]]);
				}
				return this.propertiesCach[index];
			}
		}

		// Token: 0x170004CF RID: 1231
		public OlapInfoProperty this[string name]
		{
			get
			{
				OlapInfoProperty olapInfoProperty = this.Find(name);
				if (olapInfoProperty == null)
				{
					throw new ArgumentException(SR.ICollection_ItemWithThisNameDoesNotExistInTheCollection, "name");
				}
				return olapInfoProperty;
			}
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0002F650 File Offset: 0x0002D850
		public OlapInfoProperty Find(string name)
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
			return this[num];
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002F694 File Offset: 0x0002D894
		private static Hashtable GetNamesHash(DataTable table)
		{
			Hashtable hashtable = new Hashtable();
			if (table == null)
			{
				return hashtable;
			}
			AdomdUtils.FillPropertiesNamesHashTable(table, hashtable, 0);
			return hashtable;
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0002F6B5 File Offset: 0x0002D8B5
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0002F6B8 File Offset: 0x0002D8B8
		public object SyncRoot
		{
			get
			{
				return this.propertiesCach.SyncRoot;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0002F6C5 File Offset: 0x0002D8C5
		public int Count
		{
			get
			{
				return this.propertiesCach.Length;
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002F6CF File Offset: 0x0002D8CF
		public void CopyTo(OlapInfoHierarchy[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002F6DC File Offset: 0x0002D8DC
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002F717 File Offset: 0x0002D917
		public OlapInfoPropertyCollection.Enumerator GetEnumerator()
		{
			return new OlapInfoPropertyCollection.Enumerator(this);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002F71F File Offset: 0x0002D91F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000812 RID: 2066
		private DataTable propertiesDataTable;

		// Token: 0x04000813 RID: 2067
		private OlapInfoProperty[] propertiesCach;

		// Token: 0x04000814 RID: 2068
		private Hashtable namesHash;

		// Token: 0x04000815 RID: 2069
		private Collection<int> indexMap;

		// Token: 0x020001C5 RID: 453
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060013A0 RID: 5024 RVA: 0x00044DB8 File Offset: 0x00042FB8
			internal Enumerator(OlapInfoPropertyCollection properties)
			{
				this.properties = properties;
				this.currentIndex = -1;
			}

			// Token: 0x170006DB RID: 1755
			// (get) Token: 0x060013A1 RID: 5025 RVA: 0x00044DC8 File Offset: 0x00042FC8
			public OlapInfoProperty Current
			{
				get
				{
					OlapInfoProperty olapInfoProperty;
					try
					{
						olapInfoProperty = this.properties[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return olapInfoProperty;
				}
			}

			// Token: 0x170006DC RID: 1756
			// (get) Token: 0x060013A2 RID: 5026 RVA: 0x00044E04 File Offset: 0x00043004
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060013A3 RID: 5027 RVA: 0x00044E0C File Offset: 0x0004300C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.properties.Count;
			}

			// Token: 0x060013A4 RID: 5028 RVA: 0x00044E37 File Offset: 0x00043037
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CF8 RID: 3320
			private int currentIndex;

			// Token: 0x04000CF9 RID: 3321
			private OlapInfoPropertyCollection properties;
		}
	}
}
