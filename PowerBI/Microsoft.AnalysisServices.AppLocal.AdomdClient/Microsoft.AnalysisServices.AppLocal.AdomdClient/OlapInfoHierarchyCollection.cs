using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E5 RID: 229
	public sealed class OlapInfoHierarchyCollection : ICollection, IEnumerable
	{
		// Token: 0x06000C95 RID: 3221 RVA: 0x0002F304 File Offset: 0x0002D504
		internal OlapInfoHierarchyCollection(IDSFDataSet hierarchiesDataSet)
		{
			this.hierarchiesDataSet = hierarchiesDataSet;
		}

		// Token: 0x170004C6 RID: 1222
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

		// Token: 0x170004C7 RID: 1223
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

		// Token: 0x06000C98 RID: 3224 RVA: 0x0002F3A0 File Offset: 0x0002D5A0
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

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0002F445 File Offset: 0x0002D645
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0002F448 File Offset: 0x0002D648
		public object SyncRoot
		{
			get
			{
				return this.hierarchiesDataSet.SyncRoot;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0002F455 File Offset: 0x0002D655
		public int Count
		{
			get
			{
				return this.hierarchiesDataSet.Count;
			}
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0002F462 File Offset: 0x0002D662
		public void CopyTo(OlapInfoHierarchy[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0002F46C File Offset: 0x0002D66C
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0002F4A7 File Offset: 0x0002D6A7
		public OlapInfoHierarchyCollection.Enumerator GetEnumerator()
		{
			return new OlapInfoHierarchyCollection.Enumerator(this);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0002F4AF File Offset: 0x0002D6AF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400080E RID: 2062
		private IDSFDataSet hierarchiesDataSet;

		// Token: 0x0400080F RID: 2063
		private Hashtable namesHash;

		// Token: 0x04000810 RID: 2064
		private const string cachedObjectPropertyName = "$CachedOlapInfoHierarchy";

		// Token: 0x020001C4 RID: 452
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600139B RID: 5019 RVA: 0x00044D30 File Offset: 0x00042F30
			internal Enumerator(OlapInfoHierarchyCollection hierarchies)
			{
				this.hierarchies = hierarchies;
				this.currentIndex = -1;
			}

			// Token: 0x170006D9 RID: 1753
			// (get) Token: 0x0600139C RID: 5020 RVA: 0x00044D40 File Offset: 0x00042F40
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

			// Token: 0x170006DA RID: 1754
			// (get) Token: 0x0600139D RID: 5021 RVA: 0x00044D7C File Offset: 0x00042F7C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600139E RID: 5022 RVA: 0x00044D84 File Offset: 0x00042F84
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.hierarchies.Count;
			}

			// Token: 0x0600139F RID: 5023 RVA: 0x00044DAF File Offset: 0x00042FAF
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CF6 RID: 3318
			private int currentIndex;

			// Token: 0x04000CF7 RID: 3319
			private OlapInfoHierarchyCollection hierarchies;
		}
	}
}
