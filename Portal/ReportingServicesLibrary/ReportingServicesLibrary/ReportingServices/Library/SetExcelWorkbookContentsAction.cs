using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200003C RID: 60
	internal sealed class SetExcelWorkbookContentsAction : UpdateItemContentAction<SetExcelWorkbookContentsActionParameters, ExcelWorkbookCatalogItem>
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x0000E8F6 File Offset: 0x0000CAF6
		internal SetExcelWorkbookContentsAction(RSService service)
			: base("SetExcelWorkbookContentsAction", service)
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000E904 File Offset: 0x0000CB04
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetPowerBIReportContents, base.ActionParameters.ItemPath, "ExcelWorkbook", null, null, null, null, false, base.ActionParameters.CatalogItemContent.Content, null);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000E95A File Offset: 0x0000CB5A
		protected override void InitAndCheckParams()
		{
			base.Service.ThrowIfResctrictedMimeType(base.ActionParameters.MimeType);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000E972 File Offset: 0x0000CB72
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemPath = parameters.Item;
			base.ActionParameters.CatalogItemContent = new CatalogItemContent(parameters.Content);
			this.PerformActionNow();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000E9A1 File Offset: 0x0000CBA1
		protected override void ValidateAccess(ExcelWorkbookCatalogItem item)
		{
			item.ThrowIfNoAccess(ReportOperation.UpdateReportDefinition);
		}
	}
}
