using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000074 RID: 116
	internal abstract class CacheBasedFilteredCollection : CacheBasedCollection
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x00024753 File Offset: 0x00022953
		internal CacheBasedFilteredCollection(AdomdConnection connection, IObjectCache objectCache)
			: base(connection, objectCache)
		{
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0002475D File Offset: 0x0002295D
		internal CacheBasedFilteredCollection(AdomdConnection connection, InternalObjectType objectType, IMetadataCache metadataCache)
			: this(connection, metadataCache.GetObjectCache(objectType))
		{
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0002476D File Offset: 0x0002296D
		internal CacheBasedFilteredCollection(AdomdConnection connection)
			: base(connection, null)
		{
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00024777 File Offset: 0x00022977
		internal void Initialize(DataRow parentRow, string filter)
		{
			this.parentRow = parentRow;
			this.filter = filter;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00024787 File Offset: 0x00022987
		public override void CopyTo(Array array, int index)
		{
			this.PopulateCollection();
			this.internalCollection.CopyTo(array, index);
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0002479C File Offset: 0x0002299C
		public override int Count
		{
			get
			{
				this.PopulateCollection();
				return this.internalCollection.Length;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x000247AC File Offset: 0x000229AC
		public override bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x000247AF File Offset: 0x000229AF
		public override object SyncRoot
		{
			get
			{
				this.PopulateCollection();
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000247C2 File Offset: 0x000229C2
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

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x000247FC File Offset: 0x000229FC
		protected override bool isPopulated
		{
			get
			{
				return base.isPopulated && this.internalCollection != null;
			}
		}

		// Token: 0x0400051C RID: 1308
		protected DataRow[] internalCollection;

		// Token: 0x0400051D RID: 1309
		private DataRow parentRow;

		// Token: 0x0400051E RID: 1310
		private string filter;
	}
}
