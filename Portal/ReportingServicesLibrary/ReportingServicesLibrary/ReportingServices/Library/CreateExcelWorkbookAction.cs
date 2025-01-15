using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000038 RID: 56
	internal sealed class CreateExcelWorkbookAction : CreateItemWithContentAction<CreateExcelWorkbookActionParameters, ExcelWorkbookCatalogItem>
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000E6D1 File Offset: 0x0000C8D1
		internal CreateExcelWorkbookAction(RSService service)
			: base("CreateExcelWorkbookAction", service)
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000E6E0 File Offset: 0x0000C8E0
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.CreateExcelWorkbook, base.ActionParameters.ItemName, "ExcelWorkbook", base.ActionParameters.ParentPath, "Parent", null, null, base.ActionParameters.Overwrite, base.ActionParameters.CatalogItemContent.Content, Property.ThisArrayToXml(base.ActionParameters.Properties));
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000E760 File Offset: 0x0000C960
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemName = parameters.Item;
			base.ActionParameters.ParentPath = parameters.Parent;
			base.ActionParameters.Overwrite = parameters.BoolParam;
			base.ActionParameters.CatalogItemContent = new CatalogItemContent(parameters.Content);
			base.ActionParameters.Properties = Property.XmlToThisArray(parameters.Properties);
			this.PerformActionNow();
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool IsUpdateSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000E7D2 File Offset: 0x0000C9D2
		protected override void InitAndCheckParams()
		{
			base.Service.ThrowIfResctrictedMimeType(base.ActionParameters.MimeType);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000E7EA File Offset: 0x0000C9EA
		protected override void UpdateExistingItem(ExcelWorkbookCatalogItem item)
		{
			SetExcelWorkbookContentsAction setExcelWorkbookContentsAction = base.Service.SetExcelWorkbookContentsAction;
			setExcelWorkbookContentsAction.ActionParameters.CatalogItemContent = base.ActionParameters.CatalogItemContent;
			setExcelWorkbookContentsAction.Update(item);
		}
	}
}
