using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000163 RID: 355
	internal sealed class SetPowerBIReportContentsAction : UpdateItemContentAction<UpdatePowerBIReportActionParameters, PowerBIReportCatalogItem>
	{
		// Token: 0x06000D5C RID: 3420 RVA: 0x00030B94 File Offset: 0x0002ED94
		internal SetPowerBIReportContentsAction(RSService service)
			: base("SetPowerBIReportContentsAction", service)
		{
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00030BA4 File Offset: 0x0002EDA4
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetPowerBIReportContents, base.ActionParameters.ItemPath, "PowerBIReport", null, null, null, null, false, base.ActionParameters.CatalogItemContent.Content, null);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00030BFA File Offset: 0x0002EDFA
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemPath = parameters.Item;
			base.ActionParameters.CatalogItemContent = new CatalogItemContent(parameters.Content);
			this.PerformActionNow();
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0000E9A1 File Offset: 0x0000CBA1
		protected override void ValidateAccess(PowerBIReportCatalogItem item)
		{
			item.ThrowIfNoAccess(ReportOperation.UpdateReportDefinition);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00030C2C File Offset: 0x0002EE2C
		internal override void Update(PowerBIReportCatalogItem item)
		{
			this.ValidateAccess(item);
			this.PrepareForUpdate(item);
			item.SetPreShreddedReadStreams(base.ActionParameters.Original, base.ActionParameters.Pbix, base.ActionParameters.Model);
			item.DataModelParameters = base.ActionParameters.DataModelParameters;
			item.Save(false);
		}
	}
}
