using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E7 RID: 231
	public sealed class OlapInfoPropertyCollection : ICollection, IEnumerable
	{
		// Token: 0x06000C97 RID: 3223 RVA: 0x0002F1D4 File Offset: 0x0002D3D4
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

		// Token: 0x170004C8 RID: 1224
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

		// Token: 0x170004C9 RID: 1225
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

		// Token: 0x06000C9A RID: 3226 RVA: 0x0002F320 File Offset: 0x0002D520
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

		// Token: 0x06000C9B RID: 3227 RVA: 0x0002F364 File Offset: 0x0002D564
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

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0002F385 File Offset: 0x0002D585
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0002F388 File Offset: 0x0002D588
		public object SyncRoot
		{
			get
			{
				return this.propertiesCach.SyncRoot;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0002F395 File Offset: 0x0002D595
		public int Count
		{
			get
			{
				return this.propertiesCach.Length;
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0002F39F File Offset: 0x0002D59F
		public void CopyTo(OlapInfoHierarchy[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0002F3AC File Offset: 0x0002D5AC
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002F3E7 File Offset: 0x0002D5E7
		public OlapInfoPropertyCollection.Enumerator GetEnumerator()
		{
			return new OlapInfoPropertyCollection.Enumerator(this);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0002F3EF File Offset: 0x0002D5EF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000805 RID: 2053
		private DataTable propertiesDataTable;

		// Token: 0x04000806 RID: 2054
		private OlapInfoProperty[] propertiesCach;

		// Token: 0x04000807 RID: 2055
		private Hashtable namesHash;

		// Token: 0x04000808 RID: 2056
		private Collection<int> indexMap;

		// Token: 0x020001C5 RID: 453
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001393 RID: 5011 RVA: 0x0004487C File Offset: 0x00042A7C
			internal Enumerator(OlapInfoPropertyCollection properties)
			{
				this.properties = properties;
				this.currentIndex = -1;
			}

			// Token: 0x170006D5 RID: 1749
			// (get) Token: 0x06001394 RID: 5012 RVA: 0x0004488C File Offset: 0x00042A8C
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

			// Token: 0x170006D6 RID: 1750
			// (get) Token: 0x06001395 RID: 5013 RVA: 0x000448C8 File Offset: 0x00042AC8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001396 RID: 5014 RVA: 0x000448D0 File Offset: 0x00042AD0
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.properties.Count;
			}

			// Token: 0x06001397 RID: 5015 RVA: 0x000448FB File Offset: 0x00042AFB
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CE7 RID: 3303
			private int currentIndex;

			// Token: 0x04000CE8 RID: 3304
			private OlapInfoPropertyCollection properties;
		}
	}
}
