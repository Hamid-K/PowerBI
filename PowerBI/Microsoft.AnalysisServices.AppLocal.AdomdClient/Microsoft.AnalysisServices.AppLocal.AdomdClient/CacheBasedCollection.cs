using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000073 RID: 115
	internal abstract class CacheBasedCollection : ICollection, IEnumerable
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00024962 File Offset: 0x00022B62
		internal string Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0002496A File Offset: 0x00022B6A
		internal string SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x00024972 File Offset: 0x00022B72
		protected AdomdConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0002497A File Offset: 0x00022B7A
		protected virtual bool isPopulated
		{
			get
			{
				return this.objectCache == null || this.objectCache.IsPopulated;
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00024994 File Offset: 0x00022B94
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

		// Token: 0x0600075A RID: 1882 RVA: 0x000249ED File Offset: 0x00022BED
		internal CacheBasedCollection(AdomdConnection connection)
			: this(connection, null)
		{
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x000249F7 File Offset: 0x00022BF7
		internal CacheBasedCollection(AdomdConnection connection, InternalObjectType objectType, IMetadataCache metadataCache)
			: this(connection, metadataCache.GetObjectCache(objectType))
		{
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00024A07 File Offset: 0x00022C07
		protected void Initialize(IObjectCache objectCache)
		{
			this.objectCache = objectCache;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00024A10 File Offset: 0x00022C10
		internal virtual void CheckCache()
		{
			this.objectCache.CheckCacheIsValid();
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00024A1D File Offset: 0x00022C1D
		internal virtual void MarkCacheAsNeedCheckForValidness()
		{
			if (this.isPopulated && this.objectCache != null)
			{
				this.objectCache.MarkNeedCheckForValidness();
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600075F RID: 1887
		public abstract object SyncRoot { get; }

		// Token: 0x06000760 RID: 1888
		public abstract void CopyTo(Array array, int index);

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000761 RID: 1889
		public abstract int Count { get; }

		// Token: 0x06000762 RID: 1890
		public abstract IEnumerator GetEnumerator();

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000763 RID: 1891
		public abstract bool IsSynchronized { get; }

		// Token: 0x06000764 RID: 1892
		protected abstract void PopulateCollection();

		// Token: 0x06000765 RID: 1893 RVA: 0x00024A3C File Offset: 0x00022C3C
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

		// Token: 0x06000766 RID: 1894 RVA: 0x00024A6E File Offset: 0x00022C6E
		internal virtual void AbandonCache()
		{
			if (this.objectCache != null)
			{
				this.objectCache.MarkAbandoned();
			}
		}

		// Token: 0x04000525 RID: 1317
		protected IObjectCache objectCache;

		// Token: 0x04000526 RID: 1318
		private string catalog;

		// Token: 0x04000527 RID: 1319
		private string sessionId;

		// Token: 0x04000528 RID: 1320
		private AdomdConnection connection;
	}
}
