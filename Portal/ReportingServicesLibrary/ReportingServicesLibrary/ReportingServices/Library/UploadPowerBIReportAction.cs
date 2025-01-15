using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200015F RID: 351
	internal sealed class UploadPowerBIReportAction : CreateItemAction<UploadPowerBIReportActionParameters, PowerBIReportCatalogItem>
	{
		// Token: 0x06000D42 RID: 3394 RVA: 0x00030904 File Offset: 0x0002EB04
		internal UploadPowerBIReportAction(RSService service)
			: base("UploadPowerBIReportAction", service)
		{
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00030914 File Offset: 0x0002EB14
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.CreatePowerBIReport, base.ActionParameters.ItemName, "PowerBIReport", base.ActionParameters.ParentPath, "Parent", null, null, base.ActionParameters.Overwrite, null, Property.ThisArrayToXml(base.ActionParameters.Properties));
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00030984 File Offset: 0x0002EB84
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemName = parameters.Item;
			base.ActionParameters.ParentPath = parameters.Parent;
			base.ActionParameters.Overwrite = parameters.BoolParam;
			base.ActionParameters.Properties = Property.XmlToThisArray(parameters.Properties);
			this.PerformActionNow();
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool IsUpdateSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000309E0 File Offset: 0x0002EBE0
		protected override void CreateItem(PowerBIReportCatalogItem item)
		{
			item.SetPreShreddedReadStreams(base.ActionParameters.Original, base.ActionParameters.Pbix, base.ActionParameters.Model);
			item.DataModelParameters = base.ActionParameters.DataModelParameters;
			item.Create();
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00030A20 File Offset: 0x0002EC20
		protected override void UpdateExistingItem(PowerBIReportCatalogItem item)
		{
			item.SetPreShreddedReadStreams(base.ActionParameters.Original, base.ActionParameters.Pbix, base.ActionParameters.Model);
			item.DataModelParameters = base.ActionParameters.DataModelParameters;
			item.Save(true);
		}
	}
}
