using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000073 RID: 115
	internal abstract class CacheBasedCollection : ICollection, IEnumerable
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00024632 File Offset: 0x00022832
		internal string Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0002463A File Offset: 0x0002283A
		internal string SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x00024642 File Offset: 0x00022842
		protected AdomdConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0002464A File Offset: 0x0002284A
		protected virtual bool isPopulated
		{
			get
			{
				return this.objectCache == null || this.objectCache.IsPopulated;
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00024664 File Offset: 0x00022864
		internal CacheBasedCollection(AdomdConnection connection, IObjectCache objectCache)
		{
			this.objectCache = objectCache;
			this.connection = connection;
			if (this.connection != null && this.connection.State == ConnectionState.Open)
			{
				this.catalog = this.connection.CatalogConnectionStringProperty;
				this.sessionId = this.connection.SessionID;
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x000246BD File Offset: 0x000228BD
		internal CacheBasedCollection(AdomdConnection connection)
			: this(connection, null)
		{
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000246C7 File Offset: 0x000228C7
		internal CacheBasedCollection(AdomdConnection connection, InternalObjectType objectType, IMetadataCache metadataCache)
			: this(connection, metadataCache.GetObjectCache(objectType))
		{
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000246D7 File Offset: 0x000228D7
		protected void Initialize(IObjectCache objectCache)
		{
			this.objectCache = objectCache;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000246E0 File Offset: 0x000228E0
		internal virtual void CheckCache()
		{
			this.objectCache.CheckCacheIsValid();
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000246ED File Offset: 0x000228ED
		internal virtual void MarkCacheAsNeedCheckForValidness()
		{
			if (this.isPopulated && this.objectCache != null)
			{
				this.objectCache.MarkNeedCheckForValidness();
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000752 RID: 1874
		public abstract object SyncRoot { get; }

		// Token: 0x06000753 RID: 1875
		public abstract void CopyTo(Array array, int index);

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000754 RID: 1876
		public abstract int Count { get; }

		// Token: 0x06000755 RID: 1877
		public abstract IEnumerator GetEnumerator();

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000756 RID: 1878
		public abstract bool IsSynchronized { get; }

		// Token: 0x06000757 RID: 1879
		protected abstract void PopulateCollection();

		// Token: 0x06000758 RID: 1880 RVA: 0x0002470C File Offset: 0x0002290C
		internal DataRow FindObjectByName(string name, DataRow parentRow, string nameColumn)
		{
			this.PopulateCollection();
			string dataTableFilter = AdomdUtils.GetDataTableFilter(nameColumn, name);
			DataRow[] filteredRows = this.objectCache.GetFilteredRows(parentRow, dataTableFilter);
			if (filteredRows.Length == 0)
			{
				return null;
			}
			return filteredRows[0];
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0002473E File Offset: 0x0002293E
		internal virtual void AbandonCache()
		{
			if (this.objectCache != null)
			{
				this.objectCache.MarkAbandoned();
			}
		}

		// Token: 0x04000518 RID: 1304
		protected IObjectCache objectCache;

		// Token: 0x04000519 RID: 1305
		private string catalog;

		// Token: 0x0400051A RID: 1306
		private string sessionId;

		// Token: 0x0400051B RID: 1307
		private AdomdConnection connection;
	}
}
