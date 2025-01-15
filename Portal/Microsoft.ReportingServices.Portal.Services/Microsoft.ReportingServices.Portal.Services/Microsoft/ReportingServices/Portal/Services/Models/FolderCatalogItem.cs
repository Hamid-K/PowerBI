using System;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;
using Microsoft.ReportingServices.Portal.Interfaces.Models;

namespace Microsoft.ReportingServices.Portal.Services.Models
{
	// Token: 0x02000059 RID: 89
	internal sealed class FolderCatalogItem : CatalogItem, IFolderCatalogItem, ICatalogItem
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x00012991 File Offset: 0x00010B91
		public FolderCatalogItem()
			: base(CatalogItemType.Folder)
		{
		}
	}
}
