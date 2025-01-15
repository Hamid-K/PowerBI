using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x02000024 RID: 36
	internal sealed class SdStrAndType
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00004288 File Offset: 0x00002488
		internal SdStrAndType(SecurityItemType itemType, string sd)
		{
			switch (itemType)
			{
			case SecurityItemType.Catalog:
				this.m_ItemType = "Catalog:";
				break;
			case SecurityItemType.Folder:
				this.m_ItemType = "Folder:";
				break;
			case SecurityItemType.Report:
				this.m_ItemType = "Report:";
				break;
			case SecurityItemType.Resource:
				this.m_ItemType = "Resource:";
				break;
			case SecurityItemType.Datasource:
				this.m_ItemType = "Datasource:";
				break;
			case SecurityItemType.Model:
				this.m_ItemType = "Model:";
				break;
			case SecurityItemType.ModelItem:
				this.m_ItemType = "ModelItem:";
				break;
			default:
				throw new InternalCatalogException("Invalid security descriptor type.");
			}
			this.m_SecDesc = sd;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000432E File Offset: 0x0000252E
		public override string ToString()
		{
			return this.m_ItemType + this.m_SecDesc;
		}

		// Token: 0x04000101 RID: 257
		internal string m_ItemType;

		// Token: 0x04000102 RID: 258
		internal string m_SecDesc;
	}
}
