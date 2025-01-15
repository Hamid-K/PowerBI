using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000176 RID: 374
	internal sealed class ModelCacheInfo
	{
		// Token: 0x06000DB4 RID: 3508 RVA: 0x0003231D File Offset: 0x0003051D
		internal ModelCacheInfo(IRenderEditSession session, string itemPath, string dataSourceName, string modelMetadataVersion, string modelPerspectiveName, bool isExternal, bool isMultiDimensional)
		{
			this.m_session = session;
			this.m_itemPath = itemPath;
			this.m_dataSourceName = dataSourceName;
			this.m_modelMetadataVersion = modelMetadataVersion;
			this.m_modelPerspectiveName = modelPerspectiveName;
			this.m_isExternalModel = isExternal;
			this.m_isMultiDimensional = isMultiDimensional;
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x0003235A File Offset: 0x0003055A
		internal IRenderEditSession Session
		{
			get
			{
				return this.m_session;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00032362 File Offset: 0x00030562
		internal string ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0003236A File Offset: 0x0003056A
		internal string DataSourceName
		{
			get
			{
				return this.m_dataSourceName;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x00032372 File Offset: 0x00030572
		internal string ModelMetadataVersion
		{
			get
			{
				return this.m_modelMetadataVersion;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0003237A File Offset: 0x0003057A
		internal string ModelPerspectiveName
		{
			get
			{
				return this.m_modelPerspectiveName;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00032382 File Offset: 0x00030582
		internal bool IsExternalModel
		{
			get
			{
				return this.m_isExternalModel;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0003238A File Offset: 0x0003058A
		internal bool IsMultiDimensional
		{
			get
			{
				return this.m_isMultiDimensional;
			}
		}

		// Token: 0x040005A6 RID: 1446
		private readonly IRenderEditSession m_session;

		// Token: 0x040005A7 RID: 1447
		private readonly string m_itemPath;

		// Token: 0x040005A8 RID: 1448
		private readonly string m_dataSourceName;

		// Token: 0x040005A9 RID: 1449
		private readonly string m_modelMetadataVersion;

		// Token: 0x040005AA RID: 1450
		private readonly string m_modelPerspectiveName;

		// Token: 0x040005AB RID: 1451
		private readonly bool m_isExternalModel;

		// Token: 0x040005AC RID: 1452
		private readonly bool m_isMultiDimensional;
	}
}
