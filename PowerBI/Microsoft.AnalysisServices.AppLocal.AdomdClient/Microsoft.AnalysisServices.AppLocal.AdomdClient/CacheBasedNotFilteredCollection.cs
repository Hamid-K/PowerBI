using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000075 RID: 117
	internal abstract class CacheBasedNotFilteredCollection : CacheBasedCollection
	{
		// Token: 0x06000771 RID: 1905 RVA: 0x00024B41 File Offset: 0x00022D41
		internal CacheBasedNotFilteredCollection(AdomdConnection connection, InternalObjectType objectType, IMetadataCache metadataCache)
			: base(connection, objectType, metadataCache)
		{
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00024B4C File Offset: 0x00022D4C
		internal CacheBasedNotFilteredCollection(AdomdConnection connection)
			: base(connection)
		{
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00024B55 File Offset: 0x00022D55
		public override void CopyTo(Array array, int index)
		{
			this.PopulateCollection();
			this.internalCollection.CopyTo(array, index);
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x00024B6A File Offset: 0x00022D6A
		public override int Count
		{
			get
			{
				this.PopulateCollection();
				return this.internalCollection.Count;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00024B7D File Offset: 0x00022D7D
		public override bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00024B80 File Offset: 0x00022D80
		public override object SyncRoot
		{
			get
			{
				this.PopulateCollection();
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00024B94 File Offset: 0x00022D94
		protected override void PopulateCollection()
		{
			if (!this.isPopulated)
			{
				if (!base.isPopulated)
				{
					this.objectCache.Populate();
				}
				this.internalCollection = this.objectCache.GetNonFilteredRows();
				this.nestedDataset = this.objectCache.CacheDataSet;
				this.populatedTime = DateTime.Now;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x00024BE9 File Offset: 0x00022DE9
		protected override bool isPopulated
		{
			get
			{
				return base.isPopulated && this.internalCollection != null;
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00024BFE File Offset: 0x00022DFE
		internal void Refresh()
		{
			this.objectCache.Refresh();
			this.PopulateCollection();
		}

		// Token: 0x0400052C RID: 1324
		protected DataRowCollection internalCollection;

		// Token: 0x0400052D RID: 1325
		protected DataSet nestedDataset;

		// Token: 0x0400052E RID: 1326
		protected DateTime populatedTime;
	}
}
