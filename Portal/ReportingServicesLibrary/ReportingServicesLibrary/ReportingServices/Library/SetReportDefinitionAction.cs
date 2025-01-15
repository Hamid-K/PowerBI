using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001A3 RID: 419
	internal sealed class SetReportDefinitionAction : UpdateItemAction<SetReportDefinitionActionParameters, ProfessionalReportCatalogItem>
	{
		// Token: 0x06000F37 RID: 3895 RVA: 0x00036D41 File Offset: 0x00034F41
		public SetReportDefinitionAction(RSService service)
			: base("SetReportDefinitionAction", service)
		{
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00036D50 File Offset: 0x00034F50
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetReportDefinition, base.ActionParameters.ItemPath, "Report", null, null, null, null, false, base.ActionParameters.Definition, null);
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x000053DC File Offset: 0x000035DC
		internal override bool AllowEditSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00036CF9 File Offset: 0x00034EF9
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemPath = parameters.Item;
			base.ActionParameters.Definition = parameters.Content;
			this.PerformActionNow();
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00036D23 File Offset: 0x00034F23
		internal override void Update(ProfessionalReportCatalogItem report)
		{
			base.ActionParameters.Warnings = report.UpdateDefinition(base.ActionParameters.Definition);
		}
	}
}
