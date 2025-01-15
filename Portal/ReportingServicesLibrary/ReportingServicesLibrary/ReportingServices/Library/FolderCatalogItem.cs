using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000D1 RID: 209
	[CatalogItemType(ItemType.Folder)]
	internal class FolderCatalogItem : CatalogItem
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x00004F8E File Offset: 0x0000318E
		internal FolderCatalogItem(RSService service)
			: base(service)
		{
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void ContentLoadSecurityCheck()
		{
			base.ThrowIfNoAccess(CommonOperation.ReadProperties);
		}
	}
}
