using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200003A RID: 58
	internal sealed class GetExcelWorkbookContentsAction : RSSoapAction<GetExcelWorkbookContentsActionParameters>
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x0000E863 File Offset: 0x0000CA63
		internal GetExcelWorkbookContentsAction(RSService service)
			: base("GetExcelWorkbookContentsAction", service)
		{
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000E874 File Offset: 0x0000CA74
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "ExcelWorkbook");
			ExcelWorkbookCatalogItem excelWorkbookCatalogItem = (ExcelWorkbookCatalogItem)base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.ExcelWorkbook);
			excelWorkbookCatalogItem.LoadContent();
			base.ActionParameters.MimeType = "application/octet-stream";
			base.ActionParameters.Content = excelWorkbookCatalogItem.Content;
		}
	}
}
