using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001A2 RID: 418
	internal sealed class SetRdlxReportDefinitionAction : UpdateItemAction<SetReportDefinitionActionParameters, RdlxReportCatalogItem>
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x00036C99 File Offset: 0x00034E99
		public SetRdlxReportDefinitionAction(RSService service)
			: base("SetRdlxReportDefinitionAction", service)
		{
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00036CA8 File Offset: 0x00034EA8
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetRdlxReportDefinition, base.ActionParameters.ItemPath, "Report", null, null, null, null, false, base.ActionParameters.Definition, null);
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00005BEF File Offset: 0x00003DEF
		internal override bool AllowEditSession
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00036CF9 File Offset: 0x00034EF9
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemPath = parameters.Item;
			base.ActionParameters.Definition = parameters.Content;
			this.PerformActionNow();
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00036D23 File Offset: 0x00034F23
		internal override void Update(RdlxReportCatalogItem report)
		{
			base.ActionParameters.Warnings = report.UpdateDefinition(base.ActionParameters.Definition);
		}
	}
}
