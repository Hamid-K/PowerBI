using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000A9 RID: 169
	internal abstract class UpdateItemActionParameters : RSSoapActionParameters
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x000205F7 File Offset: 0x0001E7F7
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x000205FF File Offset: 0x0001E7FF
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

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00020608 File Offset: 0x0001E808
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x00020610 File Offset: 0x0001E810
		public CatalogItem ItemInfo
		{
			get
			{
				return this.m_itemInfo;
			}
			set
			{
				this.m_itemInfo = value;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00020619 File Offset: 0x0001E819
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x04000403 RID: 1027
		private string m_itemPath;

		// Token: 0x04000404 RID: 1028
		private CatalogItem m_itemInfo;
	}
}
