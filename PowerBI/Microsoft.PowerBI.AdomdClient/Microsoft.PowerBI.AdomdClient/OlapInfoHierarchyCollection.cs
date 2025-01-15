using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E5 RID: 229
	public sealed class OlapInfoHierarchyCollection : ICollection, IEnumerable
	{
		// Token: 0x06000C88 RID: 3208 RVA: 0x0002EFD4 File Offset: 0x0002D1D4
		internal OlapInfoHierarchyCollection(IDSFDataSet hierarchiesDataSet)
		{
			this.hierarchiesDataSet = hierarchiesDataSet;
		}

		// Token: 0x170004C0 RID: 1216
		public OlapInfoHierarchy this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataTable dataTable = this.hierarchiesDataSet[index];
				if (dataTable.ExtendedProperties["$CachedOlapInfoHierarchy"] == null)
				{
					dataTable.ExtendedProperties["$CachedOlapInfoHierarchy"] = new OlapInfoHierarchy(dataTable);
				}
				return (OlapInfoHierarchy)dataTable.ExtendedProperties["$CachedOlapInfoHierarchy"];
			}
		}

		// Token: 0x170004C1 RID: 1217
		public OlapInfoHierarchy this[string name]
		{
			get
			{
				OlapInfoHierarchy olapInfoHierarchy = this.Find(name);
				if (olapInfoHierarchy == null)
				{
					throw new ArgumentException(SR.ICollection_ItemWithThisNameDoesNotExistInTheCollection, "name");
				}
				return olapInfoHierarchy;
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0002F070 File Offset: 0x0002D270
		public OlapInfoHierarchy Find(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.namesHash == null)
			{
				this.namesHash = new Hashtable(this.Count);
				for (int i = 0; i < this.hierarchiesDataSet.Count; i++)
				{
					string tableName = this.hierarchiesDataSet[i].TableName;
					if (this.namesHash[tableName] == null)
					{
						this.namesHash[tableName] = i;
					}
				}
			}
			if (!this.namesHash.ContainsKey(name))
			{
				return null;
			}
			int num = (int)this.namesHash[name];
			return this[num];
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0002F115 File Offset: 0x0002D315
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0002F118 File Offset: 0x0002D318
		public object SyncRoot
		{
			get
			{
				return this.hierarchiesDataSet.SyncRoot;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0002F125 File Offset: 0x0002D325
		public int Count
		{
			get
			{
				return this.hierarchiesDataSet.Count;
			}
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002F132 File Offset: 0x0002D332
		public void CopyTo(OlapInfoHierarchy[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002F13C File Offset: 0x0002D33C
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002F177 File Offset: 0x0002D377
		public OlapInfoHierarchyCollection.Enumerator GetEnumerator()
		{
			return new OlapInfoHierarchyCollection.Enumerator(this);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002F17F File Offset: 0x0002D37F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000801 RID: 2049
		private IDSFDataSet hierarchiesDataSet;

		// Token: 0x04000802 RID: 2050
		private Hashtable namesHash;

		// Token: 0x04000803 RID: 2051
		private const string cachedObjectPropertyName = "$CachedOlapInfoHierarchy";

		// Token: 0x020001C4 RID: 452
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600138E RID: 5006 RVA: 0x000447F4 File Offset: 0x000429F4
			internal Enumerator(OlapInfoHierarchyCollection hierarchies)
			{
				this.hierarchies = hierarchies;
				this.currentIndex = -1;
			}

			// Token: 0x170006D3 RID: 1747
			// (get) Token: 0x0600138F RID: 5007 RVA: 0x00044804 File Offset: 0x00042A04
			public OlapInfoHierarchy Current
			{
				get
				{
					OlapInfoHierarchy olapInfoHierarchy;
					try
					{
						olapInfoHierarchy = this.hierarchies[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return olapInfoHierarchy;
				}
			}

			// Token: 0x170006D4 RID: 1748
			// (get) Token: 0x06001390 RID: 5008 RVA: 0x00044840 File Offset: 0x00042A40
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001391 RID: 5009 RVA: 0x00044848 File Offset: 0x00042A48
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.hierarchies.Count;
			}

			// Token: 0x06001392 RID: 5010 RVA: 0x00044873 File Offset: 0x00042A73
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CE5 RID: 3301
			private int currentIndex;

			// Token: 0x04000CE6 RID: 3302
			private OlapInfoHierarchyCollection hierarchies;
		}
	}
}
