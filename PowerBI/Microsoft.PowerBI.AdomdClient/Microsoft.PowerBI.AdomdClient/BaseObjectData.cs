using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000072 RID: 114
	internal struct BaseObjectData
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00024598 File Offset: 0x00022798
		public AdomdConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x000245A0 File Offset: 0x000227A0
		public string Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x000245A8 File Offset: 0x000227A8
		public string SessionID
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x000245B0 File Offset: 0x000227B0
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x000245B8 File Offset: 0x000227B8
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

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x000245C1 File Offset: 0x000227C1
		public object AxisData
		{
			get
			{
				return this.axisData;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x000245C9 File Offset: 0x000227C9
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x000245D1 File Offset: 0x000227D1
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

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x000245DA File Offset: 0x000227DA
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x000245E2 File Offset: 0x000227E2
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

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x000245EB File Offset: 0x000227EB
		public string CubeName
		{
			get
			{
				return this.cubeName;
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000245F3 File Offset: 0x000227F3
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

		// Token: 0x04000510 RID: 1296
		private AdomdConnection connection;

		// Token: 0x04000511 RID: 1297
		private bool isMetadata;

		// Token: 0x04000512 RID: 1298
		private object axisData;

		// Token: 0x04000513 RID: 1299
		private object metadataData;

		// Token: 0x04000514 RID: 1300
		private IAdomdBaseObject parentObject;

		// Token: 0x04000515 RID: 1301
		private string cubeName;

		// Token: 0x04000516 RID: 1302
		private string catalog;

		// Token: 0x04000517 RID: 1303
		private string sessionId;
	}
}
