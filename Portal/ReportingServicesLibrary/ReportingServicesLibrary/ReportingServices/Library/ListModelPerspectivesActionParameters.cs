using System;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200014E RID: 334
	internal sealed class ListModelPerspectivesActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0002F94F File Offset: 0x0002DB4F
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x0002F957 File Offset: 0x0002DB57
		public string ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0002F960 File Offset: 0x0002DB60
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x0002F968 File Offset: 0x0002DB68
		public ModelCatalogItem[] ModelsWithPerspectives
		{
			get
			{
				return this.m_modelsWithPerspectives;
			}
			set
			{
				this.m_modelsWithPerspectives = value;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0002F971 File Offset: 0x0002DB71
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal override void Validate()
		{
		}

		// Token: 0x0400052F RID: 1327
		private string m_itemPath;

		// Token: 0x04000530 RID: 1328
		private ModelCatalogItem[] m_modelsWithPerspectives;
	}
}
