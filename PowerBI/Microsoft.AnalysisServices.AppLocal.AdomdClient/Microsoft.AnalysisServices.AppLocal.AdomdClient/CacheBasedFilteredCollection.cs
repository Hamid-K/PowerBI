using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000074 RID: 116
	internal abstract class CacheBasedFilteredCollection : CacheBasedCollection
	{
		// Token: 0x06000767 RID: 1895 RVA: 0x00024A83 File Offset: 0x00022C83
		internal CacheBasedFilteredCollection(AdomdConnection connection, IObjectCache objectCache)
			: base(connection, objectCache)
		{
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00024A8D File Offset: 0x00022C8D
		internal CacheBasedFilteredCollection(AdomdConnection connection, InternalObjectType objectType, IMetadataCache metadataCache)
			: this(connection, metadataCache.GetObjectCache(objectType))
		{
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00024A9D File Offset: 0x00022C9D
		internal CacheBasedFilteredCollection(AdomdConnection connection)
			: base(connection, null)
		{
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00024AA7 File Offset: 0x00022CA7
		internal void Initialize(DataRow parentRow, string filter)
		{
			this.parentRow = parentRow;
			this.filter = filter;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00024AB7 File Offset: 0x00022CB7
		public override void CopyTo(Array array, int index)
		{
			this.PopulateCollection();
			this.internalCollection.CopyTo(array, index);
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x00024ACC File Offset: 0x00022CCC
		public override int Count
		{
			get
			{
				this.PopulateCollection();
				return this.internalCollection.Length;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00024ADC File Offset: 0x00022CDC
		public override bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x00024ADF File Offset: 0x00022CDF
		public override object SyncRoot
		{
			get
			{
				this.PopulateCollection();
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00024AF2 File Offset: 0x00022CF2
		protected override void PopulateCollection()
		{
			if (!this.isPopulated)
			{
				if (!base.isPopulated)
				{
					this.objectCache.Populate();
				}
				this.internalCollection = this.objectCache.GetFilteredRows(this.parentRow, this.filter);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00024B2C File Offset: 0x00022D2C
		protected override bool isPopulated
		{
			get
			{
				return base.isPopulated && this.internalCollection != null;
			}
		}

		// Token: 0x04000529 RID: 1321
		protected DataRow[] internalCollection;

		// Token: 0x0400052A RID: 1322
		private DataRow parentRow;

		// Token: 0x0400052B RID: 1323
		private string filter;
	}
}
