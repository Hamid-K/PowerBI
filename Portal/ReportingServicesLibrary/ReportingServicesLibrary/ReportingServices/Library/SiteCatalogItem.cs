using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000D9 RID: 217
	[CatalogItemType(ItemType.Site)]
	internal class SiteCatalogItem : CatalogItem
	{
		// Token: 0x06000991 RID: 2449 RVA: 0x00004F8E File Offset: 0x0000318E
		internal SiteCatalogItem(RSService service)
			: base(service)
		{
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void ContentLoadSecurityCheck()
		{
			base.ThrowIfNoAccess(CommonOperation.ReadProperties);
		}
	}
}
