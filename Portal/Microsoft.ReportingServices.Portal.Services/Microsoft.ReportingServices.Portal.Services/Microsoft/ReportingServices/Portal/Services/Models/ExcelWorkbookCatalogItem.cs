using System;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;
using Microsoft.ReportingServices.Portal.Interfaces.Models;

namespace Microsoft.ReportingServices.Portal.Services.Models
{
	// Token: 0x02000053 RID: 83
	internal sealed class ExcelWorkbookCatalogItem : CatalogItem, IExcelWorkbookCatalogItem, ICatalogItem
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x00012857 File Offset: 0x00010A57
		public ExcelWorkbookCatalogItem()
			: base(CatalogItemType.ExcelWorkbook)
		{
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00012861 File Offset: 0x00010A61
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00012869 File Offset: 0x00010A69
		public byte[] Content { get; set; }
	}
}
