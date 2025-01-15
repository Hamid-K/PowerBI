using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000075 RID: 117
	internal abstract class CacheBasedNotFilteredCollection : CacheBasedCollection
	{
		// Token: 0x06000764 RID: 1892 RVA: 0x00024811 File Offset: 0x00022A11
		internal CacheBasedNotFilteredCollection(AdomdConnection connection, InternalObjectType objectType, IMetadataCache metadataCache)
			: base(connection, objectType, metadataCache)
		{
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0002481C File Offset: 0x00022A1C
		internal CacheBasedNotFilteredCollection(AdomdConnection connection)
			: base(connection)
		{
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00024825 File Offset: 0x00022A25
		public override void CopyTo(Array array, int index)
		{
			this.PopulateCollection();
			this.internalCollection.CopyTo(array, index);
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0002483A File Offset: 0x00022A3A
		public override int Count
		{
			get
			{
				this.PopulateCollection();
				return this.internalCollection.Count;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0002484D File Offset: 0x00022A4D
		public override bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x00024850 File Offset: 0x00022A50
		public override object SyncRoot
		{
			get
			{
				this.PopulateCollection();
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00024864 File Offset: 0x00022A64
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

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x000248B9 File Offset: 0x00022AB9
		protected override bool isPopulated
		{
			get
			{
				return base.isPopulated && this.internalCollection != null;
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000248CE File Offset: 0x00022ACE
		internal void Refresh()
		{
			this.objectCache.Refresh();
			this.PopulateCollection();
		}

		// Token: 0x0400051F RID: 1311
		protected DataRowCollection internalCollection;

		// Token: 0x04000520 RID: 1312
		protected DataSet nestedDataset;

		// Token: 0x04000521 RID: 1313
		protected DateTime populatedTime;
	}
}
