using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000BF RID: 191
	internal sealed class ListDependentItemsActionParameters : RSSoapActionParameters
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00021C39 File Offset: 0x0001FE39
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x00021C41 File Offset: 0x0001FE41
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

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00021C4A File Offset: 0x0001FE4A
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x00021C52 File Offset: 0x0001FE52
		public CatalogItemList DependentItems
		{
			get
			{
				return this.m_dependentItems;
			}
			set
			{
				this.m_dependentItems = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x00021C5B File Offset: 0x0001FE5B
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x00021C63 File Offset: 0x0001FE63
		public bool DirectDependentItemsOnly
		{
			get
			{
				return this.m_directDependentItemsOnly;
			}
			set
			{
				this.m_directDependentItemsOnly = value;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00021C6C File Offset: 0x0001FE6C
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00021C74 File Offset: 0x0001FE74
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Item");
			}
		}

		// Token: 0x04000424 RID: 1060
		private string m_itemPath;

		// Token: 0x04000425 RID: 1061
		private CatalogItemList m_dependentItems;

		// Token: 0x04000426 RID: 1062
		private bool m_directDependentItemsOnly = true;
	}
}
