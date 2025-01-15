using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000197 RID: 407
	internal class CreateRdlxReportAction : CreateItemAction<CreateReportActionParameters, RdlxReportCatalogItem>
	{
		// Token: 0x06000EED RID: 3821 RVA: 0x00036541 File Offset: 0x00034741
		internal CreateRdlxReportAction(RSService service)
			: base("CreateRdlxReportAction", service)
		{
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00036550 File Offset: 0x00034750
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.CreateRdlxReport, base.ActionParameters.ItemName, "Report", base.ActionParameters.ParentPath, "Parent", null, null, base.ActionParameters.Overwrite, base.ActionParameters.ReportDefinition, Property.ThisArrayToXml(base.ActionParameters.Properties));
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x000365C8 File Offset: 0x000347C8
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemName = parameters.Item;
			base.ActionParameters.ParentPath = parameters.Parent;
			base.ActionParameters.Overwrite = parameters.BoolParam;
			base.ActionParameters.ReportDefinition = parameters.Content;
			base.ActionParameters.Properties = Property.XmlToThisArray(parameters.Properties);
			this.PerformActionNow();
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool IsUpdateSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00036638 File Offset: 0x00034838
		protected override void UpdateExistingItem(RdlxReportCatalogItem item)
		{
			SetRdlxReportDefinitionAction setRdlxReportDefinitionAction = base.Service.SetRdlxReportDefinitionAction;
			setRdlxReportDefinitionAction.ActionParameters.Definition = base.ActionParameters.ReportDefinition;
			setRdlxReportDefinitionAction.Update(item);
			base.ActionParameters.Warnings = setRdlxReportDefinitionAction.ActionParameters.Warnings;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00036684 File Offset: 0x00034884
		protected override void PrepareForNewItem(RdlxReportCatalogItem theItem)
		{
			byte[] array = ((base.ActionParameters.ReportDefinition == null) ? theItem.Content : base.ActionParameters.ReportDefinition);
			base.ActionParameters.Warnings = theItem.PrepareNewReport(array, true);
		}
	}
}
