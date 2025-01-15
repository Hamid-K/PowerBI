using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000072 RID: 114
	internal struct BaseObjectData
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x000248C8 File Offset: 0x00022AC8
		public AdomdConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x000248D0 File Offset: 0x00022AD0
		public string Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x000248D8 File Offset: 0x00022AD8
		public string SessionID
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x000248E0 File Offset: 0x00022AE0
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x000248E8 File Offset: 0x00022AE8
		public bool IsMetadata
		{
			get
			{
				return this.isMetadata;
			}
			set
			{
				this.isMetadata = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x000248F1 File Offset: 0x00022AF1
		public object AxisData
		{
			get
			{
				return this.axisData;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x000248F9 File Offset: 0x00022AF9
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x00024901 File Offset: 0x00022B01
		public object MetadataData
		{
			get
			{
				return this.metadataData;
			}
			set
			{
				this.metadataData = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0002490A File Offset: 0x00022B0A
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x00024912 File Offset: 0x00022B12
		public IAdomdBaseObject ParentObject
		{
			get
			{
				return this.parentObject;
			}
			set
			{
				this.parentObject = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0002491B File Offset: 0x00022B1B
		public string CubeName
		{
			get
			{
				return this.cubeName;
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00024923 File Offset: 0x00022B23
		internal BaseObjectData(AdomdConnection connection, bool isMetadata, object axisData, object metadataData, IAdomdBaseObject parentObject, string cubeName, string catalog, string sessionId)
		{
			this.connection = connection;
			this.isMetadata = isMetadata;
			this.axisData = axisData;
			this.metadataData = metadataData;
			this.parentObject = parentObject;
			this.cubeName = cubeName;
			this.catalog = catalog;
			this.sessionId = sessionId;
		}

		// Token: 0x0400051D RID: 1309
		private AdomdConnection connection;

		// Token: 0x0400051E RID: 1310
		private bool isMetadata;

		// Token: 0x0400051F RID: 1311
		private object axisData;

		// Token: 0x04000520 RID: 1312
		private object metadataData;

		// Token: 0x04000521 RID: 1313
		private IAdomdBaseObject parentObject;

		// Token: 0x04000522 RID: 1314
		private string cubeName;

		// Token: 0x04000523 RID: 1315
		private string catalog;

		// Token: 0x04000524 RID: 1316
		private string sessionId;
	}
}
